using Infrastructure;
using M4u;
using Menu.Controllers;
using Menu.ViewModels;
using Messaging.Hub.Providers;
using Models.Constants;
using UnityEngine;

namespace Packages.M4u.Scripts.Trixmen
{
    public abstract class TrixViewModel<TController> : ViewModelBase, ITrixContext, M4uContextInterface where TController : ControllerBase
    {
        [SerializeField] private TController _controller;

        public TController Controller
        {
            get { return _controller; }
            set { _controller = value; }
        }

        // ReSharper disable once InconsistentNaming
        protected TrixProp<bool> _isShown = new TrixProp<bool>(false);
        public bool IsShown
        {
            get { return _isShown.Value; }
            set
            {
                _isShown.Value = value;
                if (value && Controller.IsSingleView && !Controller.IsPopup)
                {
                    GameManager.Instance.ActiveControllerName = Controller.GetType().Name;
                }
            }
        }
        protected virtual void Awake()
        {
            if (!Controller)
                Controller = IsListView ? gameObject.AddComponent<TController>() : GetComponent<TController>();

            Controller.OnBackPressed = OnBackPressed;
            Controller.OnIsShownChangedDefaultAction = shown =>
            {
                IsShown = shown;
                if (GetType() == typeof(MainMenuBindingViewModel))
                {
                    SimpleMessaging.Instance.SendMessage(new MessageData<bool, string>(shown, StaticValues.MainMenuShownChanged), typeof(HudBindingViewModel).Name);
                }
            };
        }
        private void Reset()
        {
            if (!Controller)
                Controller = IsListView ? gameObject.AddComponent<TController>() : GetComponent<TController>();
        }

        private void OnDestroy()
        {
            Destroy(Controller);
        }

        protected abstract void OnBackPressed();
    }
}
