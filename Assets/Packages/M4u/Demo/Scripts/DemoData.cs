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
    /// DemoData
    /// </summary>
	public class DemoData
	{
		public static readonly DemoData Instance = new DemoData();

		private M4uProperty<string> userName = new M4uProperty<string> ();
		private M4uProperty<int> userId = new M4uProperty<int> ();
		private M4uProperty<int> atk = new M4uProperty<int> ();
		private M4uProperty<int> def = new M4uProperty<int> ();
		private M4uProperty<Sprite> charaSprite = new M4uProperty<Sprite> ();
		private M4uProperty<Texture> backgroundTexture = new M4uProperty<Texture> ();
		private M4uProperty<float> soundVolume = new M4uProperty<float> ();
		private M4uProperty<float> scrollValue = new M4uProperty<float> ();
		private M4uProperty<bool> isSet = new M4uProperty<bool> ();
		private M4uProperty<string> description = new M4uProperty<string> ();
		private M4uProperty<string> progressTitle = new M4uProperty<string> ();
		private M4uProperty<int> progress = new M4uProperty<int> ();

		public string UserName { get { return userName.Value; } set { userName.Value = value; } }
		public int UserId { get { return userId.Value; } set { userId.Value = value; } }
		public int Atk { get { return atk.Value; } set { atk.Value = value; } }
		public int Def { get { return def.Value; } set { def.Value = value; } }
		public Sprite CharaSprite { get { return charaSprite.Value; } set { charaSprite.Value = value; } }
		public Texture BackgroundTexture { get { return backgroundTexture.Value; } set { backgroundTexture.Value = value; } }
		public float SoundVolume { get { return soundVolume.Value; } set { soundVolume.Value = value; } }
		public float ScrollValue { get { return scrollValue.Value; } set { scrollValue.Value = value; } }
		public bool IsSet { get { return isSet.Value; } set { isSet.Value = value; } }
		public string Description { get { return description.Value; } set { description.Value = value; } }
		public string ProgressTitle { get { return progressTitle.Value; } set { progressTitle.Value = value; } }
		public int Progress { get { return progress.Value; } set { progress.Value = value; } }

		private DemoData() {}
	}
}