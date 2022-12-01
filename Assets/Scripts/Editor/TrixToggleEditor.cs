using TrixComponents;
using UnityEditor;

[CustomEditor(typeof(TrixToggle))]
[CanEditMultipleObjects]
public class TrixToggleEditor : UnityEditor.UI.ToggleEditor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        TrixToggle component = (TrixToggle)target;

        base.OnInspectorGUI();

        //component.ClickSound = (Audios)EditorGUILayout.EnumPopup("Click Sound", component.ClickSound);
        component.ClickDelay = EditorGUILayout.FloatField("Click Delay", component.ClickDelay);
        serializedObject.ApplyModifiedProperties();
    }
}
