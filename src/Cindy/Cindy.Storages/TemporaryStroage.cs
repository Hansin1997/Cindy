using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{
    /// <summary>
    /// 临时储存器，数据将直接存储在字典变量中，不进行持久化存储。
    /// </summary>
    [CreateAssetMenu(fileName = "TemporaryStroage", menuName = "Cindy/Storage/TemporaryStroage", order = 1)]
    public class TemporaryStroage : AbstractStorage
    {
        protected Dictionary<string, object> map;
        protected Dictionary<string, object> Map { get { return map ?? (map = new Dictionary<string, object>()); } }

        public override IEnumerator DoClear(BoolAction<Exception> action)
        {
            try
            {
                Map.Clear();
                action?.Invoke(true, null);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoGet(string key, ResultAction<string, Exception> resultAction)
        {
            try
            {
                if (Map.ContainsKey(key) && Map[key] != null)
                    resultAction(Map[key].ToString(), null, true);
                else
                    resultAction?.Invoke(null, null, false);
            }
            catch (Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
            yield return null;
        }

        public override IEnumerator DoGetMultiple(ResultAction<string[], Exception> resultAction, Action<float> progess, string[] keys)
        {
            try
            {
                progess(0);
                string[] result = new string[keys.Length];
                for (int i = 0; i < keys.Length; i++)
                    if (map.ContainsKey(keys[i]) && map[keys[i]] != null)
                        result[i] = map[keys[i]].ToString();
                progess(1);
                resultAction?.Invoke(result, null, true);
            }
            catch (Exception e)
            {
                resultAction?.Invoke(null, e,false);
            }
            yield return null;
        }

        public override IEnumerator DoRestoreObjects(BoolAction<Exception> action, Action<float> progess, IStorableObject[] storableObjects)
        {
            try
            {
                progess(0);
                foreach (IStorableObject storableObject in storableObjects)
                {
                    if (Map.ContainsKey(storableObject.GetStorableKey()))
                        storableObject.OnStorableObjectRestore(Map[storableObject.GetStorableKey()]);
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

        public override IEnumerator DoPut(string key, object value, BoolAction<Exception> action)
        {
            try
            {
                Map[key] = value;
                action?.Invoke(true, null);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }

        public override IEnumerator DoPutMultiple(IDictionary<string, object> keyValuePairs, BoolAction<Exception> action, Action<float> progess)
        {
            try
            {
                progess(0);
                foreach (KeyValuePair<string, object> kv in keyValuePairs)
                    Map[kv.Key] = kv.Value;
                progess(1);
                action?.Invoke(true, null);
            }catch(Exception e)
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
                foreach (IStorableObject storableObject in storableObjects)
                    Map[storableObject.GetStorableKey()] = storableObject.GetStorableObject();
                progress(1);
                action?.Invoke(true, null);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
            yield return null;
        }
    }
}