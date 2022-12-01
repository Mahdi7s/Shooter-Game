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
    /// M4uActiveBinding. Bind GameObject.activeSelf
    /// </summary>
	[AddComponentMenu("M4u/ActiveBinding")]
    //ProSNY HACK
    [RequireComponent(typeof(TrixActiveBindingHandler))]
    public class M4uActiveBinding : M4uBindingBool
	{
		private GameObject ui = null;

        public override void Start ()
		{
			base.Start ();
		    var activeBindingHandler = GetComponent<TrixActiveBindingHandler>();
		    if (activeBindingHandler)
		    {
		        transform.localScale = activeBindingHandler.SavedScale;
		    }
			ui = gameObject;
			OnChange ();
		}

		public override void OnChange ()
		{
			base.OnChange ();
            
			ui.SetActive (IsCheck ());
		}

        public override string ToString()
        {
            return "GameObject.active=" + base.ToString();
        }
	}
}