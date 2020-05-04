using System;

namespace Cindy.Strings
{
    /// <summary>
    /// 文本源接口
    /// </summary>
    interface IStringSource
    {
        /// <summary>
        /// 获取一个字符串
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="context">上下文，用于创建协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        void Get(string key, UnityEngine.MonoBehaviour context, ResultAction<string, Exception> resultAction);
        /// <summary>
        /// 获取多个字符串
        /// </summary>
        /// <param name="keys">键名数组</param>
        /// <param name="context">上下文，用于创建协程等操作</param>
        /// <param name="resultAction">异步回调</param>
        void GetMultiple(string[] keys, UnityEngine.MonoBehaviour context, ResultAction<string[], Exception> resultAction);
    }
}
