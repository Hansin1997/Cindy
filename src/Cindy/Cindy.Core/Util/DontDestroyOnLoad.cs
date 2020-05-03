using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Util
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Util/DontDestroyOnLoad")]
    public class DontDestroyOnLoad : NamedObject
    {
        public ReferenceString id;
        public ReferenceBool destroySelfIfIdExist = new ReferenceBool() { value = true };

        protected virtual void Awake()
        {
            if (id.Value.Trim().Length == 0)
                id.Value = gameObject.name;
            if (destroySelfIfIdExist.Value)
            {
                DontDestroyOnLoad[] arr = FindObjectsOfType<DontDestroyOnLoad>();
                foreach(DontDestroyOnLoad d in arr)
                {
                    if (d == this)
                        continue;
                    if (d.Name.Equals(Name))
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
            }
            DontDestroyOnLoad(gameObject);
        }

        protected override string GetName()
        {
            if (id.Value.Trim().Length == 0)
                id.Value = gameObject.name;
            return id.Value;
        }
    }
}