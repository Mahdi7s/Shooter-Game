using System.Collections.Generic;
using DataServices;
using Menu.ViewModels;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.Controllers
{
    public class StoreController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        public void InitializePackagesList(List<StoreItemBindingViewModel> packages)
        {
            StoreDataService.Instance.GetStoreItems().ToViewModels(packages, gameObject, typeof(StoreItemBindingViewModel));
        }
    }
}