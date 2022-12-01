using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Infrastructure;
using Menu.Models;
using Models;
using Models.Constants;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace DataServices
{
    public class GameDataService : Singleton<GameDataService>
    {
        public MissionsConfig MissionsConfig;

        private void Awake()
        {
            if (!MissionsConfig)
            {
                MissionsConfig = ResourceManager.LoadResource<MissionsConfig>("MissionConfig");
            }
        }

        public Mission GetMission(int chapter, int mission)
        {
            Mission missionRecord = null;
            missionRecord =
                MissionsConfig.Missions.FirstOrDefault(x => x.Chapter == chapter && x.MissionNum == mission);
            missionRecord?.SetLists(missionRecord.GetEnemies(), missionRecord.GetSceneItems());
            return missionRecord;
        }

        public int? GetReplayCoinNeeded(int chapter)
        {
            return MissionsConfig.Chapters.FirstOrDefault(x => x.Number == chapter)?.ReplayCoin;
        }

        public void InsertOrUpdateMission(Mission mission)
        {
#if UNITY_EDITOR
            var missionRecord = GetMission(mission.Chapter, mission.MissionNum);
            if (missionRecord == null) //new mission. insert it
            {
                MissionsConfig.Missions.Add(mission);
            }
            else //existing mission, update it
            {
                var currentMission = MissionsConfig.Missions.SingleOrDefault(mis =>
                    mis.Chapter == mission.Chapter && mis.MissionNum == mission.MissionNum);
                if (currentMission != null)
                {
                    MissionsConfig.Missions.Remove(currentMission);
                    MissionsConfig.Missions.Add(mission);
                }
                else
                {
                    Debug.LogError("current Mission is null. update failed.");
                }
            }

            EditorUtility.SetDirty(MissionsConfig);
#endif
        }

        public List<Mission> LoadDataWhere<T>(Func<Mission, bool> predicate = null) where T : new()
        {
            var result = predicate != null
                ? MissionsConfig.Missions.Where(predicate).ToList()
                : MissionsConfig.Missions;
            return result;
        }

        public GameObject GetBackgroundForChapter(int chapterNumber)
        {
            var background = MissionsConfig.Chapters.FirstOrDefault(c => c.Number == chapterNumber);
            if (background != null && background.Background != GameplayBackgrounds.Undefined)
            {
                return ResourceManager.LoadResource<GameObject>(
                    $"GameObjects\\GameplayBackgrounds\\{background.Background}");
            }

            return null;
        }

        public int GetLevelsCount(int chapterNumber)
        {
            return MissionsConfig.Missions.Count(m => m.Chapter == chapterNumber);
        }

        public void GetChapters(Action<List<ChapterItemModel>> callback)
        {
            var gameProgress = GameManager.Instance.PlayerSaveData.CompletedMissions;
            callback(MissionsConfig.Chapters.Select(c => new ChapterItemModel
            {
                ChapterName = c.Title,
                ChapterNumber = c.Number,
                LevelsCompleted = LastLevelCompleted(gameProgress, c.Number),
                CurrentLevel = LoadCurrentChapterNumber(gameProgress, c.Number),
                TotalLevels = GetLevelsCount(c.Number),
                ChapterStars = GetChapterStarsCount(gameProgress, c.Number)
            }).ToList());
        }

        private int GetChapterStarsCount(CompletedMissions gameProgress, int chapterNumber)
        {
            var currentChapter = gameProgress.ChaptersStars.FirstOrDefault(x => x.ChapterNumber == chapterNumber);
            return currentChapter?.ChapterStarCount ?? 0;
        }

        private int LastLevelCompleted(CompletedMissions gameProgress, int chapterNumber)
        {
            var chapters = gameProgress.MissionProgresses.Where(x => x.ChapterNumber == chapterNumber).ToList();
            ;
            return chapters.Count > 0 ? chapters.Max(x => x.MissionNumber) : 0;
        }

        public List<LevelDescriptionModel> GetLevelsInChapter(int chapterNumber)
        {
            return MissionsConfig.Missions.Where(m => m.Chapter == chapterNumber).Select(m => new LevelDescriptionModel
            {
                ChapterNumber = m.Chapter,
                LevelNumber = m.MissionNum,
                Target = m.MissionGoal,
                Story = m.Description
            }).ToList();
        }

        public List<MissionDescriptionModel> GetMissionsInChapter(int chapterNumber)
        {
            return MissionsConfig.Missions.Where(m => m.Chapter == chapterNumber).Select(m =>
                new MissionDescriptionModel
                {
                    ChapterNumber = m.Chapter,
                    MissionNumber = m.MissionNum,
                    Target = m.MissionGoal,
                    Story = m.Description
                }).ToList();
        }

        public void SaveMissionProgress(MissionProgress missionProgress)
        {
            var gameProgress = GameManager.Instance.PlayerSaveData.CompletedMissions;
            var mission = gameProgress.MissionProgresses.FirstOrDefault(x =>
                x.ChapterNumber == missionProgress.ChapterNumber &&
                x.MissionNumber == missionProgress.MissionNumber);
            if (mission != null)
            {
                gameProgress.MissionProgresses.Remove(mission);
            }

            gameProgress.MissionProgresses.Add(missionProgress);

            SaveProgress();
        }

        public void SaveChapterProgress(int chapterNumber, List<MissionProgress> missionProgresses)
        {
            var gameProgress = GameManager.Instance.PlayerSaveData.CompletedMissions;
            var missions = gameProgress.MissionProgresses.Where(x => x.ChapterNumber == chapterNumber);
            gameProgress.MissionProgresses = gameProgress.MissionProgresses.Except(missions).ToList();
            gameProgress.MissionProgresses.AddRange(missionProgresses);
            SaveProgress();
        }

        public void SaveEndChapter(int chapterNumber, List<MissionProgress> missionProgresses)
        {
            var gameProgress = GameManager.Instance.PlayerSaveData.CompletedMissions;
            var missions = gameProgress.MissionProgresses.Where(x => x.ChapterNumber == chapterNumber);
            gameProgress.MissionProgresses = gameProgress.MissionProgresses.Except(missions).ToList();
            gameProgress.MissionProgresses.AddRange(missionProgresses);
            var currentStars = CalculateStarsCount(chapterNumber, missionProgresses);
            var lastStars = GetChapterStarsCount(gameProgress, chapterNumber);
            if (currentStars > lastStars)
            {
                var currentChapter = gameProgress.ChaptersStars.FirstOrDefault(x => x.ChapterNumber == chapterNumber);
                if (currentChapter != null)
                {
                    currentChapter.ChapterStarCount = currentStars;
                }
                else
                {
                    gameProgress.ChaptersStars.Add(new ChaptersStars
                    {
                        ChapterNumber = chapterNumber,
                        ChapterStarCount = currentStars
                    });
                }
            }

            SaveProgress();
        }

        public int CalculateStarsCount(int chapterNumber, List<MissionProgress> missions)
        {
            var starCount = 0;
            if (missions.Count > 0)
            {
                var loseCount = missions.Sum(x => x.LoseCount);
                var chapter = MissionsConfig.Chapters.FirstOrDefault(x => x.Number == chapterNumber);
                if (chapter != null)
                {
                    if (loseCount > chapter.OneStarMaxLose)
                        starCount = 0;
                    if (loseCount > chapter.TwoStarMaxLose)
                        starCount = 1;
                    if (loseCount > chapter.ThreeStarMaxLose)
                        starCount = 2;
                    if (loseCount <= chapter.ThreeStarMaxLose)
                        starCount = 3;
                }
            }

            return starCount;
        }

        public int GetEndChapterReward(int chapterNumber)
        {
            if (GameManager.Instance.PlayerSaveData.HighestChapterFinished < chapterNumber)
            {
                GameManager.Instance.PlayerSaveData.HighestChapterFinished = chapterNumber;
                var chapter = MissionsConfig.Chapters.FirstOrDefault(x => x.Number == chapterNumber);
                if (chapter != null)
                {
                    CoinDataService.Instance.IncreasePlayerCoin(chapter.EndChapterReward, "Source");
                    SaveProgress();
                    return chapter.EndChapterReward;
                }
            }

            return 0;
        }

        public void LoadChapterProgress(int chapterNumber, Action<List<MissionProgress>> callback)
        {
            callback(GameManager.Instance.PlayerSaveData.CompletedMissions.MissionProgresses
                .Where(x => x.ChapterNumber == chapterNumber).ToList());
        }

        public void SetTempMissionProgresses()
        {
            GameManager.Instance.MissionProgresses.Clear();
            GameManager.Instance.MissionProgresses = GameManager.Instance.PlayerSaveData.CompletedMissions
                .MissionProgresses.Where(x => x.ChapterNumber == GameManager.Instance.ChapterMission.ChapterNumber)
                .ToList();
        }

        public int LoadCurrentChapterNumber(CompletedMissions gameProgress, int chapterNumber)
        {
            var chapter = gameProgress.MissionProgresses.Where(x => x.ChapterNumber == chapterNumber).ToList();
            if (chapter.Any())
            {
                return chapter.Max(x => x.MissionNumber) >= GetLevelsCount(chapterNumber)
                    ? 1
                    : chapter.Max(x => x.MissionNumber) + 1;
            }

            return 1;
        }

        public AnimationData GetAnimationData(CharacterType character, AnimationNames animationName)
        {
            return MissionsConfig.Characters.FirstOrDefault(c => c.CharacterType == character)
                ?.Animations
                .FirstOrDefault(a => a.AnimationName == animationName);
        }

        public void TrySetWinReward(int chapterNumber, int missionNumber, int winReward, Action<bool> callback)
        {
            var rewardedMission = GameManager.Instance.PlayerSaveData.RewardedMissions.FirstOrDefault(x =>
                x.ChapterNumber == chapterNumber && x.MissionNumber == missionNumber);
            if (rewardedMission != null)
            {
                callback(false);
            }
            else
            {
                CoinDataService.Instance.IncreasePlayerCoin(winReward, "Source");
                GameManager.Instance.PlayerSaveData.RewardedMissions.Add(new ChapterMission(chapterNumber,
                    missionNumber));
                SaveProgress();
                callback(true);
            }
        }

        public void GetWheelsFortune(Action<bool> callback)
        {
            TimeController.Instance.GetRealTimeDate((realTime, errorMessage) =>
            {
                if (realTime.HasValue)
                {
                    var duration = realTime.Value.Subtract(GameManager.Instance.PlayerSaveData.WheelsFortune);
                    if (duration >= TimeSpan.FromHours(24))
                    {
                        SetWheelsFortune(realTime.Value);
                        callback(true);
                    }
                }
                else
                {
                    callback(false);
                }
            });
        }

        public void SetWheelsFortune(DateTime realTime)
        {
            GameManager.Instance.PlayerSaveData.WheelsFortune = realTime;
            SaveProgress();
        }

        public void LoadPlayerData(Action<PlayerSaveData> callback)
        {
            var playerData = new PlayerSaveData
            {
                PlayerCoin = StaticValues.PlayerInitialCoin,
                //WheelsFortune = TimeController.Instance.GameTime,
                OrangeCount = 9,
                OrangeDate = TimeController.Instance.GameTime,
                BoosterPower = 0,
                RewardedMissions = new List<ChapterMission>(),
                CompletedMissions = new CompletedMissions(),
                PlayerWeapons = WeaponDataService.Instance.GetInitialWeapons()
            };
            if (ES3.KeyExists(StaticValues.PlayerSaveData))
            {
                var playerLevelsString = ES3.Load<string>(StaticValues.PlayerSaveData);
                if (!string.IsNullOrEmpty(playerLevelsString))
                {
                    playerData = JsonConvert.DeserializeObject<PlayerSaveData>(playerLevelsString);
                }

                callback(playerData);
            }
            else
            {
                if (!GameManager.Instance.PreventGoogleSave)
                {
                    GooglePlayController.Instance.LoadGameData(data =>
                    {
                        if (data != null)
                        {
                            playerData = data;
                        }

                        callback(playerData);
                    });
                }
                else
                {
                    callback(playerData);
                }
            }
        }

        public void SaveProgress()
        {
            var playerLevelsString = JsonConvert.SerializeObject(GameManager.Instance.PlayerSaveData);
            ES3.Save<string>(StaticValues.PlayerSaveData, playerLevelsString);
            if (!GameManager.Instance.PreventGoogleSave)
            {
                GooglePlayController.Instance.SaveGame(null, playerLevelsString);
            }
        }
    }
}