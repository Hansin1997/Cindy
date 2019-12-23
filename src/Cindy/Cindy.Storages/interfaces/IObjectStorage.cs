using System;

namespace Cindy.Storages
{
    public interface IObjectStorage
    {
        void PutObjects(params IStorableObject[] storableObjects);

        void LoadObjects(params IStorableObject[] storableObjects);

        void Clear();
    }

    public interface IStorableObject
    {
        string GetStorableKey();

        object GetStorableObject();

        Type GetStorableObjectType();

        void OnPutStorableObject(object obj);
    }
}
