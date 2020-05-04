using System;

namespace Cindy.Storages
{
    /// <summary>
    /// 对象存储器接口
    /// </summary>
    public interface IObjectStorage
    {
        /// <summary>
        /// 存储多个对象
        /// </summary>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <param name="storableObjects">可存储对象数组</param>
        void PutObjects(UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects);
        /// <summary>
        /// 还原多个对象
        /// </summary>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <param name="storableObjects">可存储对象数组</param>
        void RestoreObjects(UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects);
        /// <summary>
        /// 清除所有数据
        /// </summary>
        /// <param name="context">上下文，用于开始协程等操作</param>
        /// <param name="action">异步回调</param>
        void Clear(UnityEngine.MonoBehaviour context, BoolAction<Exception> action);
    }
    /// <summary>
    /// 可存储对象接口
    /// </summary>
    public interface IStorableObject
    {
        /// <summary>
        /// 获取存储键名
        /// </summary>
        /// <returns>数据键名</returns>
        string GetStorableKey();
        /// <summary>
        /// 获取存储对象
        /// </summary>
        /// <returns>将要被序列化存储的对象</returns>
        object GetStorableObject();
        /// <summary>
        /// 获取存储对象类型
        /// </summary>
        /// <returns>存储对象的类型</returns>
        Type GetStorableObjectType();
        /// <summary>
        /// 还原存储对象
        /// </summary>
        /// <param name="obj">反序列化的对象</param>
        void OnStorableObjectRestore(object obj);
    }
}
