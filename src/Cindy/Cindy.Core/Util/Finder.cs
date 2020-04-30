using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cindy.Util
{
    public class Finder
    {
        public delegate bool Filter<T>(T target);

        public static T Find<T>(string name = null,bool includingResources = true,Filter<T> filter = null,Type type = null) where T : Object
        {
            if (name == null)
                return type == null ? Object.FindObjectOfType<T>() : Object.FindObjectOfType(type) as T;
            if(type == null)
            {
                T[] objects = Object.FindObjectsOfType<T>();
                foreach (T obj in objects)
                {
                    if (obj.name.Equals(name) && (filter == null || filter(obj)))
                        return obj;
                }
            }
            else
            {

                Object[] objects = Object.FindObjectsOfType(type);
                foreach (Object obj in objects)
                {
                    if (obj is T t && t.name.Equals(name) && (filter == null || filter(t)))
                        return t;
                }
            }
            return includingResources ? FindResource(name, filter, type) : null;
        }

        public static T FindResource<T>(string name, Filter<T> filter = null, Type type = null) where T : Object
        {
            if (name == null)
                return null;
            T result = type == null ? Resources.Load<T>(name) : Resources.Load(name, type) as T;
            if (filter == null || result == null)
                return result;
            return filter(result) ? result : null;
        }

        public static T[] LoadResourceAll<T>(string path, Type type = null) where T : Object
        {
            if(type == null)
                return Resources.LoadAll<T>(path);
            List<T> result = new List<T>();
            Object[] objects = Resources.LoadAll(path, type);
            foreach(Object obj in objects)
            {
                if (obj is T t)
                    result.Add(t);
            }
            return result.ToArray();
        }
    }
}
