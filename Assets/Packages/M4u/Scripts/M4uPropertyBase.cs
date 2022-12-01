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
    /// M4uPropertyBase. Data Binding to View
    /// </summary>
	public class M4uPropertyBase
	{
		private List<M4uBinding> bindings = new List<M4uBinding>();

		public List<M4uBinding> Bindings 
		{
			get { return bindings; }
			set { bindings = value; }
		}
	}
}