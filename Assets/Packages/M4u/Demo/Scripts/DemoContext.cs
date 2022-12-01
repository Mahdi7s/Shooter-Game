//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;

namespace M4u
{
    /// <summary>
    /// DemoContext
    /// </summary>
	public class DemoContext : M4uContext
	{
		public static readonly DemoContext Instance = new DemoContext();

		public DemoData Data { get { return DemoData.Instance; } }
		public SceneDemo2 Demo2 { get; set; }
        public SceneDemo3 Demo3 { get; set; }
        public SceneDemo4 Demo4 { get; set; }
		public SceneDemo5 Demo5 { get; set; }

		private DemoContext() {}
	}
}