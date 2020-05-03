using Cindy.Logic.ReferenceValues;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;

namespace Cindy.Storages
{
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

        [Serializable]
        public class StorableObjectOption
        {
            public ReferenceString[] labels = { new ReferenceString() { value = "default" } };
            public bool globalUniqueness = false;

            public bool ContainerLabel(params string[] labels)
            {
                HashSet<string> labelsSet = new HashSet<string>();
                foreach (ReferenceString label in this.labels)
                    labelsSet.Add(label.Value);
                if (labelsSet.Count == 0)
                    return false;
                foreach(string label in labels)
                {
                    if (labelsSet.Contains(label))
                        return true;
                }
                return false;
            }

            public bool ContainerLabel(params ReferenceString[] labels)
            {
                HashSet<string> labelsSet = new HashSet<string>();
                foreach (ReferenceString label in this.labels)
                    labelsSet.Add(label.Value);
                if (labelsSet.Count == 0)
                    return false;
                foreach (ReferenceString label in labels)
                {
                    if (labelsSet.Contains(label.Value))
                        return true;
                }
                return false;
            }
        }
    }
}
