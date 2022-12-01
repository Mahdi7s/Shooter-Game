using UnityEngine;

public class RemoveChildrenOnDisable : MonoBehaviour 
{
	private void OnDisable()
	{
		foreach (Transform child in transform) 
		{
			Destroy(child.gameObject);
		}
	}
}
