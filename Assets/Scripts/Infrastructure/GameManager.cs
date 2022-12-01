using DataServices;
using Extensions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using Models.Constants;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Infrastructure
{
    public class GameManager : Singleton<GameManager>
    {
        public Sprite NoImageSprite { get; set; }
        public bool CanPurchasePackages { get; set; } = true;
        public bool CanUseAdvertises { get; set; } = true;
        public bool CanUseGooglePlay { get; set; }
        public bool CanUseGlimAdvertise { get; set; } = true;
        public bool CanUseGlimPhone { get; set; } = true;
        public bool PreventGoogleSave { get; set; }
        public bool FirebaseInitialised { get; set; }
        public string ActiveControllerName { get; set; }
        public PlayerSaveData PlayerSaveData { get; set; } = new PlayerSaveData();
        public List<MissionProgress> MissionProgresses { get; set; } = new List<MissionProgress>();
        public ChapterMission ChapterMission { get; set; } = new ChapterMission(1, 1);
        public bool IsInGameplay { get; set; }
        public bool IsPaused { get; set; }
        public Settings GameSettings { get; set; }
        private void Awake()
        {
            NoImageSprite = TrixResource.Sprites.spr_NoImage;
            Input.multiTouchEnabled = false;
            if (ES3.KeyExists(StaticValues.GameSettings))
            {
                var settingsString = ES3.Load<string>(StaticValues.GameSettings);
                GameSettings = JsonConvert.DeserializeObject<Settings>(settingsString);
            }
            else
            {
                GameSettings = new Settings();
                var settingsString = JsonConvert.SerializeObject(GameSettings);
                ES3.Save<string>(StaticValues.GameSettings, settingsString);
            }
            TrixSoundManager.Instance.SetConfig(GameSettings.Music, GameSettings.Sfx);
            InitialiseFirebase();
        }

        public void Initialize(Action initialized)
        {
            GameDataService.Instance.LoadPlayerData(playerData =>
            {
                if (playerData != null)
                    PlayerSaveData = playerData;
                GameDataService.Instance.SaveProgress();
                initialized();
            });
        }
        private async Task InitialiseFirebase()
        {
            try
            {
                var defaults = new Dictionary<string, object>
                {
                    {"CanPurchasePackages", true}, {"CanUseAdvertises", true}, {"CanUseGooglePlay", false}, {"CanUseGlimAdvertise", true}, {"CanUseGlimPhone", true}
                };

                FirebaseRemoteConfig.SetDefaults(defaults);

                await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        // Create and hold a reference to your FirebaseApp, i.e.
                        //   app = Firebase.FirebaseApp.DefaultInstance;
                        // where app is a Firebase.FirebaseApp property of your application class.

                        // Set a flag here indicating that Firebase is ready to use by your
                        // application.
                        FirebaseInitialised = true;

                        CanPurchasePackages = FirebaseRemoteConfig.GetValue("CanPurchasePackages").BooleanValue;
                        CanUseAdvertises = FirebaseRemoteConfig.GetValue("CanUseAdvertises").BooleanValue;
                        CanUseGooglePlay = FirebaseRemoteConfig.GetValue("CanUseGooglePlay").BooleanValue;
                        CanUseGlimAdvertise = FirebaseRemoteConfig.GetValue("CanUseGlimAdvertise").BooleanValue;
                        CanUseGlimPhone = FirebaseRemoteConfig.GetValue("CanUseGlimPhone").BooleanValue;

                        FirebaseRemoteConfig.FetchAsync().ContinueWith(remoteTask =>
                        {
                            if (remoteTask.IsCompleted)
                            {
                                FirebaseRemoteConfig.ActivateFetched();
                                CanPurchasePackages = FirebaseRemoteConfig.GetValue("CanPurchasePackages").BooleanValue;
                                CanUseAdvertises = FirebaseRemoteConfig.GetValue("CanUseAdvertises").BooleanValue;
                                CanUseGooglePlay = FirebaseRemoteConfig.GetValue("CanUseGooglePlay").BooleanValue;
                                CanUseGlimAdvertise = FirebaseRemoteConfig.GetValue("CanUseGlimAdvertise").BooleanValue;
                                CanUseGlimPhone = FirebaseRemoteConfig.GetValue("CanUseGlimPhone").BooleanValue;
                            }
                        });
                    }
                    else
                    {
                        FirebaseInitialised = false;
                        Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                        // Firebase Unity SDK is not safe to use here.
                    }
                });
            }
            catch (Exception e)
            {
                FirebaseInitialised = false;
                Debug.LogError(e.Message);
                if (e.InnerException != null)
                    Debug.LogError($"InnerException: {e.InnerException.Message}");
            }
        }
        public void AddChapterProgress(MissionProgress missionProgress)
        {
            var mission =
                MissionProgresses.FirstOrDefault(x => x.ChapterNumber == missionProgress.ChapterNumber && x.MissionNumber == missionProgress.MissionNumber);
            if (mission != null)
            {
                mission.InnocentCasualties += missionProgress.InnocentCasualties;
                mission.LoseCount += missionProgress.LoseCount;
                mission.EnemiesKilled += missionProgress.EnemiesKilled;
                mission.MissedShots += missionProgress.MissedShots;
                mission.TotalHits += missionProgress.TotalHits;
                mission.TotalShots += missionProgress.TotalShots;
            }
            else
            {
                MissionProgresses.Add(missionProgress);
            }
        }
        public void SaveCurrentChapterProgress()
        {
            GameDataService.Instance.SaveChapterProgress(ChapterMission.ChapterNumber, MissionProgresses);
        }
        public void SaveEndChapter()
        {
            GameDataService.Instance.SaveEndChapter(ChapterMission.ChapterNumber, MissionProgresses);
        }
        public void DestroyMonoBehaviourComponent(MonoBehaviour component)
        {
            Destroy(component);
        }

        public void NeedSave(Action<bool, int> callback)
        {
            GameDataService.Instance.LoadChapterProgress(ChapterMission.ChapterNumber,
                savedProgress =>
                {
                    var lastSavedMission = savedProgress.Count > 0 ? savedProgress.Max(x => x.MissionNumber) : 0;
                    callback(!savedProgress.EqualsAll(MissionProgresses), lastSavedMission);
                });
        }
        public int GetLoseCount()
        {
            return MissionProgresses.Sum(m => m.LoseCount);
        }

        public bool HasLostBeforeInCurrentChapter()
        {
            return MissionProgresses.Any(m => m.LoseCount > 0);
        }
    }
}
