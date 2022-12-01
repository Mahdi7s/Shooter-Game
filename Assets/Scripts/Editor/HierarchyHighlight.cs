using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyHighlightManager
{
    public static readonly Color DefaultColorHierarchySelected = new Color(0.243f, 0.4901f, 0.9058f, 1f);
    static HierarchyHighlightManager()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= HierarchyHighlight_OnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyHighlight_OnGUI;
    }
    private static void HierarchyHighlight_OnGUI(int inSelectionId, Rect inSelectionRect)
    {
        GameObject goLabel = EditorUtility.InstanceIDToObject(inSelectionId) as GameObject;

        if (goLabel != null)
        {
            HierarchyHighlighter label = goLabel.GetComponent<HierarchyHighlighter>();

            if (label != null && Event.current.type == EventType.Repaint)
            {
                #region Determine Styling

                bool objectIsSelected = Selection.instanceIDs.Contains(inSelectionId);

                Color bkCol = label.BackgroundColor;
                Color textCol = label.TextColor;
                FontStyle textStyle = label.TextStyle;

                if (!label.isActiveAndEnabled)
                {
                    if (label.CustomInactiveColors)
                    {
                        bkCol = label.BackgroundColorInactive;
                        textCol = label.TextColorInactive;
                        textStyle = label.TextStyleInactive;
                    }
                    else
                    {
                        if (bkCol != HierarchyHighlighter.DefaultBackgroundColor)
                            bkCol.a = bkCol.a * 0.5f; //Reduce opacity by half

                        textCol.a = textCol.a * 0.5f;
                    }
                }

                #endregion


                Rect offset = new Rect(inSelectionRect.position + new Vector2(2f, 0f), inSelectionRect.size);


                #region Draw Background

                //Only draw background if background color is not completely transparent
                if (bkCol.a > 0f)
                {
                    Rect backgroundOffset = new Rect(inSelectionRect.position, inSelectionRect.size);

                    //If the background has transparency, draw a solid color first
                    if (label.BackgroundColor.a < 1f || objectIsSelected)
                    {
                        //ToDo: Pull background color from GUI.skin Style
                        EditorGUI.DrawRect(backgroundOffset, HierarchyHighlighter.DefaultBackgroundColor);
                    }

                    //Draw background
                    if (objectIsSelected)
                        EditorGUI.DrawRect(backgroundOffset, Color.Lerp(GUI.skin.settings.selectionColor, bkCol, 0.3f));
                    else
                        EditorGUI.DrawRect(backgroundOffset, bkCol);
                }

                #endregion


                EditorGUI.LabelField(offset, goLabel.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = textCol },
                    fontStyle = textStyle
                });

                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
}