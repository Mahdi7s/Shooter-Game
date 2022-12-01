using DataServices;
using Menu.ViewModels;

namespace Menu.Models
{
    public class MissionDescriptionModel : ModelBase
    {
        public int ChapterNumber { get; set; }
        public int MissionNumber { get; set; }
        public string Target { get; set; }
        public string Story { get; set; }
        public string AchievementDescription { get; set; }
        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var missionDescriptionBindingViewModel = viewModel as MissionDescriptionBindingViewModel;
            if (missionDescriptionBindingViewModel)
            {
                missionDescriptionBindingViewModel.Title = $"{MissionNumber}/{GameDataService.Instance.GetLevelsCount(ChapterNumber)}";
                missionDescriptionBindingViewModel.Target = Target;
                missionDescriptionBindingViewModel.Story = Story;
                missionDescriptionBindingViewModel.AchievementDescription = AchievementDescription;
            }
        }
    }
}