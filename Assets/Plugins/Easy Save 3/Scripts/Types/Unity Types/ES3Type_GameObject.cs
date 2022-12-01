using System;
using UnityEngine;
using System.Collections.Generic;
using ES3Internal;

namespace ES3Types
{
	[ES3PropertiesAttribute("layer", "isStatic", "tag", "name", "hideFlags", "children", "components")]
	public class ES3Type_GameObject : ES3Type
	{
		private const string prefabPropertyName = "es3Prefab";
		private const string transformPropertyName = "transformID";
		public static ES3Type Instance = null;

		public ES3Type_GameObject() : base(typeof(UnityEngine.GameObject)){ Instance = this; }

		public override void Write(object obj, ES3Writer writer)
		{
			if(WriteUsingDerivedType(obj, writer))
				return;

			var instance = (UnityEngine.GameObject)obj;

			if(ES3ReferenceMgrBase.Current != null)
			{
				var es3Prefab = instance.GetComponent<ES3Prefab>();
				if(es3Prefab != null)
					writer.WriteProperty(prefabPropertyName, es3Prefab, ES3Type_ES3PrefabInternal.Instance);
				else
				{
					writer.WriteRef(instance);
					// Write the ID of this Transform so we can assign it's ID when we load.
					writer.WriteProperty(transformPropertyName, ES3ReferenceMgrBase.Current.Add(instance.transform));
				}
			}

			var es3AutoSave = instance.GetComponent<ES3AutoSave>();

			//if(es3AutoSave == null || es3AutoSave.saveLayer)
				writer.WriteProperty("layer", instance.layer, ES3Type_int.Instance);
			//if(es3AutoSave == null || es3AutoSave.saveTag)
				writer.WriteProperty("tag", instance.tag, ES3Type_string.Instance);
			//if(es3AutoSave == null || es3AutoSave.saveName)
				writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
			//if(es3AutoSave == null || es3AutoSave.saveHideFlags)
				writer.WriteProperty("hideFlags", instance.hideFlags);
			if(es3AutoSave == null || es3AutoSave.saveChildren)
				writer.WriteProperty("children", GetChildren(instance));

			var components = new List<Component>();
			foreach (var component in instance.GetComponents<Component>())
			{
				var componentType = component.GetType();
				// Only save explicitly-supported Components, /*or those explicitly marked as Serializable*/.
				if(ES3TypeMgr.GetES3Type(componentType) != null /*|| ES3Reflection.AttributeIsDefined(componentType, ES3Reflection.serializableAttributeType)*/)
					components.Add(component);
			}

			writer.WriteProperty("components", components);
		}

		public override object Read<T>(ES3Reader reader)
		{
			UnityEngine.Object obj = null;

			// Read the intial properties regarding the instance we're loading.
			while(true)
			{
				var refMgr = ES3ReferenceMgrBase.Current;
				if(refMgr == null)
				{
					reader.Skip();
					continue;
				}

				var propertyName = ReadPropertyName(reader);
				if(propertyName == ES3Type.typeFieldName)
					return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType()).Read<T>(reader);
				
				else if(propertyName == ES3ReferenceMgrBase.referencePropertyName)
				{
					if(refMgr == null)
					{
						reader.Skip();
						continue;
					}

					long id = reader.Read<long>(ES3Type_long.Instance);
					obj = refMgr.Get(id);

					if(obj == null)
					{
						// If an instance isn't already registered for this object, create an instance and register the reference.
						obj = new GameObject();
						refMgr.Add(obj, id);
					}
					else if(!ES3Reflection.IsAssignableFrom(typeof(T), obj.GetType()))
						throw new MissingReferenceException("The instance with ID \""+id+"\" is a different type than expected. Expected \""+typeof(T)+"\", found \""+obj.GetType()+"\"");
				}

				else if (propertyName == transformPropertyName)
				{
					if(refMgr == null)
					{
						reader.Skip();
						continue;
					}

					// Now load the Transform's ID and assign it to the Transform of our object.
					long transformID = reader.Read<long>(ES3Type_long.Instance);
					refMgr.Add(((GameObject)obj).transform, transformID);
				}

				else if (propertyName == prefabPropertyName)
				{
					if(ES3ReferenceMgrBase.Current == null)
						reader.Skip();
					else
						obj = reader.Read<GameObject>(ES3Type_ES3PrefabInternal.Instance);
				}
				else if (propertyName == null)
					return obj;
				else
				{
					reader.overridePropertiesName = propertyName;
					break;
				}
			}

			if(obj == null)
				obj = new GameObject();

			ReadInto<T>(reader, obj);
			return obj;
		}
		
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.GameObject)obj;

			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					case "prefab":
						break;
					case "layer":
						instance.layer = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "tag":
						instance.tag = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "name":
						instance.name = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "hideFlags":
						instance.hideFlags = reader.Read<UnityEngine.HideFlags>();
						break;
					case "children":
						reader.Read<GameObject[]>();
						break;
					case "components":
						reader.Read<Component[]>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		/*
		 * 	Gets the direct children of this GameObject.
		 */
		public static List<GameObject> GetChildren(GameObject go)
		{
			var goTransform = go.transform;
			var children = new List<GameObject>();

			foreach(Transform child in goTransform)
				// If a child has an Auto Save component, let it save itself.
				if(child.GetComponent<ES3AutoSave>() == null)
					children.Add(child.gameObject);

			return children;
		}
	}

		public class ES3Type_GameObjectArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_GameObjectArray() : base(typeof(UnityEngine.GameObject[]), ES3Type_GameObject.Instance)
		{
			Instance = this;
		}
	}
}