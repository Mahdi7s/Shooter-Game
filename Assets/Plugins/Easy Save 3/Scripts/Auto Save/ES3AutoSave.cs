using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ES3Types;
using ES3Internal;

public class ES3AutoSave : MonoBehaviour
{
	public bool saveChildren = false;

	/*public List<ES3SerializedComponent> components;
	public bool saveLayer = false;
	public bool saveTag = false;
	public bool saveName = false;
	public bool saveHideFlags = false;*/

	/*public ES3SerializedComponent GetSerializedComponent(Component c)
	{
		foreach(var component in components)
			if(component.component == c)
				return component;
		var newComponent = new ES3SerializedComponent(c);
		components.Add(newComponent);
		return newComponent;
	}

	[Serializable]
	public class ES3SerializedComponent
	{
		public Component component;
		public List<ES3SerializedMember> members = new List<ES3SerializedMember>();
		public bool expanded = false;

		// The concrete or reflected ES3Type we can use to serialise this Component.
		private ES3Type _es3Type = null;
		public ES3Type es3Type
		{
			get
			{
				// Get the ES3Type to use to serialise this Component.
				if(_es3Type == null)
				{
					// If no members are selected, we can attempt to serialise this Component using a concrete ES3Type,
					// or create a reflected ES3Type if one does not exist.
					if(members.Count > 0)
						_es3Type = ES3TypeMgr.GetOrCreateES3Type(component.GetType(), false);
					// Else we'll create a reflected ES3Type, using the selected members to determine what members to serialise.
					else
						_es3Type = new ES3ReflectedType (component.GetType(), GetMemberNames());
				}
				return _es3Type;
			}
		}

		public ES3SerializedComponent(Component component)
		{
			this.component = component;
		}

		// Gets an array containing all of the selected members for this Component.
		private string[] GetMemberNames()
		{
			string[] memberNames = new string[members.Count];
			for(int i = 0; i < members.Count; i++)
				memberNames [i] = members [i].name;
			return memberNames;
		}

		[Serializable]
		public class ES3SerializedMember
		{
			public string name;
			public bool isProperty = false;

			public ES3SerializedMember(string name, bool isProperty)
			{
				this.name = name;
				this.isProperty = isProperty;
			}
		}
	}*/
}