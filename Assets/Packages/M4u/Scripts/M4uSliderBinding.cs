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
    /// M4uSliderBinding. Bind Slider
    /// </summary>
	[AddComponentMenu("M4u/SliderBinding")]
	public class M4uSliderBinding : M4uBindingSingle
	{
		private Slider ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<Slider> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			ui.value = float.Parse (Values [0].ToString ());
		}

        public override string ToString()
        {
            return "Slider.value=" + GetBindStr(Path);
        }
	}
}