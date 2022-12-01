//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace M4u
{
    /// <summary>
    /// M4uConst
    /// </summary>
	public static class M4uConst
	{
		public static readonly BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
	}

    /// <summary>
    /// bool check type
    /// </summary>
	public enum BoolCheckType
	{
		Bool,
        Equal,
		Greater,
        GreaterEqual,//ProSNY HACK
        Less,
        LessEqual,//ProSNY HACK
        Empty,
        String,
        Enum,
	}

    /// <summary>
    /// transform type
    /// </summary>
	public enum TransformType
	{
		Px, Py, Pz,
		Rx, Ry, Rz,
		Sx, Sy, Sz,
	}

    /// <summary>
    /// transform local type
    /// </summary>
	public enum TransformLocalType
	{
		Postion,
		Rotation,
		Scale,
	}

	/// <summary>
	/// UnityEvent type
	/// </summary>
	public enum M4uEventType : int
	{
		ButtonClick = 0,
		ToggleValueChanged = 10,
		SliderValueChanged = 20,
		ScrollbarValueChanged = 30,
		DropdownValueChanged = 40,
		InputFieldEndEdit = 50,
		ScrollRectValueChanged = 60,
		EventTrigger = 100,
        //HACK : Added by sina tahoori for input field value changed event
        InputFieldValueChanged = 110
	}
}