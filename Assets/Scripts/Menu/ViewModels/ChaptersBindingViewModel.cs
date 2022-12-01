using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using System.Collections.Generic;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(ChaptersController))]
    public class ChaptersBindingViewModel : TrixViewModel<ChaptersController>
    {
        private readonly TrixProp<List<ChapterItemBindingViewModel>> _chaptersList = new TrixProp<List<ChapterItemBindingViewModel>>(new List<ChapterItemBindingViewModel>());

        public List<ChapterItemBindingViewModel> ChaptersList
        {
            get { return _chaptersList.Value; }
            set { _chaptersList.Value = value; }
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
                Controller.InitializeChaptersList(ChaptersList);
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