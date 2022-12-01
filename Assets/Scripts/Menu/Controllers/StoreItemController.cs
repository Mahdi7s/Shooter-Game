using Controllers;
using DataServices;
using Models;
using Models.Constants;
using Utilities;

namespace Menu.Controllers
{
    public class StoreItemController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = false;
        protected internal override bool IsPopup { get; set; }

        public void BuyPackage(PackageType packageType, int coinAmount)
        {
            PurchaseController.Instance.BuyPackage(packageType, result =>
            {
                if (result)
                {
                    CoinDataService.Instance.IncreasePlayerCoin(coinAmount,"Source");
                }
                else
                {
                    TrixResource.GameObjects.InfoPopup_async(popupGameObject =>
                    {
                        PopupController.Instance.ShowPopup(typeof(InfoPopupController), popupGameObject, new PopupData { Data = "خطا در خرید" });
                    });
                }
            });
        }
    }
}