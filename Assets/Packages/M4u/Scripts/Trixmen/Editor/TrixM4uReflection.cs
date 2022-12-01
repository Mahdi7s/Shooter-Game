using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using M4u;
using UnityEngine;

namespace Packages.M4u.Scripts.Trixmen.Editor
{
    public static class TrixM4UReflection
    {
        private static List<string> _propPaths = null;
        private static List<string> _eventPaths = null;
        private static List<Type> _propTypes = new List<Type>();

        public static void Reset()
        {
            _propPaths = null;
            _eventPaths = null;
            _propTypes = new List<Type>();
        }

        public static List<Type> PropTypes => _propTypes;

        public static List<string> GetRootContextMenu(GameObject gameObject, Type propDeclaringType)
        {
            if (!gameObject)
                return null;
            var isEventBinding = propDeclaringType == typeof(M4uEventBinding);

            //if ((isEventBinding && _eventPaths == null) || !isEventBinding && _propPaths == null)
            //{
                var rootCtx = gameObject.GetComponentsInParent<M4uContextRoot>().FirstOrDefault(x => x.ContextMonoBehaviour != null);
                if (rootCtx != null)
                {
                    if (isEventBinding)
                    {
                        _eventPaths = GetTypeIncludes(rootCtx.ContextMonoBehaviour.GetType(), true);
                    }
                    else
                    {
                        _propPaths = GetTypeIncludes(rootCtx.ContextMonoBehaviour.GetType(), false);
                    }
                }
            //}
            return isEventBinding ? _eventPaths : _propPaths;
        }

        private static List<string> GetTypeIncludes(Type type, bool isEventBinding)
        {
            var typeProperties = type.GetProperties().Where(x => !x.PropertyType.IsEnum && x.PropertyType.IsClass && IsContext(x.PropertyType)).ToList();

            var returnValues = new List<string>();
            returnValues.AddRange(isEventBinding ? GetEventProps(type) : GetBindingProps(type));

            foreach (var typeProperty in typeProperties)
            {
                returnValues.AddRange(isEventBinding
                    ? GetEventProps(typeProperty.PropertyType, typeProperty.Name)
                    : GetBindingProps(typeProperty.PropertyType, typeProperty.Name));
            }
            return returnValues;
        }

        private static List<string> GetBindingProps(Type type, string prefix = "")
        {
            var typeFields = type.GetFields(M4uConst.BindingAttr).Where(x => x.FieldType.IsGenericType && x.FieldType.GetGenericTypeDefinition() == typeof(TrixProp<>)).ToList();
            var returnValues = new List<string>();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propField = typeFields.FirstOrDefault(f => string.Equals(f.Name, $"_{prop.Name}", StringComparison.CurrentCultureIgnoreCase));

                if (propField != null && char.IsUpper(prop.Name[0]))
                {
                    if (IsContext(prop.PropertyType))
                    {
                        returnValues.AddRange(GetBindingProps(prop.PropertyType, prefix + "/" + prop.Name));
                    }
                    else
                    {
                        returnValues.Add(string.IsNullOrEmpty(prefix) ? prop.Name : prefix + "/" + prop.Name);

                        _propTypes.Add(prop.PropertyType);

                        // ---------------------------------------------------------------------------------------------
                        var arg0 = propField.FieldType.GetGenericArguments()[0];
                        if (arg0.IsGenericType && arg0.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            returnValues.AddRange(GetBindingProps(arg0.GetGenericArguments()[0],
                                "★★★ Collection Bindings/" + arg0.GetGenericArguments()[0].Name));
                        }
                    }
                }
            }

            return returnValues;
        }

        private static List<string> GetEventProps(Type type, string prefix = "", List<string> list = null)
        {
            if (list == null) list = new List<string>();

            list.AddRange(GetTypeEvents(type, prefix));
            if (IsContext(type))
            {
                foreach (var tProperty in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (IsContext(tProperty.PropertyType))
                    {
                        var events = GetTypeEvents(tProperty.PropertyType, prefix + "/" + tProperty.Name);
                        list.AddRange(events);

                        GetEventProps(tProperty.PropertyType,
                            string.IsNullOrEmpty(prefix) ? tProperty.Name : prefix + "/" + tProperty.Name, list);
                    }
                    else
                    {
                        if (tProperty.PropertyType.IsGenericType && tProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            list.AddRange(GetTypeEvents(tProperty.PropertyType.GetGenericArguments()[0],
                                "★★★ Collection Bindings/" + tProperty.PropertyType.GetGenericArguments()[0].Name));
                        }
                    }
                }
            }

            return list;
        }

        private static List<string> GetTypeEvents(Type type, string prefix = "")
        {
            return type.GetMethods().Where(x => x.Name.StartsWith("On") && x.Name.EndsWith("Event"))
                .Select(prop => (string.IsNullOrEmpty(prefix) ? prop.Name : prefix + "/" + prop.Name)).ToList();
        }

        private static bool IsContext(Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(ITrixContext));
        }

        private static bool IsGenericList(Type type)
        {
            return type.IsGenericType && type.GetInterfaces().Any(x => x.IsGenericType &&
                                                                       (x.GetGenericTypeDefinition() == typeof(IEnumerable<>) || x.GetGenericTypeDefinition() == typeof(IQueryable<>)));
        }
    }
}
