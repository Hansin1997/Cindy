using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 数据源接口
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// 获取一项数据。
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        void GetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction);
        /// <summary>
        /// 获取多个数据。
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="keys">键名数组</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        void GetDataMultiple<T>(string[] keys, MonoBehaviour context, ResultAction<T[], Exception> resultAction);
        /// <summary>
        /// 设置一项数据。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        void SetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action);
        /// <summary>
        /// 设置多项数据。
        /// </summary>
        /// <param name="keys">键名数组</param>
        /// <param name="values">值数组</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        void SetDataMultiple(string[] keys, object[] values, MonoBehaviour context, BoolAction<Exception> action);
        /// <summary>
        /// 设置多项数据。
        /// </summary>
        /// <param name="keyValuePairs">数据字典</param>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action"></param>
        void SetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action);
        /// <summary>
        /// 判断数据源是否可读。
        /// </summary>
        /// <returns>数据源是否可读</returns>
        bool IsReadable();
        /// <summary>
        /// 判断数据源是否可写。
        /// </summary>
        /// <returns>数据源是否可写</returns>
        bool IsWriteable();
    }
}