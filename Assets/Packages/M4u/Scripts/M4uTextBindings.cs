//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace M4u
{
    /// <summary>
    /// M4uTextBindings. Bind Text
    /// </summary>
	[AddComponentMenu("M4u/TextBindings")]
	public class M4uTextBindings : M4uBindingMultiple
	{
		public string Format = "";

		private Text ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<Text> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();
				
			ui.text = string.Format (Format, Values);
		}

        public override string ToString()
        {
            string str = "Text.text=";
            if (Path != null && Path.Length > 0)
            {
                str += string.Format(Format, GetBindStrs(Path));
            }
            return str;
        }
	}
}