using System;
using System.Collections.Generic;
using Infrastructure;
using Messaging.Hub.Providers;
using Messaging.MessageData;
using Models;
using Models.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Controllers
{
    public class MissionDescriptionController : ControllerBase
    {
        private AsyncOperation LoadSceneAsync { get; set; }
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        public Action ShowWarningPopupAction { get; set; }
        public void StartLevel()
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData
                {
                    GameplayMessageType = GameplayMessageType.MissionStarted,
                }
            }, StaticValues.MissionController);
        }

        public void BackToMainMenu()
        {
            ShowWarningPopupAction?.Invoke();
        }
        public void ExitChapterWithoutSaving()
        {
            GameManager.Instance.MissionProgresses = new List<MissionProgress>();
            if (LoadSceneAsync == null)
            {
                LoadSceneAsync = SceneManager.LoadSceneAsync(Scenes.scn_Menu.ToString());
            }
        }
    }
}