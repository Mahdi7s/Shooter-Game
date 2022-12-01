using Controllers;
using DataServices;
using Hellmade.Sound;
using Infrastructure;
using Utilities;

namespace Menu.Controllers
{
    public class MainMenuController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        private void Start()
        {
            TrixResource.AudioClips.Menu_async(mainMenu =>
            {
                if (mainMenu)
                {
                    TrixSoundManager.Instance.StartPlay(mainMenu, Audio.AudioType.Music, loop: true, volume: 0.6f);
                }
            });
        }

        protected override void Awake()
        {
            base.Awake();
//            TrixResource.AudioClips.Menu_async(mainMenu =>
//            {
//                if (mainMenu)
//                {
//                    TrixSoundManager.Instance.StartPlay(mainMenu, Audio.AudioType.Music, loop: true, volume: 0.6f);
//                }
//            });
        }
        //private void Start()
        //{
        //    #region WheelsFortuneController
        //    if (ES3.KeyExists(StaticValues.WheelsFortune))
        //    {
        //        var lastShownTime = ES3.Load<DateTime>(StaticValues.WheelsFortune);
        //        var duration = TimeController.Instance.GameTime.Subtract(lastShownTime);

        //        if (duration >= TimeSpan.FromHours(24))
        //            ShowWheelsFortune();
        //    }
        //    else
        //    {
        //        ShowWheelsFortune();
        //    }
        //    #endregion WheelsFortuneController
        //}
        public void CheckForWheelsFortune()
        {
            GameDataService.Instance.GetWheelsFortune(result =>
            {
                if (result)
                {
                    ShowWheelsFortune();
                }
            });
        }
        private void ShowWheelsFortune()
        {
            ShowView(typeof(SpinnerController));
        }
        public void DeleteSaveGame()
        {
            GooglePlayController.Instance.DeleteSavedGame();
        }
        public void ShowChapters()
        {
            ShowView(typeof(ChaptersController));
        }
        public void ShowWeapons()
        {
            ShowView(typeof(WeaponsController));
        }
    }
}