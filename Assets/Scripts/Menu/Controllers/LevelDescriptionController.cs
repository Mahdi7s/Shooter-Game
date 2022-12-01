using Controllers;
using DataServices;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Menu.Controllers
{
    public class LevelDescriptionController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; } = true;
        protected internal override bool IsPopup { get; set; }
        private AsyncOperation LoadSceneAsync { get; set; }
        public void StartLevel()
        {
            if (PlayerOrangeHandler.Instance.TryUseOrange())
            {
                GameDataService.Instance.SetTempMissionProgresses();
                if (LoadSceneAsync == null)
                {
                    LoadSceneAsync = SceneManager.LoadSceneAsync(Scenes.scn_Gameplay.ToString());
                }
            }
            else
            {
                TrixResource.GameObjects.InMenuNoOrangePopup_async(popupGameObject =>
                {
                    PopupController.Instance.ShowPopup(typeof(InMenuNoOrangePopupController), popupGameObject, new PopupData());
                });
            }
        }
    }
}