using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonContainer : MonoBehaviour
{
    private static Dictionary<Type, object> _container = new Dictionary<Type, object>();

    public static void Register(object instance)
    {
        Type type = instance.GetType();
        _container[type] = instance;
    }

    public static T Resolve<T>()
    {
        object result = _container[typeof(T)];
        if(result != null)
            return (T)result;
        else
        {
            Debug.Log("No instance of type " + typeof(T) + " found");
            return default;
        }
    }

    public static void UnRegister(object instance)
    {
        Type type = instance.GetType();
        _container.Remove(type);
    }
}
