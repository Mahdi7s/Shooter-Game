using Menu.ViewModels;
using Utilities;

namespace Menu.Models
{
    public class ChapterItemModel : ModelBase
    {
        public string ChapterName { get; set; }
        public int ChapterNumber { get; set; }
        public int LevelsCompleted { get; set; }
        public int CurrentLevel { get; set; }
        public int TotalLevels { get; set; }
        public int ChapterStars { get; set; }

        public override void ToViewModel<TViewModel>(TViewModel viewModel)
        {
            var chapterItemBindingViewModel = viewModel as ChapterItemBindingViewModel;
            if (chapterItemBindingViewModel)
            {
                chapterItemBindingViewModel.ChapterName = ChapterName;
                chapterItemBindingViewModel.ChapterNumber = ChapterNumber;
                chapterItemBindingViewModel.LevelsCompleted = LevelsCompleted;
                chapterItemBindingViewModel.CurrentLevel = CurrentLevel;
                chapterItemBindingViewModel.TotalLevels = TotalLevels;
                chapterItemBindingViewModel.ChapterProgress = $"{LevelsCompleted}/{TotalLevels}";
                chapterItemBindingViewModel.FirstChapterStar = ChapterStars >= 1 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
                chapterItemBindingViewModel.SecondChapterStar = ChapterStars >= 2 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
                chapterItemBindingViewModel.ThirdChapterStar = ChapterStars >= 3 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
            }
        }
    }
}