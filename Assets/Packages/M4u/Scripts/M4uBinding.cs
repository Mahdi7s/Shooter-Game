//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace M4u
{
    /// <summary>
    /// M4uBinding. Bind core script
    /// </summary>
	public class M4uBinding : MonoBehaviour
    {
        private M4uContextRoot root = null;
        private string[] paths = null;
        private object[] values = null;
        private object[] objs = null;
        private PropertyInfo[] pis = null;
        private FieldInfo[] fis = null;

        public M4uContextRoot Root { get { return root; } set { root = value; } }
        public string[] Paths { get { return paths; } set { paths = value; } }
        public object[] Values { get { return values; } set { this.values = value; } }

        public virtual void Awake()
        {
            root = GetRoot();
        }

        public virtual void Start()
        {
            if (root != null && root.Context != null &&
                paths[0] != "" &&
                !(this is M4uEventBinding) && !(this is M4uEventBindings))
            {
                objs = new object[values.Length];
                pis = new PropertyInfo[values.Length];
                fis = new FieldInfo[values.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    try
                    {
                        object parent = root.Context;
                        string[] names = path.Split('.');
                        for (int j = 0; j < names.Length; j++)
                        {
                            bool isLast = (j == names.Length - 1);
                            string name = names[j];
                            ParseMember(isLast, ref name, ref parent, ref values[i], ref objs[i], ref pis[i], ref fis[i]);
                            if (isLast)
                            {
                                var pname = $"_{ name[0].ToString().ToLower()}{name.Substring(1)}";
                                var pfi = parent.GetType().GetField(pname, M4uConst.BindingAttr);
                                var p = (M4uPropertyBase)pfi.GetValue(parent);
                                if (!p.Bindings.Contains(this)) { p.Bindings.Add(this); }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(gameObject.name + ":" + path);
                        throw e;
                    }
                }
            }
        }

        public virtual void Update() { }

        public virtual void OnChange()
        {
            if (objs == null) { return; }

            for (int i = 0; i < objs.Length; i++)
            {
                values[i] = GetMember(objs[i], pis[i], fis[i]);
            }
        }

        public M4uContextRoot GetRoot()
        {
            return GetRoot(transform);
        }

        public M4uContextRoot GetRoot(Transform t)
        {
            if (t == null) { return null; }

            var root = t.GetComponent<M4uContextRoot>();
            return (root != null) ? root : GetRoot(t.parent);
        }

        public void ParseMember(bool isLast, ref string name, ref object parent, ref object lastValue, ref object lastObj, ref PropertyInfo lastPi, ref FieldInfo lastFi)
        {
            PropertyInfo pi = null;
            FieldInfo fi = null;
            object value = null;
            GetMemberInfo(ref name, ref parent, ref pi, ref fi, ref value);
            if (isLast)
            {
                lastValue = value;
                lastObj = parent;
                lastPi = pi;
                lastFi = fi;
            }
            else
            {
                parent = value;
            }
        }

        public void GetMemberInfo(ref string name, ref object parent, ref PropertyInfo pi, ref FieldInfo fi, ref object value)
        {
            Type type = parent.GetType();
            {
                pi = type.GetProperty(name, M4uConst.BindingAttr);
                if (pi != null) { value = pi.GetValue(parent, null); }
            }
            if (pi == null)
            {
                fi = type.GetField(name, M4uConst.BindingAttr);
                if (fi != null) { value = fi.GetValue(parent); }
            }
        }

        public void SetMember(object obj, PropertyInfo pi, FieldInfo fi, object value)
        {
            if (pi != null) { pi.SetValue(obj, value, null); }
            else if (fi != null) { fi.SetValue(obj, value); }
        }

        public object GetMember(object obj, PropertyInfo pi, FieldInfo fi)
        {
            return GetMember<object>(obj, pi, fi);
        }

        public T GetMember<T>(object obj, PropertyInfo pi, FieldInfo fi) where T : class
        {
            if (pi != null) { return pi.GetValue(obj, null) as T; }
            else if (fi != null) { return fi.GetValue(obj) as T; }
            return null;
        }

        public string[] GetBindStrs(string[] path)
        {
            if (path == null || path.Length == 0) { return null; }

            string parent = (root != null && root.Context != null) ? (root.Context.GetType().Name + ".") : "";
            string[] strs = new string[path.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = "[" + parent + path[i] + "]";
            }
            return strs;
        }

        public string GetBindStr(string path)
        {
            string parent = (root != null && root.Context != null) ? (root.Context.GetType().Name + ".") : "";
            return "[" + parent + path + "]";
        }
    }
}