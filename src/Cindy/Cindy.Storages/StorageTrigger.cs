using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{

    [AddComponentMenu("Cindy/Storage/StorageTrigger", 10)]
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
            if (option.loadOnStart)
                LoadObjects();

        }

        public virtual void OnApplicationPause(bool pause)
        {
            if (option.saveOnPause && pause)
                SaveObjects();
        }

        public virtual void OnApplicationQuit()
        {
            if (option.saveOnQuit)
                SaveObjects();
        }

        public void LoadObjects()
        {
            if(storage != null)
                storage.LoadObjects(FindObjects());
        }

        public void SaveObjects()
        {
            if (storage != null)
                storage.PutObjects(FindObjects());
        }

        protected AbstractStorableObject[] FindObjects()
        {
            AbstractStorableObject[] storableObjects = FindObjectsOfType<AbstractStorableObject>();
            List<AbstractStorableObject> tmp = new List<AbstractStorableObject>();
            foreach(AbstractStorableObject storableObject in storableObjects)
            {
                if (storableObject.StorableOptions.label.Equals(option.label))
                    tmp.Add(storableObject);
            }
            return tmp.ToArray();
         }
    }
}
