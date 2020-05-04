using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    /// <summary>
    /// 序列化列表
    /// </summary>
    [Serializable]
    public class SerializedList
    {
        [Multiline]
        public string[] array;

        public SerializedList()
        {

        }

        public SerializedList(IList<string> ts)
        {
            if (ts == null)
                array = new string[0];
            else
            {
                array = new string[ts.Count];
                for (int i = 0; i < array.Length; i++)
                    array[i] = ts[i];
            }
        }

        public IList<string> ToList()
        {
            IList<string> result = new List<string>();
            if (array != null)
                foreach (string t in array)
                    result.Add(t);
            return result;
        }
    }
}
