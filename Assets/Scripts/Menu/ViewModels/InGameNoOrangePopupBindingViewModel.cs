using Controllers;
using Menu.Controllers;
using Models;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using Utilities;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(InGameNoOrangePopupController))]*/
    public class InGameNoOrangePopupBindingViewModel : TrixViewModel<InGameNoOrangePopupController>
    {
        protected override void Awake()
        {
            base.Awake();
            Controller.InitializePopupAction = InitializePopup;
        }
        private void ShowAdvertiseError(string message)
        {
            TrixResource.GameObjects.InfoPopup_async(popupGameObject =>
            {
                PopupController.Instance.ShowPopup(typeof(InfoPopupController), popupGameObject, new PopupData { Data = "خطا در نمایش ویدئو تبلیغاتی" });
            });
        }
        private void AddOrange(AdvertiseAction advertiseAction)
        {
            if (advertiseAction == AdvertiseAction.Orange)
            {
                PlayerOrangeHandler.Instance.IncreaseOrange(1);
            }
        }
        private void InitializePopup()
        {
            IsShown = true;
        }

        protected override void OnBackPressed()
        {
            Controller.ClosePopup();
        }

        public void OnSeeAdvertiseButtonClickEvent()
        {
            Controller.ClosePopup();
            AdvertiseController.Instance.ShowAdvertise(AdvertiseType.Video, AdvertiseAction.Orange, result =>
            {
                if (result.IsSuccess)
                {
                    AddOrange(result.AdvertiseAction);
                }
                else
                {
                    ShowAdvertiseError(result.Message);
                }
            });
        }

        public void OnClosePopupClickEvent()
        {
            Controller.ClosePopup();
        }
    }
}