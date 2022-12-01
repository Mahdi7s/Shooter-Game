using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// TrixM4uColorBinding. Bind Color without child color bind
    /// </summary>
    [AddComponentMenu("M4u/TrixColorBinding")]
    public class TrixM4uColorBinding : M4uBindingSingle
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
        }

        public override string ToString()
        {
            return "Graphic.color=" + GetBindStr(Path);
        }
	}
}