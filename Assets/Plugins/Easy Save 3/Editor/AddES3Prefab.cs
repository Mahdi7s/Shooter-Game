using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ES3Internal;

namespace ES3Editor
{
	public class AddES3Prefab : Editor 
	{
		[MenuItem("GameObject/Easy Save 3/Enable Easy Save for Prefab", false, 1001)]
		[MenuItem("Assets/Easy Save 3/Enable Easy Save for Prefab", false, 1001)]
		public static void Enable()
		{
			var go = Selection.activeGameObject;

			if(PrefabUtility.GetPrefabType(go) != PrefabType.Prefab)
			{
				go = (GameObject)PrefabUtility.GetPrefabParent(go);
				if(go == null)
					return;
			}
			Undo.AddComponent<ES3Prefab>(go);
			//ES3Postprocessor.GeneratePrefabReferences();
		}

		[MenuItem("GameObject/Easy Save 3/Enable Easy Save for Prefab", true, 1001)]
		[MenuItem("Assets/Easy Save 3/Enable Easy Save for Prefab", true, 1001)]
		public static bool Validate()
		{
			var go = Selection.activeGameObject;
			if(go == null)
				return false;
			if(go.GetComponent<ES3Prefab>() != null)
				return false;
			return true;
		}
	}
}
