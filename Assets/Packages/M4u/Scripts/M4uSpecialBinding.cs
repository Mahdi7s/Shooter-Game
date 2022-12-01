//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace M4u
{
    /// <summary>
	/// M4uSpecialBinding. Bind Anything
    /// </summary>
	[AddComponentMenu("M4u/SpecialBinding")]
	public class M4uSpecialBinding : M4uBindingSingle
	{
		public string TargetPath;

		private object obj = null;
		private PropertyInfo pi = null;
		private FieldInfo fi = null;

		public override void Start ()
		{
			base.Start ();

            if (!string.IsNullOrEmpty(TargetPath))
            {
                string[] names = TargetPath.Split('.');
                object parent = GetComponent(names[0]);
                object value = null;
                for (int i = 1; i < names.Length; i++)
                {
                    bool isLast = (i == names.Length - 1);
                    string name = names[i];
                    ParseMember(isLast, ref name, ref parent, ref value, ref obj, ref pi, ref fi);
                }
            }
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

			SetMember (obj, pi, fi, Values [0]);
		}

        public override string ToString()
        {
            return TargetPath + "=" + GetBindStr(Path);
        }
	}
}