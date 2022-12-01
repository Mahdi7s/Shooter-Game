using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(GoogleSaveNotificationPopupController))]*/
    public class GoogleSaveNotificationPopupBindingViewModel : TrixViewModel<GoogleSaveNotificationPopupController>
    {
        protected override void Awake()
        {
            base.Awake();
            Controller.InitializePopupAction = InitializePopup;
        }

        private void InitializePopup()
        {
            IsShown = true;
        }

        protected override void OnBackPressed()
        {
            Controller.ClosePopup();
        }

        public void OnConnectGoogleButtonClickEvent()
        {
            Controller.ConnectGoogle();
        }

        public void OnClosePopupClickEvent()
        {
            Controller.ClosePopup();
        }
    }
}