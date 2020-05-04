using System;

namespace Cindy
{
    /// <summary>
    /// 通用委托
    /// </summary>
    /// <typeparam name="R">结果类型</typeparam>
    /// <typeparam name="E">异常类型</typeparam>
    /// <param name="result">结果对象</param>
    /// <param name="exception">异常对象</param>
    /// <param name="isSuccess">操作是否成功</param>
    public delegate void ResultAction<R, E>(R result, E exception, bool isSuccess) where E : Exception;
}
