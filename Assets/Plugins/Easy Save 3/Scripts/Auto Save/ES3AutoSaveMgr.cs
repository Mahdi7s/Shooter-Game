using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class ES3AutoSaveMgr : MonoBehaviour, ISerializationCallbackReceiver 
{
	public enum LoadEvent { None, Awake, Start }
	public enum SaveEvent { None, OnApplicationQuit }

	public string key = System.Guid.NewGuid().ToString();
	public SaveEvent saveEvent = SaveEvent.OnApplicationQuit;
	public LoadEvent loadEvent = LoadEvent.Awake;
	public ES3SerializableSettings settings = null;

	public ES3AutoSave[] autoSaves = null;

	public void Save()
	{
		if(autoSaves == null || autoSaves.Length == 0)
			return;

		var gameObjects = new GameObject[autoSaves.Length];
		for(int i = 0; i < autoSaves.Length; i++)
			gameObjects [i] = autoSaves [i].gameObject;

		ES3.Save<GameObject[]>(key, gameObjects, settings);
	}

	public void Load()
	{
		if(autoSaves == null || autoSaves.Length == 0)
			return;
		
		ES3.Load<GameObject[]>(key, new GameObject[0], settings);
	}

	void Start()
	{
		if(loadEvent == LoadEvent.Start)
			Load();
	}

	public void Awake()
	{
		if(loadEvent == LoadEvent.Awake)
			Load();
	}

	void OnApplicationQuit()
	{
		if(saveEvent == SaveEvent.OnApplicationQuit)
			Save();
	}

	public void UpdateAutoSaves()
	{
		autoSaves = Resources.FindObjectsOfTypeAll<ES3AutoSave>();
	}

	public void OnBeforeSerialize()
	{
		#if UNITY_EDITOR
		// If the default settings have not yet been set, set them.
		if(settings == null || settings.bufferSize == 0)
			settings = new ES3SerializableSettings (true);

		// Get any Auto Saves in the scene.
		if(BuildPipeline.isBuildingPlayer)
			UpdateAutoSaves();
		#endif
	}

	public void OnAfterDeserialize(){}
}
