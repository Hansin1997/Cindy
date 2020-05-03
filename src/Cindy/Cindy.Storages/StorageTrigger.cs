using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
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
            public ReferenceString[] labels = { new ReferenceString() { value = "default" } };
            public bool loadOnStart = true;
            public bool saveOnPause = true;
            public bool saveOnQuit = true;
        }
        public AbstractStorage storage;

        public StorageTriggerOption option;

        public ReferenceFloat progress;

        public LogicNode onLoadComleted;
        public LogicNode onSaveCompleted;
        public ExceptionCallback onLoadException;
        public ExceptionCallback onSaveException;

        public struct ExceptionCallback
        {
            public ReferenceString errorMsg;
            public LogicNode node;
        }

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

        public virtual void LoadObjects()
        {
            if (storage != null)
                storage.LoadObjects(this, (s, e) =>
                {
                    
                    if (!s)
                    {
                        onLoadException.errorMsg.Value = e == null ? "" : e.Message;
                        if (onLoadException.node != null)
                        {
                            onLoadException.node.Execute();
                        }
                        Debug.LogWarning(e);
                    }
                    else
                    {
                        if (onLoadComleted != null)
                            onLoadComleted.Execute();
                    }
                }, (p) =>
                {
                    progress.Value = p;
                }, FindObjects());
        }

        public virtual void SaveObjects()
        {
            if (storage != null)
                storage.PutObjects(this,(s,e)=>
                {
                    if (!s)
                    {
                        onSaveException.errorMsg.Value = e == null ? "" : e.Message;
                        if(onSaveException.node != null)
                        {
                            onSaveException.node.Execute();
                        }
                        Debug.LogWarning(e);
                    }
                    else
                    {
                        if(onSaveCompleted != null)
                            onSaveCompleted.Execute();
                    }
                },(p)=>
                {
                    progress.Value = p;
                },FindObjects());
        }

        protected virtual AbstractStorableObject[] FindObjects()
        {
            AbstractStorableObject[] storableObjects = FindObjectsOfType<AbstractStorableObject>();
            List<AbstractStorableObject> tmp = new List<AbstractStorableObject>();
            foreach(AbstractStorableObject storableObject in storableObjects)
            {
                if (storableObject.StorableOptions.ContainerLabel(option.labels))
                    tmp.Add(storableObject);
            }
            return tmp.ToArray();
         }
    }
}
