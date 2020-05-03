using System;

namespace Cindy
{
    public delegate void BoolAction<E>(bool isSuccess, E exception) where E : Exception;
}
