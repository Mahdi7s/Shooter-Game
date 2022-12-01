using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[InitializeOnLoad]

public class TrixActiveBindingHierarchyGUI
{

    static TrixActiveBindingHierarchyGUI()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGui;
    }

    static void OnHierarchyGui(int instanceId, Rect selectionRect)
    {
        if (Application.isPlaying)
        {
            return;
        }
        var obj = EditorUtility.InstanceIDToObject(instanceId);
        if (obj)
        {
            GameObject go = (GameObject)obj;
            if (go)
            {
                var activeBinding = go.GetComponent<TrixActiveBindingHandler>();
                if (activeBinding)
                {
                    Rect rect = new Rect(selectionRect);
                    rect.x = rect.width;
                    var toggleStatus = GUI.Toggle(rect, activeBinding.IsActiveOnEditMode, "");
                    if (!Equals(toggleStatus, activeBinding.IsActiveOnEditMode))
                    {
                        activeBinding.IsActiveOnEditMode = toggleStatus;
                        EditorUtility.SetDirty(activeBinding);
                    }
                }
            }
        }
    }

}
#endif
