using System;
using Infrastructure;
using Messaging.Hub.Providers;
using Models.Constants;
using UnityEngine;

namespace Utilities
{
    public class OnBackPressedDetector : Singleton<OnBackPressedDetector>
    {
#if PLATFORM_ANDROID
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SimpleMessaging.Instance.SendMessage(new MessageData<Type, string>(GetType(), StaticValues.OnBackPressed), GameManager.Instance.ActiveControllerName);
            }
        }
#endif
    }
}
