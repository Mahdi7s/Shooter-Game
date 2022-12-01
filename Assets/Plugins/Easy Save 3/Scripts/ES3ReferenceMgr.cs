using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES3Internal;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class ES3ReferenceMgr : ES3ReferenceMgrBase, ISerializationCallbackReceiver 
{
	public void OnBeforeSerialize()
	{
		#if UNITY_EDITOR
		if(BuildPipeline.isBuildingPlayer)
		{
			GenerateReferences();
			GeneratePrefabReferences();
		}
		#endif

	}

	public void OnAfterDeserialize(){}

	#if UNITY_EDITOR

	public void GenerateReferences()
	{
		bool undoRecorded = false;

		// Remove NULL values from Dictionary.
		if(idRef.RemoveNullValues() > 0)
		{
			//Undo.RecordObject(this, "Update Easy Save 3 Reference List");
			undoRecorded = true;
		}
			
		var sceneObjects = this.gameObject.scene.GetRootGameObjects();

		// Remove the manager object.
		ArrayUtility.Remove(ref sceneObjects, this.gameObject);

		var dependencies = EditorUtility.CollectDependencies(sceneObjects);

		for(int i=0; i<dependencies.Length; i++)
		{
			var obj = (UnityEngine.Object)dependencies[i];

			if(obj == null || !CanBeSaved(obj))
				continue;

			// If we're adding a new item to the type list, make sure we've recorded an undo for the object.
			if(Get(obj) == -1)
			{
				if(!undoRecorded)
				{
					//Undo.RecordObject(this, "Update Easy Save 3 Reference List");
					undoRecorded = true;
				}
				Add(obj);
			}
		}
	}

	public void GeneratePrefabReferences()
	{
		bool undoRecorded = false;

		if(this.prefabs.RemoveAll(item => item == null) > 0)
		{
			Undo.RecordObject(this, "Update Easy Save 3 Reference List");
			undoRecorded = true;
		}

		var es3Prefabs = Resources.FindObjectsOfTypeAll<ES3Prefab>();

		if(es3Prefabs.Length == 0)
			return;

		foreach(var es3Prefab in es3Prefabs)
		{
			if(PrefabUtility.GetPrefabType(es3Prefab.gameObject) != PrefabType.Prefab)
				continue;

			if(GetPrefab(es3Prefab) == -1)
			{
				AddPrefab(es3Prefab);
				if(!undoRecorded)
				{
					Undo.RecordObject(this, "Update Easy Save 3 Reference List");
					undoRecorded = true;
				}
			}

			bool prefabUndoRecorded = false;

			if(es3Prefab.localRefs.RemoveNullKeys() > 0)
			{
				Undo.RecordObject(es3Prefab, "Update Easy Save 3 Prefab");
				prefabUndoRecorded = true;
			}

			// Get GameObject and it's children and add them to the reference list.
			foreach(var obj in EditorUtility.CollectDependencies(new UnityEngine.Object[]{es3Prefab}))
			{
				if(obj == null || !CanBeSaved(obj))
					continue;
				
				if(es3Prefab.Get(obj) == -1)
				{
					es3Prefab.Add(obj);
					if(!prefabUndoRecorded)
					{
						Undo.RecordObject(es3Prefab, "Update Easy Save 3 Prefab");
						prefabUndoRecorded = true;
					}
				}
			}
		}
	}

	public static bool CanBeSaved(UnityEngine.Object obj)
	{
		// Check if any of the hide flags determine that it should not be saved.
		if(	(((obj.hideFlags & HideFlags.DontSave) == HideFlags.DontSave) || 
		     ((obj.hideFlags & HideFlags.DontSaveInBuild) == HideFlags.DontSaveInBuild) ||
		     ((obj.hideFlags & HideFlags.DontSaveInEditor) == HideFlags.DontSaveInEditor) ||
		     ((obj.hideFlags & HideFlags.HideAndDontSave) == HideFlags.HideAndDontSave)))
		{
			var type = obj.GetType();
			// Meshes are marked with HideAndDontSave, but shouldn't be ignored.
			if(type != typeof(Mesh) && type != typeof(Material))
				return false;
		}
		return true;
	}

	#endif
}
