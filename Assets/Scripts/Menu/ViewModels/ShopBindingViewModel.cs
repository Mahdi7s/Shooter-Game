using Controllers;
using Infrastructure;
using Menu.Controllers;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using System;
using Models;
using Utilities;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(ShopController))]
    public class ShopBindingViewModel : TrixViewModel<ShopController>
    {
        private readonly TrixProp<string> _orangeRemainingTime = new TrixProp<string>();
        private readonly TrixProp<string> _boosterDescription = new TrixProp<string>();
        private readonly TrixProp<int> _boosterPower = new TrixProp<int>();

        public string OrangeRemainingTime
        {
            get { return _orangeRemainingTime.Value; }
            set { _orangeRemainingTime.Value = value; }
        }
        public string BoosterDescription
        {
            get { return _boosterDescription.Value; }
            set { _boosterDescription.Value = value; }
        }
        public int BoosterPower
        {
            get { return _boosterPower.Value; }
            set
            {
                _boosterPower.Value = value;
                SetBoosterDescription(value);
            }
        }
        protected override void Awake()
        {
            base.Awake();
            Controller.OrangeTimeChanged = OrangeTimeChanged;
            Controller.OnIsShownChanged = OnIsShownChanged;
            BoosterPower = 0;
        }

        private void SetBoosterDescription(int value)
        {
            var time = "12";
            if (value == 2)
            {
                time = "3";
            }
            else if (value == 1)
            {
                time = "6";
            }
            else
            {
                time = "12";
            }

            BoosterDescription = $"هر {time} دقيقه ، خانواده يک پرتقال به پدر ميده تا{Environment.NewLine}بتونه از پوستش براي شليک به بقيه استفاده کنه";
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
        private void OnIsShownChanged(bool shown, object messageData)
        {
            if (shown)
            {
                BoosterPower = GameManager.Instance.PlayerSaveData.BoosterPower;
            }
        }

        private void OrangeTimeChanged(TimeSpan timeSpan)
        {
            OrangeRemainingTime = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        }

        public void OnFirstBoosterClickEvent()
        {
            Controller.PurchaseFirstBooster(result =>
            {
                if (result)
                {
                    BoosterPower = 1;
                }
            });
        }

        public void OnSecondBoosterClickEvent()
        {
            Controller.PurchaseSecondBooster(result =>
            {
                if (result)
                {
                    BoosterPower = 2;
                }
            });
        }
        public void OnFreeOrangeButtonClickEvent()
        {
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
        protected override void OnBackPressed()
        {
            if (IsShown)
            {
                Controller.ShowView(typeof(MainMenuController));
            }
        }
    }
}