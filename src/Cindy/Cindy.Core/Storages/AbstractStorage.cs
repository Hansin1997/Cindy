using Cindy.Logic.ReferenceValues;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cindy.Storages
{
    public abstract class AbstractStorage : ScriptableObject, IStorage, IObjectStorage
    {
        public abstract string Get(string key);
        public abstract void Put(string key, object value);
        public abstract string[] GetMultiple(params string[] keys);
        public abstract void PutMultiple(string[] keys, object[] values);
        public abstract void PutMultiple(IDictionary<string, object> keyValuePairs);

        public abstract void PutObjects(params IStorableObject[] storableObjects);
        public abstract void LoadObjects(params IStorableObject[] storableObjects);

        public abstract void Clear();

        protected virtual void OnException(Exception e)
        {
            Debug.LogError("[AbstracStorage Exception]: " + e);
        }

        public bool TryGetValue(string key,out string value)
        {
            try
            {
                value = Get(key);
                return true;
            }catch(Exception e)
            {
                value = default;
                OnException(e);
                return false;
            }
        }

        public bool TryGetInt(string key, out int value)
        {
            try
            {
                value = int.Parse(Get(key));
                return true;
            }
            catch (Exception e)
            {
                value = default;
                OnException(e);
                return false;
            }
        }

        public bool TryGetFloat(string key, out float value)
        {
            try
            {
                value = float.Parse(Get(key));
                return true;
            }
            catch (Exception e)
            {
                value = default;
                OnException(e);
                return false;
            }
        }

        public bool TryGetBool(string key, out bool value)
        {
            try
            {
                value = bool.Parse(Get(key));
                return true;
            }
            catch (Exception e)
            {
                value = default;
                OnException(e);
                return false;
            }
        }

    }

    public abstract class AbstractStorableObject : MonoBehaviour, IStorableObject
    {
        [SerializeField]
        public StorableObjectOption StorableOptions;

        protected StringBuilder stringBuilder;

        public string GetStorableKey()
        {
            if (stringBuilder == null)
                stringBuilder = new StringBuilder();
            else
                stringBuilder.Clear();
            stringBuilder.Append(GetType().FullName)
                .Append("_")
                .Append(name);
            if(!StorableOptions.globalUniqueness)
            {
                stringBuilder.Append("_").Append(gameObject.scene.name);
            }
            return stringBuilder.ToString();


        }
        public abstract object GetStorableObject();
        public abstract Type GetStorableObjectType();
        public abstract void OnPutStorableObject(object obj);
    }

    [Serializable]
    public class StorableObjectOption
    {
        public ReferenceString label = new ReferenceString() { value = "default" };
        public bool globalUniqueness = false;
    }
}
