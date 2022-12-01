namespace Menu.Models
{
    public abstract class ModelBase
    {
        public int Id { get; set; }

        public abstract void ToViewModel<TViewModel>(TViewModel viewModel);
    }
}
