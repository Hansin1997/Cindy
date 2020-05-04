using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cindy.Util
{
    /// <summary>
    /// Finder类，用于场景对象的搜索。
    /// </summary>
    public class Finder
    {
        /// <summary>
        /// 额外过滤器，用于检查一个对象是否满足要求
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <returns>是否满足要求</returns>
        public delegate bool Filter<T>(T target);

        /// <summary>
        /// 在场景或者资源中搜索一个对象。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="name">目标的<c>name</c></param>
        /// <param name="includingResources">当场景中未找到时是否在资源中搜索</param>
        /// <param name="filter">当名称匹配时用于检查的额外过滤器</param>
        /// <param name="type">当提供了类型时，将使用该类型参数而不再通过泛型类型来搜索对象，但返回值依然为泛型类型</param>
        /// <returns>找到的对象</returns>
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

        /// <summary>
        /// 在资源中搜索一个对象。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="name">目标的<c>name</c></param>
        /// <param name="filter">当名称匹配时用于检查的额外过滤器</param>
        /// <param name="type">当提供了类型时，将使用该类型参数而不再通过泛型类型来搜索对象，但返回值依然为泛型类型</param>
        /// <returns>找到的对象</returns>
        public static T FindResource<T>(string name, Filter<T> filter = null, Type type = null) where T : Object
        {
            if (name == null)
                return null;
            T result = type == null ? Resources.Load<T>(name) : Resources.Load(name, type) as T;
            if (filter == null || result == null)
                return result;
            return filter(result) ? result : null;
        }

        /// <summary>
        /// 在资源中加载指定路径的所有对象。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="path">目标路径</param>
        /// <param name="type">当提供了类型时，将使用该类型参数而不再通过泛型类型来搜索对象，但返回值依然为泛型类型</param>
        /// <returns>找到的对象数组</returns>
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
