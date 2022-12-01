using DataServices;
using Menu.ViewModels;
using Models.Constants;
using Utilities;

namespace Menu.Models
{
    public class WeaponItemModel : ModelBase
    {
        public int WeaponId { get; set; }
        public PackageType PackageType { get; set; }
        public string WeaponName { get; set; }
        public string WeaponDescription { get; set; }
        public WeaponsSprite WeaponEnum { get; set; }
        public bool DisableWindEffect { get; set; }
        public bool Silenced { get; set; }
        public AccessType AccessType { get; set; }
        public int AccessCost { get; set; }

        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var weaponItemBindingViewModel = viewModel as WeaponItemBindingViewModel;
            if (weaponItemBindingViewModel)
            {
                weaponItemBindingViewModel.WeaponId = WeaponId;
                weaponItemBindingViewModel.PackageType = PackageType;
                weaponItemBindingViewModel.WeaponName = WeaponName;
                weaponItemBindingViewModel.WeaponDescription = WeaponDescription;
                TrixResource.Sprites.GetByEnum(WeaponEnum, string.Empty, sprite => { weaponItemBindingViewModel.WeaponImage = sprite; });
                weaponItemBindingViewModel.DisableWindEffect = DisableWindEffect;
                weaponItemBindingViewModel.Silenced = Silenced;
                weaponItemBindingViewModel.AccessCost = AccessCost;
                weaponItemBindingViewModel.LockStatus = LockStatus.NeedMoney;
                WeaponDataService.Instance.GetEquippedWeaponId(weaponId =>
                {
                    if (WeaponId == weaponId)
                    {
                        weaponItemBindingViewModel.LockStatus = LockStatus.Equipped;
                    }
                    else
                    {
                        if (AccessType == AccessType.Free)
                        {
                            weaponItemBindingViewModel.LockStatus = LockStatus.Unlocked;
                        }
                        else
                        {
                            weaponItemBindingViewModel.LockStatus = AccessType == AccessType.ByPurchase
                                ? LockStatus.NeedMoney
                                : LockStatus.NeedCoin;
                        }
                    }
                });
            }
        }
    }
}