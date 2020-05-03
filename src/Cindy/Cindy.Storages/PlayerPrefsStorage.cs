using Cindy.Util.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{
    [CreateAssetMenu(fileName = "PlayerPrefsStorage", menuName = "Cindy/Storage/PlayerPrefsStorage", order = 1)]
    public class PlayerPrefsStorage : AbstractStorage
    {
        public string prefix = "PREFIX_";

        protected const string keysKey = "#KEYS#";

        public IList<string> GetKeys()
        {
            IList<string> result = null;
            string json = PlayerPrefs.GetString(prefix + keysKey);
            if (json != null)
            {
                SerializedList serializedList = JSON.FromJson<SerializedList>(json);
                if (serializedList != null)
                    result = serializedList.ToList();
            }
            if (result != null)
                return result;
            return new List<string>();
        }

        public void SetKeys(IList<string> keys)
        {
            if (keys == null || keys.Count == 0)
                PlayerPrefs.DeleteKey(prefix + keysKey);
            else
            {
                PlayerPrefs.SetString(prefix + keysKey, JSON.ToJson(new SerializedList(keys)));
            }
        }

        public override IEnumerator DoClear(BoolAction<Exception> action)
        {
            try
            {
                IList<string> keys = GetKeys();
                foreach (string key in keys)
                {
                    PlayerPrefs.DeleteKey(prefix + key);
                }
                keys.Clear();
                SetKeys(keys);
                PlayerPrefs.Save();
                action?.Invoke(true, null);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoGet(string key, ResultAction<string, Exception> resultAction)
        {
            try
            {
                resultAction?.Invoke(PlayerPrefs.GetString(prefix + key), null, true);
            }catch(Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
            yield return null;
        }

        public override IEnumerator DoPut(string key, object value, BoolAction<Exception> action)
        {
            try
            {
                IList<string> keys = GetKeys();
                if (value == null)
                {
                    PlayerPrefs.DeleteKey(prefix + key);
                    keys.Remove(key);
                }
                else
                {
                    PlayerPrefs.SetString(prefix + key, value.ToString());
                    if (!keys.Contains(key))
                        keys.Add(key);
                }
                SetKeys(keys);
                PlayerPrefs.Save();
                action?.Invoke(true, null);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoGetMultiple(ResultAction<string[], Exception> resultAction, Action<float> progess, string[] keys)
        {
            try
            {
                progess(0);
                string[] results = new string[keys.Length];
                for (int i = 0; i < keys.Length; i++)
                    results[i] = PlayerPrefs.GetString(prefix + keys[i]);
                progess(1);
                resultAction?.Invoke(results, null, true);
            }catch(Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
            yield return null;
        }

        public override IEnumerator DoPutMultiple(IDictionary<string, object> keyValuePairs, BoolAction<Exception> action, Action<float> progess)
        {
            try
            {
                progess(0);
                IList<string> keys = GetKeys();
                foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
                {
                    if (keyValuePair.Value == null)
                    {
                        PlayerPrefs.DeleteKey(prefix + keyValuePair.Key);
                        keys.Remove(keyValuePair.Key);
                    }
                    else
                    {
                        PlayerPrefs.SetString(prefix + keyValuePair.Key, keyValuePair.Value.ToString());
                        if (!keys.Contains(keyValuePair.Key))
                            keys.Add(keyValuePair.Key);
                    }
                }
                progess(0.95f);
                SetKeys(keys);
                PlayerPrefs.Save();
                progess(1);
                action?.Invoke(true, null);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoLoadObjects(BoolAction<Exception> action, Action<float> progess, IStorableObject[] storableObjects)
        {
            try
            {
                progess(0);
                foreach (IStorableObject storableObject in storableObjects)
                {
                    string json = PlayerPrefs.GetString(prefix + storableObject.GetStorableKey());
                    if (json == null)
                        continue;
                    storableObject.OnPutStorableObject(JSON.FromJson(json, storableObject.GetStorableObjectType()));
                }
                progess(1);
                action?.Invoke(true, null);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoPutObjects(BoolAction<Exception> action, Action<float> progress, IStorableObject[] storableObjects)
        {
            try
            {
                progress(0);
                IList<string> keys = GetKeys();
                foreach (IStorableObject storableObject in storableObjects)
                {
                    string key = storableObject.GetStorableKey(), value = JSON.ToJson(storableObject.GetStorableObject());
                    PlayerPrefs.SetString(prefix + key, value);

                    if (!keys.Contains(key))
                        keys.Add(key);
                }
                progress(0.95f);
                SetKeys(keys);
                PlayerPrefs.Save();
                progress(1f);
                action?.Invoke(true, null);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }
    }
}