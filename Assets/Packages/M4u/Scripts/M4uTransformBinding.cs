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
    /// M4uTransformBinding. Bind Transform
    /// </summary>
	[AddComponentMenu("M4u/TransformBinding")]
	public class M4uTransformBinding : M4uBindingSingle
	{
		public TransformType Type = TransformType.Px;

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

			float value = float.Parse (Values [0].ToString ());
			switch (Type)
			{
				case TransformType.Px:
					ui.localPosition = new Vector3(value, ui.localPosition.y, ui.localPosition.z);
					break;
				case TransformType.Py:
					ui.localPosition = new Vector3(ui.localPosition.x, value, ui.localPosition.z);
					break;
				case TransformType.Pz:
					ui.localPosition = new Vector3(ui.localPosition.x, ui.localPosition.y, value);
					break;
					
				case TransformType.Rx:
					ui.localRotation = Quaternion.Euler(value, ui.localRotation.y, ui.localRotation.z);
					break;
				case TransformType.Ry:
					ui.localRotation = Quaternion.Euler(ui.localRotation.x, value, ui.localRotation.z);
					break;
				case TransformType.Rz:
					ui.localRotation = Quaternion.Euler(ui.localRotation.x, ui.localRotation.y, value);
					break;
					
				case TransformType.Sx:
					ui.localScale = new Vector3(value, ui.localScale.y, ui.localScale.z);
					break;
				case TransformType.Sy:
					ui.localScale = new Vector3(ui.localScale.x, value, ui.localScale.z);
					break;
				case TransformType.Sz:
					ui.localScale = new Vector3(ui.localScale.x, ui.localScale.y, value);
					break;
			}
		}

        public override string ToString()
        {
            switch (Type)
            {
                case TransformType.Px:
                    return "Transform.localPosition.x=" + GetBindStr(Path);
                case TransformType.Py:
                    return "Transform.localPosition.y=" + GetBindStr(Path);
                case TransformType.Pz:
                    return "Transform.localPosition.z=" + GetBindStr(Path);

                case TransformType.Rx:
                    return "Transform.localRotation.x=" + GetBindStr(Path);
                case TransformType.Ry:
                    return "Transform.localRotation.y=" + GetBindStr(Path);
                case TransformType.Rz:
                    return "Transform.localRotation.z=" + GetBindStr(Path);

                case TransformType.Sx:
                    return "Transform.localScale.x=" + GetBindStr(Path);
                case TransformType.Sy:
                    return "Transform.localScale.y=" + GetBindStr(Path);
                case TransformType.Sz:
                    return "Transform.localScale.z=" + GetBindStr(Path);
            }
            return "";
        }
	}
}