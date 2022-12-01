using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Packages.M4u.Scripts.Trixmen.Editor
{
    [CustomPropertyDrawer(typeof(TrixM4uPathAttribute))]
    public class TrixM4UPathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Application.isPlaying)
                return;
            //var gameObject = property.serializedObject.targetObject as GameObject;
            var gameObject = (property.serializedObject.targetObject as global::M4u.M4uBinding)?.gameObject;
            var menuArr = TrixM4UReflection.GetRootContextMenu(gameObject, property.serializedObject.targetObject.GetType());
            if (menuArr == null || menuArr.Count <= 0)
                return;

            var selectedIndex = menuArr.IndexOf(property.stringValue.Replace(".", "/"));
            selectedIndex = EditorGUI.Popup(position, "Path", selectedIndex, menuArr.ToArray());
            if (selectedIndex >= 0 && selectedIndex < menuArr.Count)
            {
                property.stringValue = menuArr.ElementAt(selectedIndex).Replace("/", ".");
            }
        }
    }
}
