using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M4u;
using System.Reflection;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class TrixPlaceholderBinding : M4uBindingSingle
{
    public override void Awake()
    {
        base.Awake();

    }

    public override void Start()
    {
        base.Start();
        if (!string.IsNullOrEmpty(Path))
        {
            string tmpPath = Path.StartsWith("★★★ Collection Bindings") ? Path.Split('.').Last() : Path;
            string[] names = tmpPath.Split('.');
            string name = "";
            object parent = Root.Context;

            //string[] names = Path.Split('.');
            //string name = "";
            //object parent = Root.Context;
            object value = null;
            object obj = null;
            PropertyInfo pi = null;
            FieldInfo fi = null;
            for (int i = 0; i < names.Length; i++)
            {
                bool isLast = (i == names.Length - 1);
                name = names[i];
                ParseMember(isLast, ref name, ref parent, ref value, ref obj, ref pi, ref fi);
            }
            SetPlaceholder(parent, name);
        }

    }

    private void SetPlaceholder(object obj, string propName)
    {
        var objType = obj.GetType();
        var objProp = objType.GetProperty(propName);
        if (objProp != null)
        {
            if (objProp.PropertyType == typeof(UnityEngine.GameObject))
            {
                objProp.SetValue(obj, gameObject, null);
            }
            else
            {
                var component = gameObject.GetComponent(objProp.PropertyType);
                if (component)
                {
                    objProp.SetValue(obj, component, null);
                }
                else
                {
                    Debug.LogError("Can't find " + objProp.PropertyType + " for " + propName);
                }
            }
        }
        else
        {
            Debug.LogError("Null TrixPlaceholderBinding for " + propName);
        }
    }

    public override string ToString()
    {
        return gameObject.name + "=" + GetBindStr(Path);
    }
}
