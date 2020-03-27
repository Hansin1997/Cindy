using UnityEngine;

namespace Cindy.Util
{
    public class Finder
    {
        public delegate bool Filter<T>(T target);

        public static T Find<T>(string name = null,bool includingResources = true,Filter<T> filter = null) where T : Object
        {
            if (name == null)
                return Object.FindObjectOfType<T>();
            T[] objects = Object.FindObjectsOfType<T>();
            foreach(T obj in objects)
            {
                if (obj.name.Equals(name) && (filter == null || filter(obj)))
                    return obj;
            }
            return includingResources ? FindResource(name, filter) : null;
        }

        public static T FindResource<T>(string name, Filter<T> filter = null) where T : Object
        {
            if (name == null)
                return null;
            T result = Resources.Load<T>(name);
            if (filter == null || result == null)
                return result;
            return filter(result) ? result : null;
        }

        public static T[] LoadResourceAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}
