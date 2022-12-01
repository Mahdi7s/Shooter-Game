using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Reflection;
using ES3Internal;

[InitializeOnLoad]
public class ES3Postprocessor : UnityEditor.AssetModificationProcessor
{
	public static ES3ReferenceMgr _refMgr;
	public static ES3AutoSaveMgr _autoSaveMgr;
	public static bool didGenerateReferences = false;
	public static ES3DefaultSettings settings;

	// This constructor is also called once when playmode is activated.
	static ES3Postprocessor()
	{
		ES3Editor.ES3Window.OpenEditorWindowOnStart();
		#if UNITY_2017_3_OR_NEWER
		EditorApplication.playModeStateChanged += PlaymodeStateChanged;
		#else
		EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
		#endif
	}

	#if UNITY_2017_3_OR_NEWER
	static void PlaymodeStateChanged(PlayModeStateChange state)
	#else
	static void PlaymodeStateChanged()
	#endif
	{
		// This is called when we press the Play button, but before serialisation takes place.
		if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
		{
			var go = GameObject.Find("Easy Save 3 Manager");
			var defaultSettings = ES3EditorUtility.GetDefaultSettings();
			if(go == null && !defaultSettings.addMgrToSceneAutomatically)
				return;

			AddManagerToScene();

			if(defaultSettings.autoUpdateReferences)
			{
				_refMgr.GenerateReferences();
				_refMgr.GeneratePrefabReferences();
			}
			_autoSaveMgr.UpdateAutoSaves();

			AssetDatabase.Refresh();
		}
	}

	public static GameObject AddManagerToScene()
	{
		var go = GameObject.Find("Easy Save 3 Manager");

		if(go == null)
		{
			go = new GameObject("Easy Save 3 Manager");
			var inspectorInfo = go.AddComponent<ES3InspectorInfo>();
			inspectorInfo.message = "The Easy Save 3 Manager is required in any scenes which use Easy Save, and is automatically added to your scene when you enter Play mode.\n\nTo stop this from automatically being added to your scene, go to 'Window > Easy Save 3 > Settings' and deselect the 'Auto Add Manager to Scene' checkbox.";



			_refMgr = go.AddComponent<ES3ReferenceMgr>();
			_autoSaveMgr = go.AddComponent<ES3AutoSaveMgr>();
			Undo.RegisterCreatedObjectUndo(go, "Enabled Easy Save for Scene");
		}
		else
		{
			_refMgr = go.GetComponent<ES3ReferenceMgr>();
			if(_refMgr == null)
			{
				_refMgr = go.AddComponent<ES3ReferenceMgr>();
				Undo.RegisterCreatedObjectUndo(_refMgr, "Enabled Easy Save for Scene");
			}

			_autoSaveMgr = go.GetComponent<ES3AutoSaveMgr>();
			if(_autoSaveMgr == null)
			{
				_autoSaveMgr = go.AddComponent<ES3AutoSaveMgr>();
				Undo.RegisterCreatedObjectUndo(_autoSaveMgr, "Enabled Easy Save for Scene");
			}
		}
		return go;
	}

	/*public static void GenerateReferences()
	{
		GenerateReferences(EditorSceneManager.GetActiveScene());
	}

	public static void GenerateReferences(Scene scene)
	{
		if(!scene.isLoaded || EditorApplication.isPlaying)
			return;

		Debug.Log("Generated references");

		var refMgr = GetReferenceMgr();
		if(refMgr == null)
			return;

		bool undoRecorded = false;

		didGenerateReferences = true;

		// Remove NULL values from Dictionary.
		if(refMgr.idRef.RemoveNullValues() > 0)
		{
			Undo.RecordObject(refMgr, "Update Easy Save 3 Reference List");
			undoRecorded = true;
		}

		var sceneObjects = scene.GetRootGameObjects();

		var dependencies = EditorUtility.CollectDependencies(sceneObjects);

		for(int i=0; i<dependencies.Length; i++)
		{
			var obj = (UnityEngine.Object)dependencies[i];

			// If we're adding a new item to the type list, make sure we've recorded an undo for the object.
			if(refMgr.Get(obj) == -1)
			{
				if(!undoRecorded)
				{
					Undo.RecordObject(refMgr, "Update Easy Save 3 Reference List");
					undoRecorded = true;
				}
				refMgr.Add(obj);
			}
		}
	}

	public static void GeneratePrefabReferences()
	{
		var refMgr = GetReferenceMgr();
		if(refMgr == null)
			return;

		bool undoRecorded = false;

		// Remove null values from prefab array.
		if(refMgr != null)
		{
			if(refMgr.prefabs.RemoveAll(item => item == null) > 0)
			{
				Undo.RecordObject(refMgr, "Update Easy Save 3 Reference List");
				undoRecorded = true;
			}
		}

		var es3Prefabs = Resources.FindObjectsOfTypeAll<ES3Prefab>();

		if(es3Prefabs.Length == 0)
			return;

		foreach(var es3Prefab in es3Prefabs)
		{
			Debug.Log(es3Prefab);
			if(PrefabUtility.GetPrefabType(es3Prefab.gameObject) != PrefabType.Prefab)
				continue;

			if(refMgr != null)
			{
				if(refMgr.GetPrefab(es3Prefab) != -1)
				{
					refMgr.AddPrefab(es3Prefab);
					if(!undoRecorded)
					{
						Undo.RecordObject(refMgr, "Update Easy Save 3 Reference List");
						undoRecorded = true;
					}
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
				if(es3Prefab.Get(obj) != -1)
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
	}*/
}
