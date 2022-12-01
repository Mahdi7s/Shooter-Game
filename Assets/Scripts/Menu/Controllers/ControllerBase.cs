using Messaging.Hub.Providers;
using Models.Constants;
using System;
using Hellmade.Sound;
using Infrastructure;
using Messaging.MessageData;
using UnityEngine;
using Utilities;

namespace Menu.Controllers
{
    public abstract class ControllerBase : MonoBehaviour
    {
        protected internal abstract bool IsSingleView { get; set; }
        protected internal abstract bool IsPopup { get; set; }
        public Action OnBackPressed { get; set; }
        public Action<bool> OnIsShownChangedDefaultAction { get; set; }
        public Action<bool, object> OnIsShownChanged { get; set; }
        protected virtual void Awake()
        {
            SimpleMessaging.Instance.Register<Type>(this, message =>
            {
                if (message.Command.Equals(StaticValues.OnBackPressed, StringComparison.Ordinal) && (IsSingleView || IsPopup))
                {
                    OnBackPressed();
                }
            }, GetType().Name);
            SimpleMessaging.Instance.Register<ShowViewMessageData>(this, message =>
             {
                 if (string.Equals(message.Command, StaticValues.ShowViewCommand, StringComparison.Ordinal) && !IsPopup)
                 {
                     if (GetType() == message.Message.ViewModelType)
                     {
                         OnIsShownChangedDefaultAction?.Invoke(true);
                         OnIsShownChanged?.Invoke(true, message.Message.ViewModelData);
                     }
                     else if (IsSingleView)
                     {
                         OnIsShownChangedDefaultAction?.Invoke(false);
                     }
                 }
                 else if (string.Equals(message.Command, StaticValues.DisplayViewCommand, StringComparison.Ordinal) && !IsPopup)
                 {
                     if (GetType() == message.Message.ViewModelType)
                     {
                         OnIsShownChangedDefaultAction?.Invoke(true);
                         OnIsShownChanged?.Invoke(true, message.Message.ViewModelData);
                     }
                 }
                 else if (string.Equals(message.Command, StaticValues.HideViewCommand, StringComparison.Ordinal) && !IsPopup)
                 {
                     if (GetType() == message.Message.ViewModelType)
                     {
                         OnIsShownChangedDefaultAction?.Invoke(false);
                         OnIsShownChanged?.Invoke(false, message.Message.ViewModelData);
                     }
                 }
             });
        }

        private void OnDestroy()
        {
            SimpleMessaging.Instance.UnRegister<Type>(this);
            SimpleMessaging.Instance.UnRegister<ShowViewMessageData>(this);
        }

        public void ShowView(Type controllerType, object data = null, bool hideOtherViews = true)
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
            {
                Command = hideOtherViews ? StaticValues.ShowViewCommand : StaticValues.DisplayViewCommand,
                Message = new ShowViewMessageData { ViewModelType = controllerType, ViewModelData = data }
            });

          
        }

        public void ShowView(Type controllerType, bool backState, object data = null, bool hideOtherViews = true)
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
            {
                Command = hideOtherViews ? StaticValues.ShowViewCommand : StaticValues.DisplayViewCommand,
                Message = new ShowViewMessageData { ViewModelType = controllerType, ViewModelData = data }
            });

            if (backState && (controllerType == typeof(MainMenuController) || controllerType == typeof(ChaptersController)))
            {
                TrixResource.AudioClips.Menu_async(mainMenu =>
                {
                    if (mainMenu)
                    {
                        TrixSoundManager.Instance.StartPlay(mainMenu, Audio.AudioType.Music, loop: true, volume: 0.6f);
                    }
                });
            }
        }
        public void HideView(Type controllerType, object data = null)
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
            {
                Command = StaticValues.HideViewCommand,
                Message = new ShowViewMessageData { ViewModelType = controllerType, ViewModelData = data }
            });
        }
    }
}
