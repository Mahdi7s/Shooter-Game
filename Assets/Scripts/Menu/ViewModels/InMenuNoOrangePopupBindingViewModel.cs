using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(InMenuNoOrangePopupController))]*/
    public class InMenuNoOrangePopupBindingViewModel : TrixViewModel<InMenuNoOrangePopupController>
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

        public void OnShopButtonClickEvent()
        {
            Controller.GoToShop();
        }

        public void OnClosePopupClickEvent()
        {
            Controller.ClosePopup();
        }
    }
}