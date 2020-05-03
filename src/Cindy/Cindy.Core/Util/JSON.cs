using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Cindy
{
    public class JSON
    {
        private static JsonUtil instance;
        public static JsonUtil Instance { get { return instance == null ? (instance = new NewtonsoftJson()) : instance; } set { instance = value; } }

        public static string ToJson(object obj)
        {
            return Instance.ToJson(obj);
        }

        public static object FromJson(string json, Type type)
        {
            return Instance.FromJson(json, type);
        }

        public static T FromJson<T>(string json)
        {
            return Instance.FromJson<T>(json);
        }
    }

    public abstract class JsonUtil
    {
        public abstract string ToJson(object obj);
        public abstract object FromJson(string json, Type type);
        public abstract T FromJson<T>(string json);
    }

    public class UnityJson : JsonUtil
    {
        public override object FromJson(string json, Type type)
        {
            return JsonUtility.FromJson(json, type);
        }

        public override T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public override string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }

    public class NewtonsoftJson : JsonUtil
    {
        public override object FromJson(string json, Type type)
        {
            if (type.Equals(typeof(bool)))
                json = json.ToLower(); // 解决bool值首字母大写异常
            return JsonConvert.DeserializeObject(json, type);
        }

        public override T FromJson<T>(string json)
        {
            if (typeof(T).Equals(typeof(bool)))
                json = json.ToLower(); // 解决bool值首字母大写异常
            return JsonConvert.DeserializeObject<T>(json);
        }

        public override string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
