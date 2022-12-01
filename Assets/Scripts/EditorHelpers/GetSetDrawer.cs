using System.Reflection;
using CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace EditorHelpers
{
#if UNITY_EDITOR || UNITY_EDITOR_64
    [CustomPropertyDrawer(typeof(GetSetAttribute))]
    sealed class GetSetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetAttribute = (GetSetAttribute)base.attribute;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                targetAttribute.Dirty = true;
            }
            else if (targetAttribute.Dirty)
            {
                var parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

                var type = parent.GetType();
                var info = type.GetProperty(targetAttribute.Name);

                if (info == null)
                    Debug.LogError("Invalid property name \"" + targetAttribute.Name + "\"");
                else
                    info.SetValue(parent, fieldInfo.GetValue(parent), null);

                targetAttribute.Dirty = false;
            }
        }

        public static object GetParentObject(string path, object obj)
        {
            while (true)
            {
                var fields = path.Split('.');

                if (fields.Length == 1) return obj;

                var info = obj?.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                obj = info?.GetValue(obj);

                path = string.Join(".", fields, 1, fields.Length - 1);
            }
        }
    }
#endif
}