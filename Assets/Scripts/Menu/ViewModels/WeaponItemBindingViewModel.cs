using Menu.Controllers;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;
using Utilities;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(WeaponItemController))]*/
    public class WeaponItemBindingViewModel : TrixViewModel<WeaponItemController>
    {
        private readonly TrixProp<int> _weaponId = new TrixProp<int>();
        private readonly TrixProp<string> _weaponName = new TrixProp<string>();
        private readonly TrixProp<string> _weaponDescription = new TrixProp<string>();
        private readonly TrixProp<Sprite> _weaponImage = new TrixProp<Sprite>();
        private readonly TrixProp<bool> _disableWindEffect = new TrixProp<bool>();
        private readonly TrixProp<bool> _silenced = new TrixProp<bool>();
        private readonly TrixProp<LockStatus> _lockStatus = new TrixProp<LockStatus>();
        private readonly TrixProp<int> _accessCost = new TrixProp<int>();

        public int WeaponId
        {
            get { return _weaponId.Value; }
            set { _weaponId.Value = value; }
        }

        public PackageType PackageType { get; set; }
        public string WeaponName
        {
            get { return _weaponName.Value; }
            set { _weaponName.Value = value; }
        }
        public string WeaponDescription
        {
            get { return _weaponDescription.Value; }
            set { _weaponDescription.Value = value; }
        }

        public Sprite WeaponImage
        {
            get { return _weaponImage.Value; }
            set { _weaponImage.Value = value; }
        }

        public bool DisableWindEffect
        {
            get { return _disableWindEffect.Value; }
            set { _disableWindEffect.Value = value; }
        }

        public bool Silenced
        {
            get { return _silenced.Value; }
            set { _silenced.Value = value; }
        }

        public LockStatus LockStatus
        {
            get { return _lockStatus.Value; }
            set { _lockStatus.Value = value; }
        }

        public int AccessCost
        {
            get { return _accessCost.Value; }
            set { _accessCost.Value = value; }
        }

        protected override void Awake()
        {
            IsListView = true;
            WeaponImage = TrixResource.Sprites.spr_NoImage;
            base.Awake();
        }

        public void OnWeaponButtonClickEvent()
        {
            switch (LockStatus)
            {
                case LockStatus.Equipped:
                    break;
                case LockStatus.Unlocked:
                    Controller.EquipWeapon(WeaponId);
                    break;
                case LockStatus.NeedCoin:
                    Controller.BuyWeaponWithCoin(WeaponId, AccessCost);
                    break;
                case LockStatus.NeedMoney:
                    Controller.BuyWeaponWithRealMoney(PackageType, WeaponId);
                    break;
            }
        }

        protected override void OnBackPressed()
        {
        }
    }
}