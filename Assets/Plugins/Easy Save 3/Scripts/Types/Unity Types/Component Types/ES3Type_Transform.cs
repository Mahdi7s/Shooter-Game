using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("localPosition","localRotation","localScale","parent")]
	public class ES3Type_Transform : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3Type_Transform() : base(typeof(UnityEngine.Transform))
		{
			Instance = this;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Transform)obj;

			writer.WriteProperty("localPosition", instance.localPosition);
			writer.WriteProperty("localRotation", instance.localRotation);
			writer.WriteProperty("localScale", instance.localScale);
			writer.WritePropertyByRef("parent", instance.parent);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Transform)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					case "localPosition":
						instance.localPosition = reader.Read<Vector3>();
						break;
					case "localRotation":
						instance.localRotation = reader.Read<Quaternion>();
						break;
					case "localScale":
						instance.localScale = reader.Read<Vector3>();
						break;
					case "parent":
						instance.SetParent(reader.Read<Transform>());
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}
}