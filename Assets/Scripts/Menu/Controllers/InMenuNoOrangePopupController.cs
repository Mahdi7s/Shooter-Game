using Contracts;
using Controllers;
using Models;
using System;

namespace Menu.Controllers
{
    public class InMenuNoOrangePopupController : ControllerBase, IPopupController
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; } = true;
        public Action InitializePopupAction { get; set; }
        public void GoToShop()
        {
            ClosePopup();
            ShowView(typeof(ShopController));
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