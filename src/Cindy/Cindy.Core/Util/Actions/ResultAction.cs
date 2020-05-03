using System;

namespace Cindy
{
    public delegate void ResultAction<R, E>(R result, E exception, bool isSuccess) where E : Exception;
}
