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
    /// M4uRawImageBinding. Bind RawImage
    /// </summary>
	[AddComponentMenu("M4u/RawImageBinding")]
	public class M4uRawImageBinding : M4uBindingSingle
	{
		private RawImage ui = null;

		public override void Start ()
		{
			base.Start ();

			ui = GetComponent<RawImage> ();
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			ui.texture = Values [0] as Texture;
		}

        public override string ToString()
        {
            return "RawImage.texture=" + GetBindStr(Path);
        }
	}
}