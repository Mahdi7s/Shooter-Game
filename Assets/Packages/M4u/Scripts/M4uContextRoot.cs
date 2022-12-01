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
    /// M4uContextRoot. M4u core script
    /// </summary>
	[AddComponentMenu("M4u/ContextRoot")]
	public class M4uContextRoot : MonoBehaviour
	{
        public string ContextName;
		public M4uContextMonoBehaviour ContextMonoBehaviour;

		private M4uContextInterface context = null;

        /// <summary>
        /// Context. Data Binding to View
        /// </summary>
		public M4uContextInterface Context 
        {
            get { return context; } 
            set 
            {
                context = value;
                if (context != null) { ContextName = context.ToString(); }
            }
        }

		void Awake()
		{
			if (ContextMonoBehaviour != null)
			{
                Context = ContextMonoBehaviour;
			}
		}
	}
}