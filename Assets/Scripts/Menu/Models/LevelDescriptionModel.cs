using DataServices;
using Menu.ViewModels;

namespace Menu.Models
{
    public class LevelDescriptionModel : ModelBase
    {
        public int ChapterNumber { get; set; }
        public int LevelNumber { get; set; }
        public string Target { get; set; }
        public string Story { get; set; }
        public string AchievementDescription { get; set; }
        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var levelDescriptionBindingViewModel = viewModel as LevelDescriptionBindingViewModel;
            if (levelDescriptionBindingViewModel)
            {
                levelDescriptionBindingViewModel.Title = $"{LevelNumber}/{GameDataService.Instance.GetLevelsCount(ChapterNumber)}";
                levelDescriptionBindingViewModel.Target = Target;
                levelDescriptionBindingViewModel.Story = Story;
                levelDescriptionBindingViewModel.AchievementDescription = AchievementDescription;
            }
        }
    }
}