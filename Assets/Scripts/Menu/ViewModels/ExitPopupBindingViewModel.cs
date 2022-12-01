using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(ExitPopupController))]*/
    public class ExitPopupBindingViewModel : TrixViewModel<ExitPopupController>
    {
        protected override void Awake()
        {
            base.Awake();
            Controller.InitializePopupAction = InitializePopup;
        }

        private void InitializePopup()
        {
            IsShown = true;
        }

        protected override void OnBackPressed()
        {
            Controller.ClosePopup();
        }

        public void OnExitButtonClickEvent()
        {
            Controller.ExitGame();
        }

        public void OnClosePopupClickEvent()
        {
            Controller.ClosePopup();
        }
    }
}