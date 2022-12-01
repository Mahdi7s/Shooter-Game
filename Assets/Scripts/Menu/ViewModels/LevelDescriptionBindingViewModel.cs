using DataServices;
using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using System.Linq;
using Hellmade.Sound;
using Infrastructure;
using Models;
using Utilities;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(LevelDescriptionController))]
    public class LevelDescriptionBindingViewModel : TrixViewModel<LevelDescriptionController>
    {
        private readonly TrixProp<string> _title = new TrixProp<string>();
        private readonly TrixProp<string> _target = new TrixProp<string>();
        private readonly TrixProp<string> _story = new TrixProp<string>();
        private readonly TrixProp<string> _achievementDescription = new TrixProp<string>();
        private int _audioId;
        public string Title
        {
            get { return _title.Value; }
            set { _title.Value = value; }
        }
        public string Target
        {
            get { return _target.Value; }
            set { _target.Value = value; }
        }
        public string Story
        {
            get { return _story.Value; }
            set { _story.Value = value; }
        }
        public string AchievementDescription
        {
            get { return _achievementDescription.Value; }
            set { _achievementDescription.Value = value; }
        }
        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
        }

        private void OnIsShownChanged(bool shown, object data)
        {
            var chapterAndLevel = data as ChapterMission;
            if (chapterAndLevel != null)
            {
                var currentLevel = GameDataService.Instance.GetLevelsInChapter(chapterAndLevel.ChapterNumber).FirstOrDefault(x => x.LevelNumber == chapterAndLevel.MissionNumber);
                currentLevel?.ToViewModel(this);
                GameManager.Instance.ChapterMission = chapterAndLevel;
            }

            if (shown)
            {
                TrixResource.AudioClips.LevelDescription_async(descriptionAudio =>
                {
                    if (descriptionAudio)
                    {
                        _audioId = TrixSoundManager.Instance.StartPlay(descriptionAudio, Audio.AudioType.Music);
                    }
                });
            }
            else
            {
                TrixSoundManager.Instance.StopPlay(_audioId, 2);
            }
        }
        protected override void OnBackPressed()
        {
            if (IsShown)
            {
                Controller.ShowView(typeof(ChaptersController),true);
            }
        }

        public void OnStartLevelButtonClickEvent()
        {
            Controller.StartLevel();
        }
    }
}