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

namespace M4u
{
    /// <summary>
    /// M4uHierarchyIcon. Show/Hide Hierarchy Icon
    /// </summary>
    [InitializeOnLoad]
    public class M4uHierarchyIcon
    {
        private static readonly string ResPath = Application.dataPath + "/M4u/Editor/Resource/";
        private static readonly string[] IconPaths = { ResPath + "icon_r_{0}.png", ResPath + "icon_b_{0}.png" };
        private static readonly int IconCount = 26;
        private static readonly string IconPrefsKey = "m4u_show_hierarchy_icon";

        private static Texture[,] icons = new Texture[2, IconCount];

        private static bool IsShowIcon 
        {
            get 
            {
                return (PlayerPrefs.GetInt(IconPrefsKey, 0) == 1); 
            }
            set 
            {
                PlayerPrefs.SetInt(IconPrefsKey, (value) ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        static M4uHierarchyIcon()
        {
            ShowHierarchyIcon(IsShowIcon);
        }

        [MenuItem("Tools/M4u/Show Hierarchy Icon")]
        public static void ShowHierarchyIcon()
        {
            ShowHierarchyIcon(true);
        }

        [MenuItem("Tools/M4u/Hide Hierarchy Icon")]
        public static void HideHierarchyIcon()
        {
            ShowHierarchyIcon(false);
        }

        private static void ShowHierarchyIcon(bool isShow)
        {
            IsShowIcon = isShow;
            if (isShow)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
            }
            else
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            }
        }

        private static void OnHierarchyWindowItemOnGUI(int id, Rect rect)
        {
            var go = EditorUtility.InstanceIDToObject(id) as GameObject;
            if (go == null) { return; }

            int idx = 0;
            foreach (var c in go.GetComponents<Component>())
            {
                bool isRoot = (c is M4uContextRoot);
                bool isBind = (c is M4uBinding);
                if (isRoot || isBind)
                {
                    idx++;

                    int rootId = id;
                    if(isBind)
                    {
                        var bind = c as M4uBinding;
                        var root = bind.GetRoot();
                        rootId = (root != null) ? root.gameObject.GetInstanceID() : 0;
                    }
                    rootId = Mathf.Abs(rootId);

                    int iconType = (isRoot) ? 0 : 1;
                    int colorIdx = rootId % IconCount;
                    if (icons[iconType, colorIdx] == null)
                    {
                        icons[iconType, colorIdx] = M4uUtil.CreateTexture2D(File.ReadAllBytes(string.Format(IconPaths[iconType], colorIdx)));
                    }

					var icon = icons[iconType, colorIdx];
					var irect = new Rect(rect);
					irect.x += rect.width - icon.width * idx;
					irect.width = icon.width;
					irect.height = icon.height;

					GUI.DrawTexture(irect, icon);
                }
            }
        }
    }
}