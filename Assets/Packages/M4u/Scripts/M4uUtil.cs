//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// M4uUtil
    /// </summary>
    public static class M4uUtil
	{
        /// <summary>
        /// Get random value
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random value</returns>
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Get random value
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random value</returns>
        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Load Sprite
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Sprite</returns>
		public static Sprite LoadSprite(string fileName) 
		{
			return LoadSprite (fileName, null, true);
		}

        /// <summary>
        /// Load Sprite
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="spriteName">Sprite name</param>
        /// <returns>Sprite</returns>
		public static Sprite LoadSprite(string fileName, string spriteName) 
		{
			return LoadSprite (fileName, spriteName, false);
		}

        /// <summary>
        /// Load Sprite
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="spriteName">Sprite name</param>
        /// <param name="isSingle">true:single false:multiple</param>
        /// <returns>Sprite</returns>
		public static Sprite LoadSprite(string fileName, string spriteName, bool isSingle) 
		{
			if (isSingle)
			{
				return Resources.Load<Sprite>("Texture/" + fileName);
			}
			else
			{
				Sprite[] sprites = Resources.LoadAll<Sprite>("Atlas/" + fileName);
				return Array.Find<Sprite>(sprites, (s) => s.name.Equals(spriteName));
			}
		}
		
        /// <summary>
        /// Load Texture
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Texture</returns>
		public static Texture LoadTexture(string fileName)
		{
			return Resources.Load ("Texture/" + fileName) as Texture;
		}

        /// <summary>
        /// Create Texture2D
        /// </summary>
        /// <param name="data">Image byte data</param>
        /// <returns>Texture2D</returns>
        public static Texture2D CreateTexture2D(byte[] data = null)
        {
            return CreateTexture2D(0, 0, data);
        }

        /// <summary>
        /// Create Texture2D
        /// </summary>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <param name="data">Image byte data. null is ignored</param>
        /// <returns>Texture2D</returns>
        public static Texture2D CreateTexture2D(int width, int height, byte[] data = null)
        {
			var t = new Texture2D(width, height, TextureFormat.ARGB32, false);
            t.anisoLevel = 0;
            t.filterMode = FilterMode.Bilinear;
            t.mipMapBias = 0f;
            t.wrapMode = TextureWrapMode.Clamp;
            if (data != null)
            {
                t.LoadImage(data);
                t.Apply();
            }
            return t;
        }
    }
}