using System;

namespace Cindy
{
    /// <summary>
    /// 布尔型委托
    /// </summary>
    /// <typeparam name="E">异常类型</typeparam>
    /// <param name="isSuccess">操作是否成功</param>
    /// <param name="exception">异常对象</param>
    public delegate void BoolAction<E>(bool isSuccess, E exception) where E : Exception;
}
