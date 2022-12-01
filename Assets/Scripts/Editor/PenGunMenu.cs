using Models.Constants;
using System;
using System.Linq;
using DataServices;
using Infrastructure;
using Models;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEditor;
using static UnityEngine.SceneManagement.SceneManager;

namespace Assets.Scripts.Editor
{
    //[CustomEditor(typeof(MissionManager))]
    [InitializeOnLoad]
    public class PenGunMenu : UnityEditor.Editor
    {
        [MenuItem("PenGun/Save Mission %F5")]
        static void Save()
        {
            var missionManager = GetActiveScene().GetRootGameObjects().Single(o => o.CompareTag("MainCamera"))
                .GetComponent<MissionManager>();
            if (missionManager)
            {
                missionManager.SaveMission();
            }
        }
        [MenuItem("PenGun/Reset Orange Count")]
        public static void ResetOrangeCount()
        {
            var playerData = new PlayerSaveData
            {
                PlayerCoin = StaticValues.PlayerInitialCoin,
                PlayerWeapons = WeaponDataService.Instance.GetInitialWeapons()
            };
            if (ES3.KeyExists(StaticValues.PlayerSaveData))
            {
                var playerLevelsString = ES3.Load<string>(StaticValues.PlayerSaveData);
                if (!string.IsNullOrEmpty(playerLevelsString))
                {
                    playerData = JsonConvert.DeserializeObject<PlayerSaveData>(playerLevelsString);
                }
            }
            playerData.OrangeCount = 9;
            playerData.OrangeDate = DateTime.Now;
            var playerDataString = JsonConvert.SerializeObject(playerData);
            ES3.Save<string>(StaticValues.PlayerSaveData, playerDataString);
        }
        static PenGunMenu()
        {
            EditorApplication.playModeStateChanged += change =>
            {
                if (change == PlayModeStateChange.ExitingEditMode)
                {
                    Save();
                }
            };
        }

        [MenuItem("PenGun/Set Mission Rewards")]
        public static void SetMissionRewards()
        {
            var missionsConfig = ResourceManager.LoadResource<MissionsConfig>("MissionConfig");
            if (missionsConfig)
            {
                missionsConfig.Missions = missionsConfig.Missions.OrderBy(x => x.Chapter).ThenBy(x => x.MissionNum).ToList();
                foreach (var mission in missionsConfig.Missions)
                {
                    mission.WinReward = mission.Chapter * 10;
                }
                EditorUtility.SetDirty(missionsConfig);
            }
        }
    }
}
