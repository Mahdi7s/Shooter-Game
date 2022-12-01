using TrixComponents;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrixButton))]
[CanEditMultipleObjects]
public class TrixButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        TrixButton component = (TrixButton)target;

        base.OnInspectorGUI();

        component.ClickSound = (AudioClip)EditorGUILayout.ObjectField("Click Sound", component.ClickSound, typeof(AudioClip), true);
        component.ClickDelay = EditorGUILayout.FloatField("Click Delay", component.ClickDelay);
        //component.IsGamePlayHudButton = EditorGUILayout.Toggle("Is Gameplay Hud Button", component.IsGamePlayHudButton);
        serializedObject.ApplyModifiedProperties();
    }
}
