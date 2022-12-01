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
    /// M4uImageBinding. Bind Image
    /// </summary>
	[AddComponentMenu("M4u/ImageBinding")]
	public class M4uImageBinding : M4uBindingSingle
	{
		private Image ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<Image> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			ui.sprite = Values[0] as Sprite;
		}

        public override string ToString()
        {
            return "Image.sprite=" + GetBindStr(Path);
        }
	}
}