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
    /// M4uBindingMultiple. Bind multiple Path
    /// </summary>
	public class M4uBindingMultiple : M4uBinding
	{
		public string[] Path;

		public override void Awake ()
		{
			base.Awake ();

			this.Paths = Path;
			Values = new object[Paths.Length];
		}
	}
}