using Contracts;
using Controllers;
using DataServices;
using Infrastructure;
using Models;
using Models.Constants;
using Newtonsoft.Json;
using System;
using Hellmade.Sound;
using UnityEngine;
using Utilities;

namespace Menu.Controllers
{
    public class SettingsPopupController : ControllerBase, IPopupController
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; } = true;
        public Action InitializePopupAction { get; set; }
        public void InitializePopup(PopupData popupData)
        {
            InitializePopupAction?.Invoke();
        }

        public void ChangeSensitivity(float value)
        {
            GameManager.Instance.GameSettings.SetSensitivity(value);
        }
        public void ToggleMusic()
        {
            GameManager.Instance.GameSettings.ToggleMusic();

            TrixSoundManager.Instance.SetConfig(GameManager.Instance.GameSettings.Music, GameManager.Instance.GameSettings.Sfx);

            if (!GameManager.Instance.GameSettings.Music)
                TrixSoundManager.Instance.StopAllMusic(1f);
            else
            {
                TrixResource.AudioClips.Menu_async(mainMenu =>
                {
                    if (mainMenu)
                    {
                        TrixSoundManager.Instance.StartPlay(mainMenu, Audio.AudioType.Music, loop: true, volume: 0.6f);
                    }
                });
            }

            var settingsString = JsonConvert.SerializeObject(GameManager.Instance.GameSettings);
            ES3.Save<string>(StaticValues.GameSettings, settingsString);
        }
        public void ToggleSfx()
        {
            GameManager.Instance.GameSettings.ToggleSfx();

            TrixSoundManager.Instance.SetConfig(GameManager.Instance.GameSettings.Music, GameManager.Instance.GameSettings.Sfx);

            if (!GameManager.Instance.GameSettings.Sfx)
            {
                TrixSoundManager.Instance.StopAllSounds();
                TrixSoundManager.Instance.StopAllUiSounds();
            }

            var settingsString = JsonConvert.SerializeObject(GameManager.Instance.GameSettings);
            ES3.Save<string>(StaticValues.GameSettings, settingsString);
        }
        public void ToggleNotification()
        {
            GameManager.Instance.GameSettings.ToggleNotification();
            var settingsString = JsonConvert.SerializeObject(GameManager.Instance.GameSettings);
            ES3.Save<string>(StaticValues.GameSettings, settingsString);
        }
        public void GoogleButtonClick(Action<bool> callback)
        {
            if (GooglePlayController.Instance.LoggedIn)
            {
                GooglePlayController.Instance.Logout();
                GameManager.Instance.PreventGoogleSave = true;
                ES3.Save<bool>(StaticValues.PreventGoogleSave, true);
                callback(false);
            }
            else
            {
                GooglePlayController.Instance.TryLogin(succeed =>
                {
                    if (succeed)
                    {
                        GameManager.Instance.PreventGoogleSave = false;
                        ES3.Save<bool>(StaticValues.PreventGoogleSave, false);
                        GameDataService.Instance.SaveProgress();
                        callback(true);
                    }
                    else
                    {
                        callback(false);
                    }
                });
            }
        }

        public void SupportButtonClick()
        {
            Application.OpenURL(StaticValues.TelegramSupport);
        }
        public void ClosePopup()
        {
            var settingsString = JsonConvert.SerializeObject(GameManager.Instance.GameSettings);
            ES3.Save<string>(StaticValues.GameSettings, settingsString);
            PopupController.Instance.ClosePopup(GetType());
        }
    }
}