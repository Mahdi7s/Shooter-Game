using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameFactory
{
    private static Dictionary<Type, IFactoryObject> _dict = new Dictionary<Type, IFactoryObject>();
    private static Dictionary<string, GameObject> _referencesDictionary = new Dictionary<string, GameObject>();

    public static void AddSingleton<T>(T singleton) where T : class
    {
        _dict[typeof(T)] = new FactoryObject<T> { IsSingleton = true, Instance = singleton };
    }
    public static void AddSingleton<T>(Func<T> factory) where T : class
    {
        _dict[typeof(T)] = new FactoryObject<T> { IsSingleton = true, Factory = factory };
    }

    public static void AddTransient<T>() where T : class, new()
    {
        AddTransient(() => new T());
    }
    public static void AddTransient<T>(Func<T> factory) where T : class, new()
    {
        _dict[typeof(T)] = new FactoryObject<T> { IsSingleton = false, Factory = factory };
    }

    public static void AddGameObject(ReferencesName referenceName, GameObject referenceGameObject)
    {
        _referencesDictionary[referenceName.ToString()] = referenceGameObject;
    }

    public static T Get<T>() where T : class
    {
        return (T)_dict[typeof(T)].GetObject();
    }
    public static bool TryGetGameObject(ReferencesName referenceName, out GameObject referenceGameObject)
    {
        return _referencesDictionary.TryGetValue(referenceName.ToString(), out referenceGameObject);
    }
    private interface IFactoryObject
    {
        object GetObject();
    }

    private sealed class FactoryObject<T> : IFactoryObject where T : class
    {
        public object Instance { get; set; }
        public Func<T> Factory { get; set; }
        public bool IsSingleton { get; set; }

        public object GetObject()
        {
            object retval = null;
            if (IsSingleton)
            {
                if (Instance != null)
                {
                    retval = Instance;
                }
                else
                {
                    Instance = Factory();
                    retval = Instance;
                }
            }
            else
            {
                retval = Factory();
            }

            return retval;
        }
    }
}
