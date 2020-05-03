using System;

namespace Cindy.Storages
{
    public interface IObjectStorage
    {
        void PutObjects(UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects);

        void LoadObjects(UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects);

        void Clear(UnityEngine.MonoBehaviour context, BoolAction<Exception> action);
    }

    public interface IStorableObject
    {
        string GetStorableKey();

        object GetStorableObject();

        Type GetStorableObjectType();

        void OnPutStorableObject(object obj);
    }
}
