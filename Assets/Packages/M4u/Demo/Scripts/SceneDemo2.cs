//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// SceneDemo2. All Bind Demo
    /// </summary>
	public class SceneDemo2 : MonoBehaviour
	{
		public M4uContextRoot ContextRoot;
		public M4uContextRoot ContextMonoRoot;
		public DemoContext DemoContext;
		public DemoContextMono DemoContextMono;

		private M4uProperty<int> charaX = new M4uProperty<int> ();
		private M4uProperty<int> charaY = new M4uProperty<int> ();

		public int CharaX { get { return charaX.Value; } set { charaX.Value = value; } }
		public int CharaY { get { return charaY.Value; } set { charaY.Value = value; } }

		void Awake()
		{
			DemoContext.Instance.Demo2 = this;
			ContextRoot.Context = DemoContext.Instance;
			DemoContext = DemoContext.Instance;

			OnClickUpdate ();
		}

		public void OnClickUpdate()
		{
			DemoData d = DemoContext.Instance.Data;
			d.UserName = "yedo" + M4uUtil.Random(1, 100);
			d.UserId = M4uUtil.Random(1, 100);
			d.CharaSprite = M4uUtil.LoadSprite ("CommonAtlas", ("common_button_" + M4uUtil.Random(1, 3)));
			d.Atk = M4uUtil.Random(0, 100);
			d.Def = M4uUtil.Random(0, 100);
			d.BackgroundTexture = M4uUtil.LoadTexture ("banner_" + M4uUtil.Random(1, 4));
			d.SoundVolume = M4uUtil.Random (0f, 1f);
			d.ScrollValue = M4uUtil.Random (0f, 1f);
			d.IsSet = (M4uUtil.Random(0, 2) == 0);
			d.Description = string.Format("My name is {0}!", d.UserName);
			d.ProgressTitle = "Now progress is ";
			d.Progress = M4uUtil.Random (0, 100);

			this.CharaX = M4uUtil.Random (-300, 300);
			this.CharaY = M4uUtil.Random (-430, -360);

			Vector3 rot = DemoContextMono.CharaRot;
			DemoContextMono.CharaRot = new Vector3 (rot.x, rot.y, M4uUtil.Random (-180, 180));
		}
	}
}