using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class GameSettings : SerializedScriptableObject 
{
    [FolderPath(AbsolutePath = false)]
    public string EnemisFolder;
    [FolderPath(AbsolutePath = false)]
    public string SceneItemsFolder;
}
