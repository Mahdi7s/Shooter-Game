using UnityEditor;
using UnityEngine;
using Utilities;

[CustomEditor(typeof(WheelsFortuneHandler))]
public class WheelsFortuneEditor : Editor
{
    private WheelsFortuneHandler _targetScript;
    void OnEnable()
    {
        _targetScript = (WheelsFortuneHandler)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Fill Items") && _targetScript != null)
            _targetScript.FillItems();
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}
