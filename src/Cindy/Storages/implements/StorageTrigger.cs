using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages.implements
{
    public class StorageTrigger : MonoBehaviour
    {
        [Serializable]
        public class StorageTriggerOption
        {
            public string label = "default";
            public bool loadOnStart = true;
            public bool saveOnPause = true;
            public bool saveOnQuit = true;
        }
        public AbstractStorage storage;

        public StorageTriggerOption option;

        public virtual void Start()
        {
            if (option.loadOnStart && storage != null)
                storage.LoadObjects(FindObjects());

        }

        public virtual void OnApplicationPause(bool pause)
        {
            if (option.saveOnPause && storage != null && pause)
                storage.PutObjects(FindObjects());
        }

        public virtual void OnApplicationQuit()
        {
            if (option.saveOnQuit && storage != null)
                storage.PutObjects(FindObjects());
        }

        protected AbstractStorableObject[] FindObjects()
        {
            AbstractStorableObject[] storableObjects = FindObjectsOfType<AbstractStorableObject>();
            List<AbstractStorableObject> tmp = new List<AbstractStorableObject>();
            foreach(AbstractStorableObject storableObject in storableObjects)
            {
                if (storableObject.baseOptions.label.Equals(option.label))
                    tmp.Add(storableObject);
            }
            return tmp.ToArray();
         }
    }
}
