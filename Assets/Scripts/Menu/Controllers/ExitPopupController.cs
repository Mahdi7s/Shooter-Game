using System;
using Contracts;
using Controllers;
using Models;
using UnityEngine;

namespace Menu.Controllers
{
    public class ExitPopupController : ControllerBase, IPopupController
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; } = true;
        public Action InitializePopupAction { get; set; }
        public void ExitGame()
        {
            Application.Quit();
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