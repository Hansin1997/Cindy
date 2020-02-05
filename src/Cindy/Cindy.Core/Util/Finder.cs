namespace Cindy.Util
{
    public class Finder
    {
        public static T Find<T>(string name = null) where T : UnityEngine.Object
        {
            if (name == null)
                return UnityEngine.Object.FindObjectOfType<T>();
            T[] objects = UnityEngine.Object.FindObjectsOfType<T>();
            foreach(T obj in objects)
            {
                if (obj.name.Equals(name))
                    return obj;
            }
            return null;
        }

        public static T FindResource<T>(string name = null) where T : UnityEngine.Object
        {
            T[] objects = UnityEngine.Resources.FindObjectsOfTypeAll<T>();
            if (name == null)
                return objects.Length > 0 ? objects[0] : null;
            foreach(T obj in objects)
            {
                if (obj.name.Equals(name))
                    return obj;
            }
            return null;

        }
    }
}
