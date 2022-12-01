using System;
using System.Collections.Generic;
using Controllers;
using DataServices;
using Infrastructure;
using Messaging.Hub.Providers;
using Messaging.MessageData;
using Models;
using Models.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Menu.Controllers
{
    public class EndGameBoardsController : ControllerBase
    {
        private AsyncOperation LoadSceneAsync { get; set; }
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        public BoardType BoardType { get; set; }
        public Action<int> ShowWarningPopupAction { get; set; }
        public Action<int> WinRewardAction { get; set; }
        public Action ShowPauseMenuAction { get; set; }
        public Action ShowTempAdvViewAction { get; set; }
        public Action HideViewAction { get; set; }
        public bool Timeout { get; set; }
        protected override void Awake()
        {
            base.Awake();
            SimpleMessaging.Instance.Register<GameplayMessageData>(this, data =>
            {
                if (data.Message.GameplayMessageType == GameplayMessageType.GamePaused)
                {
                    ShowPauseMenuAction?.Invoke();
                }
            });
        }
        public void SetWinReward(int chapterNumber, int missionNumber, int winReward)
        {
            GameDataService.Instance.TrySetWinReward(chapterNumber, missionNumber, winReward, rewarded =>
            {
                WinRewardAction?.Invoke(rewarded ? winReward : 0);
            });
        }
        public void CloseButtonClick()
        {
            GameManager.Instance.NeedSave((needSave, lastSavedMission) =>
            {
                if (BoardType != BoardType.EndChapter && needSave && !Timeout)
                {
                    ShowWarningPopupAction?.Invoke(lastSavedMission);
                }
                else
                {
                    ExitChapterWithoutSaving();
                    //HideViewAction?.Invoke();
                }
            });
        }
        public void RetryButtonClick()
        {
            if (BoardType != BoardType.Win)
            {
                AdvertiseController.Instance.ShowAdvertise(AdvertiseType.Video, AdvertiseAction.Retry, result =>
                {
                    if (result.IsSuccess)
                    {
                        AdvertiseSeenSuccessfully(result.AdvertiseAction);
                    }
                    else
                    {
                        AdvertiseSeenFailed(result.Message);
                    }
                });
                ShowTempAdvViewAction?.Invoke();
            }
            else
            {
                GameManager.Instance.ChapterMission.MissionNumber--;
                ShowView(typeof(MissionDescriptionController));
                HideViewAction?.Invoke();
            }
        }
        public void RetryCoinButtonClick()
        {
            
//                GameManager.Instance.ChapterMission.MissionNumber--;
                ShowView(typeof(MissionDescriptionController));
                HideViewAction?.Invoke();

            
        }
        public void ContinueButtonClick()
        {
            ShowView(typeof(MissionDescriptionController));
            HideViewAction?.Invoke();
        }
        public void ResumeButtonClick()
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData
                {
                    GameplayMessageType = GameplayMessageType.GameResumed
                }
            });
        }
        public void SaveProgressByAdvertise()
        {
            AdvertiseController.Instance.ShowAdvertise(AdvertiseType.Video, AdvertiseAction.Save, result =>
            {
                if (result.IsSuccess)
                {
                    AdvertiseSeenSuccessfully(result.AdvertiseAction);
                }
                else
                {
                    AdvertiseSeenFailed(result.Message);
                }
            });
            ShowTempAdvViewAction?.Invoke();
        }
        public void WarningConfirm()
        {
            ExitChapterWithoutSaving();
            //HideViewAction?.Invoke();
        }
        public void AdvertiseSeenSuccessfully(AdvertiseAction advertiseAction)
        {
            if (advertiseAction == AdvertiseAction.Save)
            {
                GameManager.Instance.SaveCurrentChapterProgress();
            }
            else
            {
                ShowView(typeof(MissionDescriptionController));
                HideViewAction?.Invoke();
            }
        }
        public void AdvertiseSeenFailed(string error)
        {
            TrixResource.GameObjects.InfoPopup_async(popupGameObject =>
            {
                PopupController.Instance.ShowPopup(typeof(InfoPopupController), popupGameObject, new PopupData { Data = "خطا در نمایش ویدئو تبلیغاتی" });
            });
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