using System;

namespace Models.Constants
{
    public class StaticValues
    {
        #region Urls
        public const string UrlKey = "Url";
        public const string TimeApiUrl = "http://dts.48degree.com:1372/api/Time/";
        public const string TelegramSupport = "https://t.me/TrixmenStudio";
        public const string TelegramLink = "https://t.me/Trixmen";
        public const string InstagramLink = "https://www.instagram.com/trixmenstudio";
        #endregion

        #region Directories
        public const string DirectoryResources = "Assets/Resources/";
        public const string DirectoryScenes = "Assets/Scenes/";
        public const string DirectoryEnums = "Assets/Scripts/Models/Constants/";
        public const string DirectoryEditors = "Assets/Scripts/Editor/";
        public const string DirectoryUtilities = "Assets/Scripts/Utilities/";

        public const string DirectoryM4UContexts = "Assets/Scripts/Menu/Contexts/";
        public const string DirectoryM4UController = "Assets/Scripts/Menu/Controllers/";
        public const string DirectoryM4UViewModels = "Assets/Scripts/Menu/ViewModels/";
        public const string DirectoryM4UModels = "Assets/Scripts/Menu/Models/";
        #endregion

        #region MenuItems
        public const string MenuItemSceneEditorLoader = "Scenes/";
        public const string MenuItemGenerateRoot = "Generate/";
        #endregion

        #region Messaging Ids
        public const string EnableMissionDescription = "EnableMissionDescription";
        public const string ShowViewCommand = "ShowViewCommand";
        public const string DisplayViewCommand = "DisplayViewCommand";
        public const string HideViewCommand = "HideViewCommand";
        public const string ShowPopupCommand = "ShowPopupCommand";
        public const string HidePopupCommand = "HidePopupCommand";
        public const string MainMenuShownChanged = "MainMenuShownChanged";
        public const string OnBackPressed = "OnBackPressed";
        public const string MissionController = "MissionController";
        #endregion

        #region Messages
        public static string ErrorMessage(int errorCode, bool isInTwoLine = true) =>
            errorCode >= 200 ? $"خطا در اتصال به سرور! {Environment.NewLine} لطفا از وصل بودن اينترنت مطمئن شويد" :
                isInTwoLine ?
                    $"خطا در دریافت اطلاعات {Environment.NewLine} [کد خطا {errorCode}]" :
                    $"خطا در دریافت اطلاعات [کد خطا {errorCode}]";

        #endregion

        #region GameValues        
        public static int OrangeTimer { get; set; } = 720;
        public const float TimerUpdateInterval = 0.5f;
        public const float SyncTimerUpdateInterval = 60f;
        public const int PlayerInitialCoin = 30;
        public const int MaxOrangeCount = 9;
        #endregion GameValues

        #region EasySave
        public const string PlayerSaveData = "PlayerSaveData";
        public const string PreventGoogleSave = "PreventGoogleSave";
        public const string GameSettings = "GameSettings";
        //public const string PlayerCoin = "PlayerCoin";
        //public const string OrangeDate = "OrangeDate";
        //public const string OrangeCount = "OrangeCount";
        //public const string CompletedMissions = "CompletedMissions";
        //public const string RewardedMissions = "RewardedMissions";
        //public const string PlayerWeapons = "PlayerWeapons";
        //public const string BoosterPower = "BoosterPower";
        //public const string WheelsFortune = "WheelsFortune";
        #endregion
    }
}