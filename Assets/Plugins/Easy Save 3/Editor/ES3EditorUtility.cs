using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using ES3Internal;

public class ES3EditorUtility : Editor 
{
	public static string PathToEasySaveFolder()
	{
		string[] guids = AssetDatabase.FindAssets("ES3Window");
		if(guids.Length == 0)
			Debug.LogError("Could not locate the Easy Save 3 folder because the ES3Window script has been moved or removed.");
		if(guids.Length > 1)
			Debug.LogError("Could not locate the Easy Save 3 folder because more than one ES3Window script exists in the project, but this needs to be unique to locate the folder.");

		return AssetDatabase.GUIDToAssetPath(guids[0]).Split(new string[]{"Editor"}, System.StringSplitOptions.RemoveEmptyEntries)[0];
	}

	public static ES3DefaultSettings GetDefaultSettings()
	{
		var go = Resources.Load<GameObject>("ES3/ES3 Default Settings");
		if(go == null)
			Debug.LogError("Could not find ES3 Default Settings object in Easy Save 3/Resources/ES3.");
		var settings = go.GetComponent<ES3DefaultSettings>();
		if(settings == null)
			Debug.LogError("There is no ES3 Default Settings script attached to the ES3 Default Settings object in Easy Save 3/Resources/ES3");
		return settings;
	}

	public static void DisplayLink(string label, string url)
	{
		var style = ES3Editor.EditorStyle.Get;
		if(GUILayout.Button(label, style.link))
			Application.OpenURL(url);

		var buttonRect = GUILayoutUtility.GetLastRect();
		buttonRect.width = style.link.CalcSize(new GUIContent(label)).x;

		EditorGUIUtility.AddCursorRect(buttonRect, MouseCursor.Link);

			
	}
}
