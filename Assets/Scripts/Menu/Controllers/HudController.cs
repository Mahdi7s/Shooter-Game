using DataServices;
using Infrastructure;
using Messaging.Hub.Providers;
using Models.Constants;
using System;
using Controllers;
using Models;
using Utilities;

namespace Menu.Controllers
{
    public class HudController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; }
        public Action<int> OrangeCountChanged { get; set; }
        public Action<int> CoinAmountChanged { get; set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerOrangeHandler.Instance.OrangeCountChanged = OrangeCountChanged;
            CoinDataService.Instance.CoinAmountChanged = CoinAmountChanged;
        }

        public void InitializeHud()
        {
            CoinDataService.Instance.GetPlayerCoin(playerCoin =>
            {
                CoinAmountChanged?.Invoke(playerCoin);
            });
            OrangeCountChanged?.Invoke(PlayerOrangeHandler.Instance.OrangeCount);
        }

        public void OnOrangeBarClick()
        {
            ShowView(typeof(ShopController));
        }
        public void OnCoinClick()
        {
            ShowView(typeof(StoreController));
        }
        public void OnSettingClick()
        {
            TrixResource.GameObjects.SettingsPopup_async(result =>
            {
                PopupController.Instance.ShowPopup(typeof(SettingsPopupController), result, new PopupData());
            });
        }
        public void OnBackButtonPressed()
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<Type, string>(GetType(), StaticValues.OnBackPressed), GameManager.Instance.ActiveControllerName);
        }
    }
}