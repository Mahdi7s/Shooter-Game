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
    /// M4uInputFieldBindings. Bind InputField
    /// </summary>
	[AddComponentMenu("M4u/InputFieldBindings")]
	public class M4uInputFieldBindings : M4uBindingMultiple
	{
		public string Format = "";

		private InputField ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<InputField> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();
				
			ui.text = string.Format(Format, Values);
		}

        public override string ToString()
        {
            string str = "InputField.text=";
            if(Path != null && Path.Length > 0)
            {
                str += string.Format(Format, GetBindStrs(Path));
            }
            return str;
        }
	}
}