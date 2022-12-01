using Menu.ViewModels;
using Packages.M4u.Scripts.Trixmen;
using System.Collections.Generic;
using DataServices;

namespace Menu.Controllers
{
    public class ChaptersController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }

        public void InitializeChaptersList(List<ChapterItemBindingViewModel> chapterItems)
        {
            GameDataService.Instance.GetChapters(chapters =>
            {
                chapters.ToViewModels(chapterItems, gameObject, typeof(ChapterItemBindingViewModel));
            });
        }
    }
}