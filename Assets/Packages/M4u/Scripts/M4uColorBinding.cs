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
    /// M4uColorBinding. Bind Color
    /// </summary>
    [AddComponentMenu("M4u/ColorBinding")]
    public class M4uColorBinding : M4uBindingSingle
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

            SetColor(ui, (Color)Values[0]);
		}

        private void SetColor(Transform t, Color color)
        {
            var g = t.GetComponent<Graphic>();
            if (g != null) { g.color = color; }

            for(int i = 0; i < t.childCount; i++)
            {
                SetColor(t.GetChild(i), color);
            }
        }

        public override string ToString()
        {
            return "Graphic.color=" + GetBindStr(Path);
        }
	}
}