//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// M4uEnableBinding. Bind Behaviour.enabled
    /// </summary>
	[AddComponentMenu("M4u/EnableBinding")]
	public class M4uEnableBinding : M4uBindingBool
	{
		private Transform ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = transform;
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			SetEnable (ui);
		}

		private void SetEnable(Transform t)
		{
			foreach (var value in t.GetComponents<MonoBehaviour> ())
			{
				value.enabled = IsCheck();
			}
			for (int i = 0; i < t.childCount; i++) 
			{
				SetEnable(t.GetChild(i));
			}
		}

        public override string ToString()
        {
            return "Transform.enabled=" + base.ToString();
        }
	}
}