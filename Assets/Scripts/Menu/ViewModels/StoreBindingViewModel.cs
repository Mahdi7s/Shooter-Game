using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using System.Collections.Generic;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(StoreController))]*/
    public class StoreBindingViewModel : TrixViewModel<StoreController>
    {
        private readonly TrixProp<List<StoreItemBindingViewModel>> _packagesList = new TrixProp<List<StoreItemBindingViewModel>>(new List<StoreItemBindingViewModel>());

        public List<StoreItemBindingViewModel> PackagesList
        {
            get { return _packagesList.Value; }
            set { _packagesList.Value = value; }
        }
        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
        }
        private void OnIsShownChanged(bool shown, object data)
        {
            if (shown)
            {
                Controller.InitializePackagesList(PackagesList);
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