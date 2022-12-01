using System.Collections.Generic;
using Infrastructure;
using Models;
using Models.Constants;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects
{
#if UNITY_EDITOR || UNITY_EDITOR_64
    [CreateAssetMenu(fileName = "ResourceTypes", menuName = "Datasets/ResourceType", order = 1)]
    public class ResourceTypeDataSet : ScriptableObject
    {
        public List<ResourceType> ResourceTypes;
    }

    public class ResourceTypeDataSetInitializer
    {
        [MenuItem(StaticValues.MenuItemGenerateRoot + "InitializeResourceTypeDataSet")]
        public static void InitializeList()
        {
            var resourceTypeDataSet = ResourceManager.LoadResource<ResourceTypeDataSet>("ResourceTypeDataSet");
            if (resourceTypeDataSet)
            {
                resourceTypeDataSet.ResourceTypes.Clear();
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "Sprite",
                    Extension = "jpg|png|jpeg",
                    Prefix = "spr|icons"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "GameObject",
                    Extension = "prefab",
                    Prefix = "pre"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "Animation",
                    Extension = "anim",
                    Prefix = "anim"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "RuntimeAnimatorController",
                    Extension = "controller",
                    Prefix = "ctrl"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "AudioClip",
                    Extension = "mp3|wav",
                    Prefix = "sfx"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "Font",
                    Extension = "ttf",
                    Prefix = "fnt"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "Shader",
                    Extension = "shader",
                    Prefix = "shd"
                });
                resourceTypeDataSet.ResourceTypes.Add(new ResourceType
                {
                    Type = "Material",
                    Extension = "mat",
                    Prefix = "mat"
                });
                EditorUtility.SetDirty(resourceTypeDataSet);
            }
        }
    }
#endif
}
