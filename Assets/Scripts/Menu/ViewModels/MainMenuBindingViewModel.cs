using Controllers;
using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using Utilities;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(MainMenuController))]
    public class MainMenuBindingViewModel : TrixViewModel<MainMenuController>
    {
        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
            IsShown = true;
        }

        private void Start()
        {
            Controller.CheckForWheelsFortune();
        }
        private void OnIsShownChanged(bool isShown, object data)
        {
            if (isShown)
            {
                Controller.CheckForWheelsFortune();
            }
        }
        public void OnStartButtonClickEvent()
        {
            Controller.ShowChapters();
        }

        public void OnDeleteSaveFileButtonClickEvent()
        {
            Controller.DeleteSaveGame();
        }

        public void OnFatherClickEvent()
        {
            Controller.ShowWeapons();
        }
        protected override void OnBackPressed()
        {
            if (IsShown)
            {
                TrixResource.GameObjects.ExitPopup_async(result =>
                {
                    PopupController.Instance.ShowPopup(typeof(ExitPopupController), result, null);
                });
            }
        }
    }
}