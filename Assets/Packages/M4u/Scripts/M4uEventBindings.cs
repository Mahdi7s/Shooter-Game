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
using System.Reflection;

namespace M4u
{
    /// <summary>
	/// M4uEventBindings. Bind UnityEvent
    /// </summary>
	[AddComponentMenu("M4u/EventBindings")]
	public class M4uEventBindings : M4uBindingMultiple
	{
		public M4uEventType[] Type;
		public EventTriggerType[] TriggerType;

		public override void Start ()
		{
			base.Start ();

			if (Path != null && Path.Length > 0)
			{
				for (int i = 0; i < Path.Length; i++)
				{
					string[] names = Path[i].Split('.');
					string name = "";
					object parent = Root.Context;
					object value = null;
					object obj = null;
					PropertyInfo pi = null;
					FieldInfo fi = null;
					for (int j = 0; j < names.Length; j++)
					{
						bool isLast = (j == names.Length - 1);
						name = names[j];
						ParseMember(isLast, ref name, ref parent, ref value, ref obj, ref pi, ref fi);
					}
					M4uEventBinding.SetEvent(this, Type[i], TriggerType[i], parent, name);
				}
			}
		}

        public override string ToString()
        {
			string value = "";
			for (int i = 0; i < Type.Length; i++)
			{
				if(value != "") { value += "/"; }
				value += Type[i].ToString() + "=" + GetBindStr(Path[i]);
			}
			return value;
        }
	}
}