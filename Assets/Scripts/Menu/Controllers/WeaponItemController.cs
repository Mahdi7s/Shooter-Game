using Controllers;
using DataServices;
using Messaging.Hub.Providers;
using Models;
using Models.Constants;
using Utilities;

namespace Menu.Controllers
{
    public class WeaponItemController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = false;
        protected internal override bool IsPopup { get; set; }

        public void EquipWeapon(int weaponId)
        {
            WeaponDataService.Instance.EquipPlayerWeapon(weaponId);
            SimpleMessaging.Instance.SendMessage(new MessageData<int, string> { Message = weaponId }, typeof(WeaponsController).FullName);
        }

        public void BuyWeaponWithCoin(int weaponId, int cost)
        {
            CoinDataService.Instance.GetPlayerCoin(playerCoin =>
            {
                if (playerCoin < cost)
                {
                    TrixResource.GameObjects.NoCoinPopup_async(popupGameObject =>
                    {
                        PopupController.Instance.ShowPopup(typeof(NoCoinPopupController), popupGameObject, new PopupData());
                    });
                }
                else
                {
                    Refresh();
                    WeaponDataService.Instance.AddPlayerWeapon(weaponId);
                    EquipWeapon(weaponId);
                    CoinDataService.Instance.DecreasePlayerCoin(cost, "Sink");
                }
            });
        }
        public void BuyWeaponWithRealMoney(PackageType weaponPackageType, int weaponId)
        {
            PurchaseController.Instance.BuyPackage(weaponPackageType, isSucceed =>
            {
                if (isSucceed)
                {
                    Refresh();
                    WeaponDataService.Instance.AddPlayerWeapon(weaponId);
                    EquipWeapon(weaponId);
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
        public void Refresh()
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<int, string> { Command = "Refresh" }, typeof(WeaponsController).FullName);
        }
    }
}