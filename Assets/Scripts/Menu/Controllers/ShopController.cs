using System;
using Controllers;
using DataServices;
using Infrastructure;
using Models.Constants;
using Utilities;

namespace Menu.Controllers
{
    public class ShopController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        public Action<TimeSpan> OrangeTimeChanged { get; set; }
        protected override void Awake()
        {
            base.Awake();
            PlayerOrangeHandler.Instance.OrangeTimeChanged = OrangeTimeChanged;
        }

        public void PurchaseFirstBooster(Action<bool> purchaseResult)
        {
            PurchaseController.Instance.BuyBooster(1, result =>
            {
                if (result)
                {
                    GameManager.Instance.PlayerSaveData.BoosterPower = 1;
                    GameDataService.Instance.SaveProgress();
                    StaticValues.OrangeTimer = 180;
                    purchaseResult(true);
                }
                else
                {
                    purchaseResult(false);
                }
            });
        }
        public void PurchaseSecondBooster(Action<bool> purchaseResult)
        {
            PurchaseController.Instance.BuyBooster(2, result =>
            {
                if (result)
                {
                    GameManager.Instance.PlayerSaveData.BoosterPower = 2;
                    GameDataService.Instance.SaveProgress();
                    StaticValues.OrangeTimer = 300;
                    purchaseResult(true);
                }
                else
                {
                    purchaseResult(false);
                }
            });
        }
    }
}