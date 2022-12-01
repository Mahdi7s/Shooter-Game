using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Infrastructure;
using Models;
using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class GooglePlayController : Singleton<GooglePlayController>
    {
        private ISavedGameMetadata _gameMetadata;
        public bool LoggedIn { get; set; }
        private void Awake()
        {
            var config = new PlayGamesClientConfiguration.Builder()
                // enables saving game progress.
                .EnableSavedGames()
                // requests the email address of the player be available.
                // Will bring up a prompt for consent.
                .RequestEmail()
                // requests a server auth code be generated so it can be passed to an
                //  associated back end server application and exchanged for an OAuth token.
                //.RequestServerAuthCode(false)
                // requests an ID token be generated.  This OAuth token can be used to
                //  identify the player to other services such as Firebase.
                //.RequestIdToken()
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = false;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }

        public void TryLogin(Action<bool> success)
        {
            // authenticate user:
            Social.localUser.Authenticate(isSucceed =>
            {
                Debug.LogWarning("Google Login is successful? " + isSucceed);
                LoggedIn = isSucceed;
                success(isSucceed);
            });
        }

        public void Logout()
        {
            // sign out
            PlayGamesPlatform.Instance.SignOut();
            LoggedIn = false;
        }
        private byte[] ToByteArray(PlayerSaveData playerSaveData, string serializedPlayerSaveData)
        {
            if (playerSaveData != null)
            {
                serializedPlayerSaveData = JsonConvert.SerializeObject(playerSaveData);
            }
            return Encoding.UTF8.GetBytes(serializedPlayerSaveData);
        }
        private PlayerSaveData FromByteArray(byte[] playerSaveData)
        {
            var saveFileString = Encoding.UTF8.GetString(playerSaveData);
            return JsonConvert.DeserializeObject<PlayerSaveData>(saveFileString);
        }
        private void OpenSavedGameFile(Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
        {
#if !UNITY_EDITOR
            try
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.OpenWithAutomaticConflictResolution("CrosswordSave", DataSource.ReadCacheOrNetwork,
                    ConflictResolutionStrategy.UseMostRecentlySaved, callback);
            }
            catch (Exception exception)
            {
                Debug.LogError("Open Save Error: " + exception.Message);
                callback(SavedGameRequestStatus.InternalError, null);
            }
#else
            callback(SavedGameRequestStatus.InternalError, null);
#endif
        }
        public void LoadGameData(Action<PlayerSaveData> callback)
        {
            if (!LoggedIn)
            {
                callback(null);
                return;
            }
            OpenSavedGameFile((status, gameMetadata) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    _gameMetadata = gameMetadata;
                    var savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                    savedGameClient.ReadBinaryData(_gameMetadata, (requestStatus, data) =>
                    {
                        if (requestStatus == SavedGameRequestStatus.Success)
                        {
                            Debug.LogWarning("Loaded");
                            callback(FromByteArray(data));
                        }
                        else
                        {
#if !UNITY_EDITOR
                            Debug.LogError("Load Failed");
#endif
                            callback(null);
                        }
                    });
                }
                else
                {
#if !UNITY_EDITOR
                    Debug.LogError("Open Load File Failed");
#endif
                    callback(null);
                }
            });
        }
        public void SaveGame(PlayerSaveData playerSaveData, string serializedPlayerSaveData)
        {
            if (!LoggedIn)
                return;
            OpenSavedGameFile((status, gameMetadata) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    _gameMetadata = gameMetadata;
                    var savedGameClient = PlayGamesPlatform.Instance.SavedGame;

                    var builder = new SavedGameMetadataUpdate.Builder();
                    builder = builder.WithUpdatedPlayedTime(TimeSpan.Zero).WithUpdatedDescription("Saved game at " + TimeController.Instance.GameTime);
                    var updatedMetadata = builder.Build();
                    var saveDataArray = ToByteArray(playerSaveData, serializedPlayerSaveData);
                    savedGameClient.CommitUpdate(_gameMetadata, updatedMetadata, saveDataArray, (requestStatus, metadata) =>
                    {
                        if (requestStatus == SavedGameRequestStatus.Success)
                        {
                            _gameMetadata = metadata;
                            Debug.LogWarning("Saved");
                        }
                        else
                        {
#if !UNITY_EDITOR
                            Debug.LogError("Not Saved");
#endif
                        }
                    });
                }
                else
                {
#if !UNITY_EDITOR
                    Debug.LogError("Open Save File Failed");
#endif
                }
            });
        }
        public void DeleteSavedGame()
        {
            if (!LoggedIn)
                return;
            OpenSavedGameFile((requestStatus, metadata) =>
            {
                if (requestStatus == SavedGameRequestStatus.Success)
                {
                    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                    savedGameClient.Delete(metadata);
                    Debug.LogWarning("Deleted");
                }
                else
                {
#if !UNITY_EDITOR
                    Debug.LogError("Delete Save File Failed");
#endif
                }
            });
        }
        public void WelcomeAchievement(Action<bool> success)
        {
            if (!LoggedIn)
            {
                success(false);
                return;
            }

            Social.ReportProgress(TrixGooglePlay.achievement_welcome, 100.0f, success);
        }
    }
}
