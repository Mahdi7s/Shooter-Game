//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace M4u
{
    /// <summary>
    /// M4uEventBinding. Bind UnityEvent
    /// </summary>
    [AddComponentMenu("M4u/EventBinding")]
    public class M4uEventBinding : M4uBindingSingle
    {
        public M4uEventType Type;
        public EventTriggerType TriggerType;

        public override void Start()
        {
            base.Start();

            if (!string.IsNullOrEmpty(Path))
            {
                string tmpPath = Path.StartsWith("★★★ Collection Bindings") ? Path.Split('.').Last() : Path;
                string[] names = tmpPath.Split('.');
                string name = "";
                object parent = Root.Context;
                object value = null;
                object obj = null;
                PropertyInfo pi = null;
                FieldInfo fi = null;
                for (int i = 0; i < names.Length; i++)
                {
                    bool isLast = (i == names.Length - 1);
                    name = names[i];
                    ParseMember(isLast, ref name, ref parent, ref value, ref obj, ref pi, ref fi);
                }
                SetEvent(this, Type, TriggerType, parent, name);
            }
        }

        public static void SetEvent(MonoBehaviour m, M4uEventType type, EventTriggerType triggerType, object obj, string name)
        {
            if (type == M4uEventType.ButtonClick)
            {
                UI<Button>(m).onClick.AddListener((UnityAction)Action<UnityAction>(obj, name));
            }
            else if (type == M4uEventType.ToggleValueChanged)
            {
                UI<Toggle>(m).onValueChanged.AddListener((UnityAction<bool>)Action<UnityAction<bool>>(obj, name));
            }
            else if (type == M4uEventType.SliderValueChanged)
            {
                UI<Slider>(m).onValueChanged.AddListener((UnityAction<float>)Action<UnityAction<float>>(obj, name));
            }
            else if (type == M4uEventType.ScrollbarValueChanged)
            {
                UI<Scrollbar>(m).onValueChanged.AddListener((UnityAction<float>)Action<UnityAction<float>>(obj, name));
            }
            else if (type == M4uEventType.DropdownValueChanged)
            {
#if !(UNITY_5_0 || UNITY_5_1)
                UI<Dropdown>(m).onValueChanged.AddListener((UnityAction<int>)Action<UnityAction<int>>(obj, name));
#endif
            }
            else if (type == M4uEventType.InputFieldEndEdit)
            {
                UI<InputField>(m).onEndEdit.AddListener((UnityAction<string>)Action<UnityAction<string>>(obj, name));
            }
            //HACK : Added by sina tahoori to enable value changed listener 
            else if(type == M4uEventType.InputFieldValueChanged)
            {
                UI<InputField>(m).onValueChanged.AddListener((UnityAction<string>)Action<UnityAction<string>>(obj, name));
            }
            else if (type == M4uEventType.ScrollRectValueChanged)
            {
                UI<ScrollRect>(m).onValueChanged.AddListener((UnityAction<Vector2>)Action<UnityAction<Vector2>>(obj, name));
            }
            else if (type == M4uEventType.EventTrigger)
            {
                var e = new EventTrigger.Entry();
                e.eventID = triggerType;
                e.callback.AddListener((UnityAction<BaseEventData>)Action<UnityAction<BaseEventData>>(obj, name));

                var trigger = UI<EventTrigger>(m);
#if UNITY_5_0
				if(trigger.delegates == null) { trigger.delegates = new List<EventTrigger.Entry>(); }
				trigger.delegates.Add(e);
#else
                if (trigger.triggers == null) { trigger.triggers = new List<EventTrigger.Entry>(); }
                trigger.triggers.Add(e);
#endif
            }
        }

        private static T UI<T>(MonoBehaviour m) where T : Component
        {
            return m.GetComponent<T>();
        }

        private static Delegate Action<T>(object obj, string name)
        {
            return Delegate.CreateDelegate(typeof(T), obj, name);
        }

        public override string ToString()
        {
            return Type.ToString() + "=" + GetBindStr(Path);
        }
    }
}