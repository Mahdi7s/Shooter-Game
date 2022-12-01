using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("color", "time")]
	public class ES3Type_GradientColorKey : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3Type_GradientColorKey() : base(typeof(UnityEngine.GradientColorKey))
		{
			Instance = this;
		}

		public override void Write(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.GradientColorKey)obj;
			
			writer.WriteProperty("color", instance.color, ES3Type_Color.Instance);
			writer.WriteProperty("time", instance.time, ES3Type_float.Instance);
		}

		public override object Read<T>(ES3Reader reader)
		{
			var instance = new UnityEngine.GradientColorKey();
			ReadInto<T>(reader, instance);
			return instance;
		}

		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.GradientColorKey)obj;
			string propertyName;
			while((propertyName = reader.ReadPropertyName()) != null)
			{
				switch(propertyName)
				{
					
					case "color":
						instance.color = reader.Read<UnityEngine.Color>(ES3Type_Color.Instance);
						break;
					case "time":
						instance.time = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}

		public class ES3Type_GradientColorKeyArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_GradientColorKeyArray() : base(typeof(GradientColorKey[]), ES3Type_GradientColorKey.Instance)
		{
			Instance = this;
		}
	}
}