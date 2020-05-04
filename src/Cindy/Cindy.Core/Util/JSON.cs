using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Cindy
{
    /// <summary>
    /// Json工具类
    /// </summary>
    public class JSON
    {
        private static IJsonUtil instance;
        /// <summary>
        /// Json工具类实例
        /// </summary>
        public static IJsonUtil Instance { get { return instance == null ? (instance = new NewtonsoftJson()) : instance; } set { instance = value; } }

        /// <summary>
        /// 序列化一个对象为Json字符串。
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(object obj)
        {
            return Instance.ToJson(obj);
        }
        /// <summary>
        /// 从Json字符串反序列化一个对象。
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <param name="type">目标对象类型</param>
        /// <returns>反序列化的对象</returns>
        public static object FromJson(string json, Type type)
        {
            return Instance.FromJson(json, type);
        }
        /// <summary>
        /// 从Json字符串反序列化一个对象。
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>反序列化的对象</returns>
        public static T FromJson<T>(string json)
        {
            return Instance.FromJson<T>(json);
        }
    }

    /// <summary>
    /// Json操作接口
    /// </summary>
    public interface IJsonUtil
    {
        /// <summary>
        /// 序列化一个对象为Json字符串。
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <returns>Json字符串</returns>
        string ToJson(object obj);
        /// <summary>
        /// 从Json字符串反序列化一个对象。
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <param name="type">目标对象类型</param>
        /// <returns>反序列化的对象</returns>
        object FromJson(string json, Type type);
        /// <summary>
        /// 从Json字符串反序列化一个对象。
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>反序列化的对象</returns>
        T FromJson<T>(string json);
    }

    public class UnityJson : IJsonUtil
    {
        public object FromJson(string json, Type type)
        {
            return JsonUtility.FromJson(json, type);
        }

        public T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }

    public class NewtonsoftJson : IJsonUtil
    {
        public object FromJson(string json, Type type)
        {
            if (type.Equals(typeof(bool)))
                json = json.ToLower(); // 解决bool值首字母大写异常
            return JsonConvert.DeserializeObject(json, type);
        }

        public T FromJson<T>(string json)
        {
            if (typeof(T).Equals(typeof(bool)))
                json = json.ToLower(); // 解决bool值首字母大写异常
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
