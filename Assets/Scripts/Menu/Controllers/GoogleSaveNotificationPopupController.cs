using Controllers;
using Models;
using System;
using Contracts;
using UnityEngine.SceneManagement;
using Utilities;

namespace Menu.Controllers
{
    public class GoogleSaveNotificationPopupController : ControllerBase, IPopupController
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; } = true;
        public Action InitializePopupAction { get; set; }
        public void ConnectGoogle()
        {
            ClosePopup();
            GooglePlayController.Instance.TryLogin(succeed =>
            {
                if (succeed)
                {
                    SceneManager.LoadSceneAsync(Scenes.scn_Loading.ToString());
                }
                else
                {
                    TrixResource.GameObjects.InfoPopup_async(popupGameObject =>
                    {
                        PopupController.Instance.ShowPopup(typeof(InfoPopupController), popupGameObject, new PopupData { Data = "خطا در اتصال به گوگل" });
                    });
                }
            });
        }

        public void ClosePopup()
        {
            PopupController.Instance.ClosePopup(GetType());
        }

        public void InitializePopup(PopupData popupData)
        {
            InitializePopupAction?.Invoke();
        }
    }
}