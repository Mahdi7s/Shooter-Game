using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(InfoPopupController))]*/
    public class InfoPopupBindingViewModel : TrixViewModel<InfoPopupController>
    {
        private readonly TrixProp<string> _message = new TrixProp<string>();

        public string Message
        {
            get { return _message.Value; }
            set { _message.Value = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            Controller.InitializePopupAction = InitializePopup;
        }

        private void InitializePopup(string message)
        {
            Message = message;
            IsShown = true;
        }

        public void OnConfirmButtonClickEvent()
        {
            Controller.ClosePopup();
        }
        protected override void OnBackPressed()
        {
        }
    }
}