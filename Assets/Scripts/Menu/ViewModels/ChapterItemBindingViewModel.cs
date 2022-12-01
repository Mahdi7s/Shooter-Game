using Infrastructure;
using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;
using Utilities;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(ChapterItemController))]
    public class ChapterItemBindingViewModel : TrixViewModel<ChapterItemController>
    {
        private readonly TrixProp<string> _chapterName = new TrixProp<string>();
        private readonly TrixProp<int> _chapterNumber = new TrixProp<int>();
        private readonly TrixProp<int> _levelsCompleted = new TrixProp<int>();
        private readonly TrixProp<int> _currentLevel = new TrixProp<int>();
        private readonly TrixProp<int> _totalLevels = new TrixProp<int>();
        private readonly TrixProp<string> _chapterProgress = new TrixProp<string>();
        private readonly TrixProp<Sprite> _firstChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _secondChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _thirdChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<_2dxFX_GrayScale> _grayScale = new TrixProp<_2dxFX_GrayScale>();

        public string ChapterName
        {
            get { return _chapterName.Value; }
            set { _chapterName.Value = value; }
        }
        public int ChapterNumber
        {
            get { return _chapterNumber.Value; }
            set { _chapterNumber.Value = value; }
        }

        public int LevelsCompleted
        {
            get { return _levelsCompleted.Value; }
            set
            {
                _levelsCompleted.Value = value;
                ChapterProgress = $"{LevelsCompleted}/{TotalLevels}";
            }
        }
        public int CurrentLevel
        {
            get { return _currentLevel.Value; }
            set { _currentLevel.Value = value; }
        }

        public int TotalLevels
        {
            get { return _totalLevels.Value; }
            set
            {
                _totalLevels.Value = value;
                ChapterProgress = $"{LevelsCompleted}/{TotalLevels}";
            }
        }

        public string ChapterProgress
        {
            get { return _chapterProgress.Value; }
            set { _chapterProgress.Value = value; }
        }
        public Sprite FirstChapterStar
        {
            get { return _firstChapterStar.Value; }
            set { _firstChapterStar.Value = value; }
        }
        public Sprite SecondChapterStar
        {
            get { return _secondChapterStar.Value; }
            set { _secondChapterStar.Value = value; }
        }
        public Sprite ThirdChapterStar
        {
            get { return _thirdChapterStar.Value; }
            set { _thirdChapterStar.Value = value; }
        }
        public _2dxFX_GrayScale GrayScale
        {
            get { return _grayScale.Value; }
            set
            {
                _grayScale.Value = value;
                if (value)
                {
                    value.enabled = !(GameManager.Instance.PlayerSaveData.HighestChapterFinished >= ChapterNumber - 1);
                }
            }
        }
        protected override void Awake()
        {
            IsListView = true;
            FirstChapterStar = TrixResource.Sprites.spr_NoImage;
            SecondChapterStar = TrixResource.Sprites.spr_NoImage;
            ThirdChapterStar = TrixResource.Sprites.spr_NoImage;
            base.Awake();
        }
        public void OnChapterClickEvent()
        {
#if UNITY_EDITOR || UNITY_EDITOR_64
            Controller.ShowChapterCurrentLevelDescription(ChapterNumber, CurrentLevel);
#else
            if (GameManager.Instance.PlayerSaveData.HighestChapterFinished >= ChapterNumber - 1)
            {
                Controller.ShowChapterCurrentLevelDescription(ChapterNumber, CurrentLevel);
            }
#endif
        }
        protected override void OnBackPressed()
        {
            //DoNothing
        }
    }
}