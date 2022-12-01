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
    /// M4uTransformLocalBinding. Bind Transform
    /// </summary>
	[AddComponentMenu("M4u/TransformLocalBinding")]
	public class M4uTransformLocalBinding : M4uBindingSingle
	{
		public TransformLocalType Type = TransformLocalType.Postion;

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

			var value = (Vector3)Values [0];
			switch (Type)
			{
				case TransformLocalType.Postion:
					ui.localPosition = value;
					break;
				case TransformLocalType.Rotation:
					ui.localRotation = Quaternion.Euler (value);
					break;
				case TransformLocalType.Scale:
					ui.localScale = value;
					break;
			}
		}

        public override string ToString()
        {
            switch (Type)
            {
                case TransformLocalType.Postion:
                    return "Transform.localPosition=" + GetBindStr(Path);
                case TransformLocalType.Rotation:
                    return "Transform.localRotation=" + GetBindStr(Path);
                case TransformLocalType.Scale:
                    return "Transform.localScale=" + GetBindStr(Path);
            }
            return "";
        }
	}
}