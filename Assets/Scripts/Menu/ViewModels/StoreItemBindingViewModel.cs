using Controllers;
using Menu.Controllers;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(StoreItemController))]*/
    public class StoreItemBindingViewModel : TrixViewModel<StoreItemController>
    {
        public int PackageId { get; set; }
        public PackageType PackageType { get; set; }
        private readonly TrixProp<int> _coinAmount = new TrixProp<int>();
        private readonly TrixProp<int> _cost = new TrixProp<int>();
        public int CoinAmount
        {
            get { return _coinAmount.Value; }
            set { _coinAmount.Value = value; }
        }
        public int Cost
        {
            get { return _cost.Value; }
            set { _cost.Value = value; }
        }
        protected override void Awake()
        {
            IsListView = true;
            base.Awake();
        }
        protected override void OnBackPressed()
        {
        }

        public void OnBuyPackageClickEvent()
        {
            Controller.BuyPackage(PackageType, CoinAmount);
        }
    }
}