using System;
using UnityEngine;

namespace ES3Types
{
	public class ES3Type_enum : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3Type_enum(Type type) : base(type)
		{
			isPrimitive = true;
			Instance = this;
		}

		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((int)obj);
		}

		public override object Read<T>(ES3Reader reader)
		{
			return (T)(object)reader.Read_int();
		}
	}
}