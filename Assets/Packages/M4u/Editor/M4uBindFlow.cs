//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace M4u
{
    /// <summary>
    /// M4uBindFlow. Show ContextRoot->Binding Flow
    /// </summary>
    public class M4uBindFlow : EditorWindow
    {
        private static readonly string RootOption = "flow node 1";
        private static readonly string BindOption = "flow node 3";
        private static readonly string NoneLabel = "None";
        private static readonly string ButtonText = "Select";
        private static readonly Rect RootBaseRect = new Rect(10f, 10f, 0f, 0f);
        private static readonly Rect BindBaseRect = new Rect(300f, 10f, 400f, 0f);
        private static readonly float BindMoveBaseY = 100f;
        private static readonly float GUIDefalutSpace = 5f;

        private Vector2 dragPosition = Vector2.zero;
        private Vector2 scrollPos = Vector2.zero;
        private List<GameObject> roots = new List<GameObject>();
        private List<GameObject> binds = new List<GameObject>();

        [MenuItem("Tools/M4u/Open BindFlow", false, 10)]
        public static void Open()
        {
			GetWindow<M4uBindFlow>(typeof(M4uBindFlow).Name);
        }

        void OnGUI()
        {
            var e = Event.current;
            switch (e.type)
            {
				case UnityEngine.EventType.MouseDown:
                    dragPosition = e.mousePosition;
                    break;
				case UnityEngine.EventType.MouseDrag:
                    scrollPos += (dragPosition - e.mousePosition);
                    dragPosition = e.mousePosition;
                    Repaint();
                    break;
            }

            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUI.skin.box))
            {
                scrollPos = scrollView.scrollPosition;

                roots.Clear();
                binds.Clear();
				var gos = Resources.FindObjectsOfTypeAll<GameObject>().Where((g) => (g.activeInHierarchy && g.transform.parent == null)).OrderBy((g) => g.transform.GetSiblingIndex());
                foreach(var go in gos)
                {
                    GetRootAndBind(go);
                }
                
                BeginWindows();
                int id = 0;
                float y = RootBaseRect.y;
                for (int i = 0; i < roots.Count + 1; i++)
                {
                    bool hasRoot = (i < roots.Count);
                    if (!hasRoot && binds.Count == 0) { break; }
                    
                    var rootGo = (hasRoot) ? roots[i] : null;
                    var root = (hasRoot) ? rootGo.GetComponent<M4uContextRoot>() : null;

                    id++;
                    var rootRect = new Rect(RootBaseRect.x, y, RootBaseRect.width, RootBaseRect.height);
                    GUI.WindowFunction func = (_) =>
                    {
                        var go = rootGo;
                        EditorGUILayout.ObjectField(go, typeof(GameObject), true);

                        if(root != null && root.Context != null)
                        {
                            EditorGUILayout.LabelField(root.Context.GetType().Name, EditorStyles.textField);
                        }
                        else
                        {
                            EditorGUILayout.LabelField(NoneLabel, EditorStyles.textField);
                        }

                        if (GUILayout.Button(ButtonText)) { Selection.activeGameObject = go; }
                    };
                    rootRect = GUILayout.Window(id, rootRect, func, typeof(M4uContextRoot).Name, RootOption);

                    bool hasBind = false;
                    for (int j = 0; j < binds.Count; j++)
                    {
                        var bindGo = binds[j];
                        var components = bindGo.GetComponents(typeof(M4uBinding));
                        if (!hasRoot || ((M4uBinding)components[0]).GetRoot() == root)
                        {
                            hasBind = true;

                            id++;
                            var bindRect = new Rect(BindBaseRect.x, y, BindBaseRect.width, BindBaseRect.height);
                            func = (_) =>
                            {
                                var go = bindGo;
                                EditorGUILayout.ObjectField(go, typeof(GameObject), true);

                                foreach (var component in components)
                                {
                                    EditorGUILayout.LabelField(component.ToString(), EditorStyles.textField);
                                }

                                if (GUILayout.Button(ButtonText)) { Selection.activeGameObject = go; }
                            };
                            bindRect = GUILayout.Window(id, bindRect, func, typeof(M4uBinding).Name, BindOption);
                            y += BindMoveBaseY + (components.Length - 1) * (EditorStyles.textField.lineHeight + GUIDefalutSpace);

                            var sp = new Vector3(rootRect.x + rootRect.width, rootRect.y + rootRect.height / 2f, 0f);
                            var ep = new Vector3(bindRect.x, bindRect.y + bindRect.height / 2f, 0f);
                            Handles.DrawBezier(sp, ep, sp, ep, Color.black, null, 2f);

                            binds.RemoveAt(j);
                            j--;
                        }
                    }
                    if (!hasBind) { y += BindMoveBaseY; }
                }
                EndWindows();

                EditorGUILayout.LabelField("", GUILayout.Width(BindBaseRect.x + BindBaseRect.width), GUILayout.Height(y));
            }
        }

        private void GetRootAndBind(GameObject go)
        {
            var root = go.GetComponent<M4uContextRoot>();
            if (root != null && !roots.Contains(root.gameObject))
            {
                roots.Add(root.gameObject);
            }

            var bind = go.GetComponent<M4uBinding>();
            if (bind != null && !binds.Contains(bind.gameObject))
            {
                binds.Add(bind.gameObject);
            }

            for (int i = 0; i < go.transform.childCount; i++)
            {
                GetRootAndBind(go.transform.GetChild(i).gameObject);
            }
        }
    }
}