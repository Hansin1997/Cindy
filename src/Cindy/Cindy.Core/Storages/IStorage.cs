using System;
using System.Collections.Generic;

namespace Cindy.Storages
{
    /// <summary>
    /// 存储器接口
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        void Get(string key, UnityEngine.MonoBehaviour context, ResultAction<string, Exception> resultAction);
        /// <summary>
        /// 存储一个数据
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="value">数据值</param>
        /// <param name="action">异步回调</param>
        void Put(string key, UnityEngine.MonoBehaviour context, object value, BoolAction<Exception> action);
        /// <summary>
        /// 获取多个数据
        /// </summary>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <param name="keys">键名数组</param>
        void GetMultiple(UnityEngine.MonoBehaviour context, ResultAction<string[], Exception> resultAction, Action<float> progess, params string[] keys);
        /// <summary>
        /// 存储多个数据
        /// </summary>
        /// <param name="keys">键名数组</param>
        /// <param name="values">值数组</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        void PutMultiple(string[] keys, object[] values, UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess);
        /// <summary>
        /// 存储多个数据
        /// </summary>
        /// <param name="keyValuePairs">数据字典</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        void PutMultiple(IDictionary<string, object> keyValuePairs, UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess);
        /// <summary>
        /// 清除所有数据
        /// </summary>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        void Clear(UnityEngine.MonoBehaviour context, BoolAction<Exception> action);
    }
}
