//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;

namespace M4u
{
    /// <summary>
    /// DemoContextMono
    /// </summary>
	public class DemoContextMono : M4uContextMonoBehaviour
	{
		private M4uProperty<Vector3> charaRot = new M4uProperty<Vector3> ();

		public Vector3 CharaRot { get { return charaRot.Value; } set { charaRot.Value = value; } }
	}
}