//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace M4u
{
    /// <summary>
    /// M4uCollectionBinding. Bind Collection
    /// </summary>
    [AddComponentMenu("M4u/CollectionBinding")]
    [RequireComponent(typeof(RemoveChildrenOnDisable))]
    public class M4uCollectionBinding : M4uBindingSingle
    {
        public float InstantiateTimeGap;
        public GameObject Data;
        public string SavePath;
        public string OnChanged;

        private bool _flag;
        private bool isChange = false;
        private Action onChanged = null;
        private ICollection saveCollection = null;
        private List<GameObject> saveGos = new List<GameObject>();
        private List<object> saveObjs = new List<object>();
        private void OnEnable()
        {
            _flag = true;
        }
        public override void Start()
        {
            base.Start();

            if (!string.IsNullOrEmpty(SavePath))
            {
                string[] names = SavePath.Split('.');
                object parent = Root.Context;
                object value = null;
                object obj = null;
                PropertyInfo pi = null;
                FieldInfo fi = null;
                for (int i = 0; i < names.Length; i++)
                {
                    bool isLast = (i == names.Length - 1);
                    string name = names[i];
                    ParseMember(isLast, ref name, ref parent, ref value, ref obj, ref pi, ref fi);
                }
                saveCollection = GetMember<ICollection>(obj, pi, fi);
            }

            if (!string.IsNullOrEmpty(OnChanged))
            {
                string[] names = OnChanged.Split('.');
                string name = "";
                object parent = Root.Context;
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
                onChanged = (Action)Delegate.CreateDelegate(typeof(Action), parent, name);
            }
            OnChange();
        }

        public override void Update()
        {
            base.Update();
            if (_flag)
            {
                StartCoroutine(CheckForChange());
            }
        }

        private IEnumerator CheckForChange()
        {
            _flag = false;
            var waitTime = 0f;
            object value = Values[0];
            if (value != null)
            {
                Type type = value.GetType();
                int count = 0;
                ICollection collection = null;
                if (type.IsPrimitive)
                {
                    if (int.TryParse(value.ToString(), out count) && saveObjs.Count != count)
                    {
                        isChange = true;
                        waitTime = count * InstantiateTimeGap;
                    }
                }
                else
                {
                    collection = value as ICollection;
                    if (collection != null)
                    {
                        waitTime = collection.Count * InstantiateTimeGap;
                        if (saveObjs.Count != collection.Count)
                        {
                            isChange = true;
                        }
                        else if (type.IsArray)
                        {
                            var data = (Array)value;
                            for (int i = saveObjs.Count - 1; i >= 0; i--)
                            {
                                if (saveObjs[i] != data.GetValue(i))
                                {
                                    isChange = true;
                                    break;
                                }
                            }
                        }
                        else if (value is IList)
                        {
                            var data = (IList)value;
                            for (int i = saveObjs.Count - 1; i >= 0; i--)
                            {
                                if (saveObjs[i] != data[i])
                                {
                                    isChange = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (isChange)
                {
                    isChange = false;

                    foreach (var go in saveGos)
                    {
                        Destroy(go);
                    }

                    saveGos.Clear();
                    saveObjs.Clear();

                    var saveList = (saveCollection != null) ? (saveCollection as IList) : null;
                    var saveDic = (saveCollection != null && saveList == null)
                        ? (saveCollection as IDictionary)
                        : null;
                    if (saveList != null)
                    {
                        saveList.Clear();
                    }

                    if (saveDic != null)
                    {
                        saveDic.Clear();
                    }

                    if (type.IsPrimitive)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            StartCoroutine(CreateData(i, saveList, saveDic, i));
                        }
                    }
                    else if (collection != null)
                    {
                        var j = 0;
                        foreach (var obj in collection)
                        {
                            StartCoroutine(CreateData(obj, saveList, saveDic, j));
                            j++;
                        }
                    }

                    if (onChanged != null)
                    {
                        onChanged();
                    }
                }
            }
            yield return new WaitForSeconds(waitTime + 0.1f);
            _flag = true;
        }
        private IEnumerator CreateData(object obj, IList saveList, IDictionary saveDic, int order)
        {
            yield return new WaitForSeconds(InstantiateTimeGap * order);
            var go = Instantiate(Data);
            var root = go.GetComponent<M4uContextRoot>();
            var pos = go.transform.localPosition;
            var rot = go.transform.eulerAngles;
            var scale = go.transform.localScale;
            go.name = go.name.Replace("(Clone)", string.Format("_{0}", order + 1));
            go.transform.SetParent(transform);
            go.transform.localPosition = pos;
            go.transform.eulerAngles = rot;
            go.transform.localScale = scale;
            if (root != null && root.Context == null && (obj is M4uContextInterface))
            {
                root.Context = (M4uContextInterface)obj;
            }

            saveGos.Add(go);
            saveObjs.Add(obj);
            if (saveList != null) { saveList.Add(go); }
            else if (saveDic != null) { saveDic.Add(obj, go); }
            //return go;


            /*var go = (GameObject)Instantiate(Data);
		    var root = go.GetComponent<TrixCollectionContext>();
		    var pos = go.transform.localPosition;
		    var rot = go.transform.eulerAngles;
		    var scale = go.transform.localScale;
		    go.transform.SetParent(transform);
		    go.transform.localPosition = pos;
		    go.transform.eulerAngles = rot;
		    go.transform.localScale = scale;
            if (root != null && root.Context == null && (obj is M4uContextInterface))
            {
                root.Context = (M4uContextInterface)obj;
            }

            saveGos.Add(go);
		    saveObjs.Add(obj);
		    if (saveList != null) { saveList.Add(go); }
		    else if (saveDic != null) { saveDic.Add(obj, go); }
		    return go;*/

        }

        public override void OnChange()
        {
            base.OnChange();
            isChange = true;
        }

        public override string ToString()
        {
            return "Collection=" + GetBindStr(Path) + "/" + ((Data != null) ? Data.name : "None");
        }
    }
}