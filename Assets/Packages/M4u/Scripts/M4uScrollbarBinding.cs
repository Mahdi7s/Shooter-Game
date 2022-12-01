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
    /// M4uScrollbarBinding. Bind Scrollbar
    /// </summary>
	[AddComponentMenu("M4u/ScrollbarBinding")]
	public class M4uScrollbarBinding : M4uBindingSingle
	{
		private Scrollbar ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<Scrollbar> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			ui.value = float.Parse (Values [0].ToString ());
		}

        public override string ToString()
        {
            return "Scrollbar.value=" + GetBindStr(Path);
        }
	}
}