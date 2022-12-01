using Infrastructure;
using Models;

namespace Menu.Controllers
{
    public class ChapterItemController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; }

        public void ShowChapterCurrentLevelDescription(int chapterNumber, int levelNumber)
        {
            ShowView(typeof(LevelDescriptionController), new ChapterMission(chapterNumber, levelNumber));
        }
    }
}