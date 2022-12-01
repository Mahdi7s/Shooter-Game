using Menu.ViewModels;
using Models.Constants;

namespace Menu.Models
{
    public class StoreItemModel : ModelBase
    {
        public PackageType PackageType { get; set; }
        public int CoinAmount { get; set; }
        public int Cost { get; set; }

        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var storeItemBindingViewModel = viewModel as StoreItemBindingViewModel;
            if (storeItemBindingViewModel)
            {
                storeItemBindingViewModel.PackageId = Id;
                storeItemBindingViewModel.PackageType = PackageType;
                storeItemBindingViewModel.CoinAmount = CoinAmount;
                storeItemBindingViewModel.Cost = Cost;
            }
        }
    }
}