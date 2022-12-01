using System;
using Contracts;
using Controllers;
using Models;

namespace Menu.Controllers
{
    public class InfoPopupController : ControllerBase, IPopupController
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; } = true;
        public Action<string> InitializePopupAction { get; set; }
        public void InitializePopup(PopupData popupData)
        {
            InitializePopupAction?.Invoke(popupData?.Data);
        }
        public void ClosePopup()
        {
            PopupController.Instance.ClosePopup(GetType());
        }
    }
}