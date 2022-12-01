using System;
using System.Collections;
using System.Collections.Generic;
using Models.Constants;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Utilities;

namespace Infrastructure
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        private static Dictionary<string, UnityEngine.Object> ResourceCache { get; set; } = new Dictionary<string, UnityEngine.Object>();
        private void Awake()
        {
            //            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene previous, Scene next)
        {
            if (!string.IsNullOrEmpty(previous.name))
            {
                ResourceCache.Clear();
            }
        }

        public void LoadResourceAsync<TResource>(string value, string downloadPath, Action<TResource> callback) where TResource : UnityEngine.Object
        {
            UnityEngine.Object resource = null;
            var dictionaryKey = string.Equals(value, StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase)
                ? downloadPath
                : value;
            if (string.IsNullOrEmpty(dictionaryKey) && typeof(TResource) == typeof(Sprite))
            {
                callback(GameManager.Instance.NoImageSprite as TResource);
                return;
            }
            if (ResourceCache.TryGetValue(dictionaryKey, out resource))
            {
                callback(resource as TResource);
            }
            else
            {
                if (string.Equals(value, StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dictionaryKey))
                    {
                        callback(null);
                        return;
                    }
                    if (typeof(TResource) == typeof(Sprite))
                    {
                        //ProTODO: Download Sprite
                    }
                    else if (typeof(TResource) == typeof(AudioClip))
                    {
                        //ProTODO: Download AudioClip
                    }
                    else
                    {
                        callback(null);
                    }
                }
                else
                {
                    StartCoroutine(DoLoadResAsync<TResource>(dictionaryKey, result =>
                    {
                        if (result)
                        {
                            ResourceCache.Add(dictionaryKey, result);
                        }
                        callback(result);
                    }));
                }
            }
        }

        public static TResource LoadResource<TResource>(string key) where TResource : UnityEngine.Object
        {
            UnityEngine.Object resource = null;
            if (ResourceCache.TryGetValue(key, out resource))
            {
                return resource as TResource;
            }
            var loadedResource = Resources.Load<TResource>(key);
#if !UNITY_EDITOR && !UNITY_EDITOR_64
            ResourceCache.Add(key, loadedResource);
#endif 
            return loadedResource;
        }
        private IEnumerator DoLoadResAsync<TResource>(string path, Action<TResource> callback) where TResource : UnityEngine.Object
        {
            var resource = Resources.LoadAsync<TResource>(path);
            yield return resource;
            callback(resource.asset as TResource);
        }
        private IEnumerator GetAudioClip(string url, Action<AudioClip> callback)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    clip.name = url;
                    callback(clip);
                }
            }
        }
        private void FetchSprite(string imageUrl, Action<string, Sprite> callback)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                //callback(imageUrl, R.Sprites.spr_NoImage);
            }
            else
            {
                try
                {
                    Uri url = new Uri(imageUrl);
                    StartCoroutine(GetTexture(url.OriginalString, (resultUrl, texture) =>
                        {
                            if (texture)
                            {
                                Texture2D texture2D = (Texture2D)texture;
                                var spr = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                                callback(url.OriginalString, spr);
                            }
                            else
                            {
                                callback(url.OriginalString, null);
                            }
                        }));
                }
                catch (Exception)
                {
                    callback(imageUrl, null);
                }
            }
        }

        private static IEnumerator GetTexture(string url, Action<string, Texture> texture)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            texture(url, DownloadHandlerTexture.GetContent(www));
        }


    }
}
