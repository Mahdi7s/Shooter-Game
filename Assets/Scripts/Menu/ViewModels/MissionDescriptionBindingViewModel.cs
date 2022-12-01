using System.Collections;
using DataServices;
using Infrastructure;
using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using System.Linq;
using Controllers;
using Hellmade.Sound;
using Models;
using UnityEngine;
using Utilities;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(LevelDescriptionController))]
    public class MissionDescriptionBindingViewModel : TrixViewModel<MissionDescriptionController>
    {
        private readonly TrixProp<string> _title = new TrixProp<string>();
        private readonly TrixProp<string> _target = new TrixProp<string>();
        private readonly TrixProp<string> _story = new TrixProp<string>();
        private readonly TrixProp<string> _achievementDescription = new TrixProp<string>();
        private readonly TrixProp<string> _remainingOrange = new TrixProp<string>();
        private readonly TrixProp<Animator> _orangeAnimator = new TrixProp<Animator>();
        private readonly TrixProp<bool> _showWarningPopup = new TrixProp<bool>();

        private bool _isNewLevel;
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
        public string RemainingOrange
        {
            get { return _remainingOrange.Value; }
            set { _remainingOrange.Value = value; }
        }
        public Animator OrangeAnimator
        {
            get { return _orangeAnimator.Value; }
            set { _orangeAnimator.Value = value; }
        }
        public bool ShowWarningPopup
        {
            get { return _showWarningPopup.Value; }
            set { _showWarningPopup.Value = value; }
        }
        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
            Controller.ShowWarningPopupAction = ShowWarningPopupAction;
        }

        private void Start()
        {
            OnStartLevelButtonClickEvent();
        }

        private void OnIsShownChanged(bool shown, object data)
        {
            if (shown)
            {
                TrixResource.AudioClips.LevelDescription_async(descriptionAudio =>
                {
                    if (descriptionAudio)
                    {
                        _audioId = TrixSoundManager.Instance.StartPlay(descriptionAudio, Audio.AudioType.UiVoice);
                    }
                });
                PlayerOrangeHandler.Instance.OrangeCountChanged = OrangeCountChanged;
                if (data == null)
                    _isNewLevel = true;
                RemainingOrange = PlayerOrangeHandler.Instance.OrangeCount.ToString();
                var currentLevel = GameDataService.Instance.GetMissionsInChapter(GameManager.Instance.ChapterMission.ChapterNumber).FirstOrDefault(x => x.MissionNumber == GameManager.Instance.ChapterMission.MissionNumber);
                currentLevel?.ToViewModel(this);
            }
        }
        private void ShowWarningPopupAction()
        {
            ShowWarningPopup = true;
        }
        private void OrangeCountChanged(int orangeCount)
        {
            RemainingOrange = orangeCount.ToString();
        }
        private IEnumerator WaitForAnimator()
        {
            yield return new WaitForSeconds(0.75f);
            RemainingOrange = PlayerOrangeHandler.Instance.OrangeCount.ToString();
            yield return new WaitForSeconds(1.25f);
            Controller.StartLevel();
            IsShown = false;
        }
        public void OnStartLevelButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 2);
            if (!_isNewLevel || PlayerOrangeHandler.Instance.TryUseOrange())
            {
                if (OrangeAnimator && _isNewLevel)
                {
                    OrangeAnimator.SetTrigger("Decrease");
                    StartCoroutine(WaitForAnimator());
                }
                else
                {
                    Controller.StartLevel();
                    IsShown = false;
                }
            }
            else
            {
                TrixResource.GameObjects.InGameNoOrangePopup_async(popupGameObject =>
                {
                    PopupController.Instance.ShowPopup(typeof(InGameNoOrangePopupController), popupGameObject, new PopupData());
                });
            }
        }
        public void OnWarningConfirmButtonClickEvent()
        {
            Controller.ExitChapterWithoutSaving();
        }
        public void OnWarningDenyButtonClickEvent()
        {
            ShowWarningPopup = false;
        }
        public void OnCloseButtonClickEvent()
        {
            OnBackPressed();
        }
        protected override void OnBackPressed()
        {
            if (IsShown && !ShowWarningPopup)
            {
                Controller.BackToMainMenu();
            }
        }
    }
}