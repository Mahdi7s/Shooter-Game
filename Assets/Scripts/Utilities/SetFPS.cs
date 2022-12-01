using UnityEngine;
using System.Collections;

public class SetFPS : MonoBehaviour {
	
	public int CustomFrames;
	
	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
		if (CustomFrames!=0)
			Application.targetFrameRate = CustomFrames;
		else
			Application.targetFrameRate=30;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
