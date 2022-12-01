using Menu.ViewModels;

namespace Menu.Models
{
    public class OrangeItemModel : ModelBase
    {
        public int OrderInList { get; set; }
        public bool IsAvailable { get; set; }
        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var orangeItemViewModel = viewModel as OrangeItemBindingViewModel;
            if (orangeItemViewModel)
            {
                orangeItemViewModel.OrderInList = OrderInList;
                orangeItemViewModel.IsAvailable = IsAvailable;
            }
        }
    }
}