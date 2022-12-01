using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using System.Collections.Generic;
using Models.Constants;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(WeaponsController))]*/
    public class WeaponsBindingViewModel : TrixViewModel<WeaponsController>
    {
        private readonly TrixProp<List<WeaponItemBindingViewModel>> _weaponsList = new TrixProp<List<WeaponItemBindingViewModel>>(new List<WeaponItemBindingViewModel>());

        public List<WeaponItemBindingViewModel> WeaponsList
        {
            get { return _weaponsList.Value; }
            set { _weaponsList.Value = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
            Controller.SelectedWeaponChanged = SelectedWeaponChanged;
            Controller.RefreshList = RefreshList;
        }

        private void RefreshList()
        {
            if (IsShown)
            {
                Controller.InitializeWeaponsList(WeaponsList);
            }
        }

        private void SelectedWeaponChanged(int weaponId)
        {
            WeaponsList.ForEach(x =>
            {
                if (x.LockStatus == LockStatus.Equipped || x.LockStatus == LockStatus.Unlocked)
                {
                    x.LockStatus = x.WeaponId == weaponId ? LockStatus.Equipped : LockStatus.Unlocked;
                }
            });
        }

        private void OnIsShownChanged(bool shown, object data)
        {
            if (shown)
            {
                Controller.InitializeWeaponsList(WeaponsList);
            }
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