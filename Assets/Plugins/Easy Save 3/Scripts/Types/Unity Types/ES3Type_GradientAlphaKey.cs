using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("alpha", "time")]
	public class ES3Type_GradientAlphaKey : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3Type_GradientAlphaKey() : base(typeof(UnityEngine.GradientAlphaKey))
		{
			Instance = this;
		}

		public override void Write(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.GradientAlphaKey)obj;
			
			writer.WriteProperty("alpha", instance.alpha, ES3Type_float.Instance);
			writer.WriteProperty("time", instance.time, ES3Type_float.Instance);
		}

		public override object Read<T>(ES3Reader reader)
		{
			var instance = new UnityEngine.GradientAlphaKey();
			ReadInto<T>(reader, instance);
			return instance;
		}

		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.GradientAlphaKey)obj;
			string propertyName;
			while((propertyName = reader.ReadPropertyName()) != null)
			{
				switch(propertyName)
				{
					
					case "alpha":
						instance.alpha = reader.Read<System.Single>(ES3Type_float.Instance);
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

		public class ES3Type_GradientAlphaKeyArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_GradientAlphaKeyArray() : base(typeof(GradientAlphaKey[]), ES3Type_GradientAlphaKey.Instance)
		{
			Instance = this;
		}
	}
}