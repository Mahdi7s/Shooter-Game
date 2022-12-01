using Controllers;
using Infrastructure;
using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;
using Utilities;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(SettingsPopupController))]*/
    public class SettingsPopupBindingViewModel : TrixViewModel<SettingsPopupController>
    {
        private readonly TrixProp<float> _sensitivity = new TrixProp<float>();
        private readonly TrixProp<Sprite> _musicButtonBackground = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _musicButtonIcon = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _sfxButtonBackground = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _sfxButtonIcon = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _googleButtonBackground = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _googleButtonIcon = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _notificationButtonBackground = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _notificationButtonIcon = new TrixProp<Sprite>();
        public float Sensitivity
        {
            get { return _sensitivity.Value; }
            set { _sensitivity.Value = value; }
        }
        public Sprite MusicButtonBackground
        {
            get { return _musicButtonBackground.Value; }
            set { _musicButtonBackground.Value = value; }
        }
        public Sprite MusicButtonIcon
        {
            get { return _musicButtonIcon.Value; }
            set { _musicButtonIcon.Value = value; }
        }
        public Sprite SfxButtonBackground
        {
            get { return _sfxButtonBackground.Value; }
            set { _sfxButtonBackground.Value = value; }
        }
        public Sprite SfxButtonIcon
        {
            get { return _sfxButtonIcon.Value; }
            set { _sfxButtonIcon.Value = value; }
        }
        public Sprite GoogleButtonBackground
        {
            get { return _googleButtonBackground.Value; }
            set { _googleButtonBackground.Value = value; }
        }
        public Sprite GoogleButtonIcon
        {
            get { return _googleButtonIcon.Value; }
            set { _googleButtonIcon.Value = value; }
        }
        public Sprite NotificationButtonBackground
        {
            get { return _notificationButtonBackground.Value; }
            set { _notificationButtonBackground.Value = value; }
        }
        public Sprite NotificationButtonIcon
        {
            get { return _notificationButtonIcon.Value; }
            set { _notificationButtonIcon.Value = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            Controller.InitializePopupAction = InitializePopup;
            MusicButtonBackground = GameManager.Instance.NoImageSprite;
            MusicButtonIcon = GameManager.Instance.NoImageSprite;
            SfxButtonBackground = GameManager.Instance.NoImageSprite;
            SfxButtonIcon = GameManager.Instance.NoImageSprite;
            GoogleButtonBackground = GameManager.Instance.NoImageSprite;
            GoogleButtonIcon = GameManager.Instance.NoImageSprite;
            NotificationButtonBackground = GameManager.Instance.NoImageSprite;
            NotificationButtonIcon = GameManager.Instance.NoImageSprite;

            Sensitivity = GameManager.Instance.GameSettings.Sensitivity;
            CheckSettings();
        }
        private void InitializePopup()
        {
            IsShown = true;
        }

        private void CheckSettings()
        {
            if (GameManager.Instance.GameSettings.Music)
            {
                MusicButtonBackground = TrixResource.Sprites.spr_BlueButton;
                MusicButtonIcon = TrixResource.Sprites.spr_EnabledMusic;
            }
            else
            {
                MusicButtonBackground = TrixResource.Sprites.spr_RedButton;
                MusicButtonIcon = TrixResource.Sprites.spr_DisabledMusic;
            }

            if (GameManager.Instance.GameSettings.Sfx)
            {
                SfxButtonBackground = TrixResource.Sprites.spr_BlueButton;
                SfxButtonIcon = TrixResource.Sprites.spr_EnabledSfx;
            }
            else
            {
                SfxButtonBackground = TrixResource.Sprites.spr_RedButton;
                SfxButtonIcon = TrixResource.Sprites.spr_DisabledSfx;
            }

            if (GameManager.Instance.GameSettings.Notification)
            {
                NotificationButtonBackground = TrixResource.Sprites.spr_BlueButton;
                NotificationButtonIcon = TrixResource.Sprites.spr_EnabledNotification;
            }
            else
            {
                NotificationButtonBackground = TrixResource.Sprites.spr_RedButton;
                NotificationButtonIcon = TrixResource.Sprites.spr_DisabledNotification;
            }

            if (GameManager.Instance.CanUseGooglePlay)
            {
                if (GooglePlayController.Instance.LoggedIn)
                {
                    GoogleButtonBackground = TrixResource.Sprites.spr_BlueButton;
                    GoogleButtonIcon = TrixResource.Sprites.spr_Connected;
                }
                else
                {
                    GoogleButtonBackground = TrixResource.Sprites.spr_RedButton;
                    GoogleButtonIcon = TrixResource.Sprites.spr_Disconnected;
                }
            }
            else
            {
                GoogleButtonBackground = TrixResource.Sprites.spr_GrayButton;
                GoogleButtonIcon = TrixResource.Sprites.spr_Disconnected;
            }

        }
        public void OnSensitivitySliderValueChangedEvent(float value)
        {
            Controller.ChangeSensitivity(value);
        }
        public void OnMusicButtonClickEvent()
        {
            Controller.ToggleMusic();
            CheckSettings();
        }
        public void OnSfxButtonClickEvent()
        {
            Controller.ToggleSfx();
            CheckSettings();
        }
        public void OnGoogleButtonClickEvent()
        {
            if (GameManager.Instance.CanUseGooglePlay)
            {
                Controller.GoogleButtonClick(result =>
                {
                    if (!result)
                    {
                        CheckSettings();
                    }
                });
            }
        }
        public void OnNotificationButtonClickEvent()
        {
            Controller.ToggleNotification();
            CheckSettings();
        }
        public void OnSupportButtonClickEvent()
        {
            Controller.SupportButtonClick();
        }
        public void OnCloseButtonClickEvent()
        {
            Controller.ClosePopup();
        }
        protected override void OnBackPressed()
        {
            Controller.ClosePopup();
        }
    }
}