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
	/// M4uSpecialBindings. Bind Anything
    /// </summary>
	[AddComponentMenu("M4u/SpecialBindings")]
    public class M4uSpecialBindings : M4uBindingMultiple
	{
		public string[] TargetPath;

		private object[] obj = null;
		private PropertyInfo[] pi = null;
		private FieldInfo[] fi = null;

		public override void Start ()
		{
			base.Start ();

            if (TargetPath != null && TargetPath.Length > 0)
            {
                obj = new object[TargetPath.Length];
                pi = new PropertyInfo[obj.Length];
                fi = new FieldInfo[obj.Length];
                for (int i = 0; i < TargetPath.Length; i++)
                {
                    string[] names = TargetPath[i].Split('.');
                    object parent = GetComponent(names[0]);
                    object value = null;
                    for (int j = 1; j < names.Length; j++)
                    {
                        bool isLast = (j == names.Length - 1);
                        string mname = names[j];
                        ParseMember(isLast, ref mname, ref parent, ref value, ref obj[i], ref pi[i], ref fi[i]);
                    }
                }
            }
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();

            if (obj != null)
            {
                for (int i = 0; i < obj.Length; i++)
                {
                    SetMember(obj[i], pi[i], fi[i], Values[i]);
                }
            }
		}

        public override string ToString()
        {
            string str = "";
            if(TargetPath != null)
            {
                string[] binds = GetBindStrs(Path);
                for (int i = 0; i < TargetPath.Length; i++)
                {
                    str += (str == "") ? "" : "/";
                    str += TargetPath[i] + "=";
                    str += (i < binds.Length) ? binds[i] : "";
                }
            }
            return str;
        }
	}
}