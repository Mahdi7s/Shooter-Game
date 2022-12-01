//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace M4u
{
    /// <summary>
    /// M4uBindingSingle. Bind single Path
    /// </summary>
	public class M4uBindingSingle : M4uBinding
	{
		[TrixM4uPath]
		public string Path;

		public override void Awake ()
		{
			base.Awake ();
		    var tmpPath = Path.StartsWith("★★★ Collection Bindings") ? Path.Split('.').Last() : Path;
			Paths = new string[] { tmpPath };
			Values = new object[1];
		}
	}
}