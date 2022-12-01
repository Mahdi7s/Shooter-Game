using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(NoCoinPopupController))]*/
    public class NoCoinPopupBindingViewModel : TrixViewModel<NoCoinPopupController>
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

        public void OnGoToStoreButtonClickEvent()
        {
            Controller.GoToStore();
        }

        public void OnClosePopupClickEvent()
        {
            Controller.ClosePopup();
        }
    }
}