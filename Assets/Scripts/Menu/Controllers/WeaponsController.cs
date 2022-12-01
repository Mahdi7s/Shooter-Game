using DataServices;
using Menu.ViewModels;
using Messaging.Hub.Providers;
using Packages.M4u.Scripts.Trixmen;
using System;
using System.Collections.Generic;

namespace Menu.Controllers
{
    public class WeaponsController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        public Action<int> SelectedWeaponChanged { get; set; }
        public Action RefreshList { get; set; }
        protected override void Awake()
        {
            base.Awake();
            SimpleMessaging.Instance.Register<int>(this, data =>
            {
                if (string.IsNullOrEmpty(data.Command))
                {
                    SelectedWeaponChanged?.Invoke(data.Message);
                }
                else
                {
                    RefreshList?.Invoke();
                }
            }, GetType().FullName);
        }

        private void OnDestroy()
        {
            SimpleMessaging.Instance.UnRegister<int>(this);
        }

        public void InitializeWeaponsList(List<WeaponItemBindingViewModel> weaponItems)
        {
            WeaponDataService.Instance.GetWeapons().ToViewModels(weaponItems, gameObject, typeof(WeaponItemBindingViewModel));
        }
    }
}