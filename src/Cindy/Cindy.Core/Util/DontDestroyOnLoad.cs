using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Util
{
    /// <summary>
    /// 绑定DontDestroyOnLoad的物体将移至'DontDestroyOnLoad'场景，在场景切换时不销毁。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Util/DontDestroyOnLoad")]
    public class DontDestroyOnLoad : NamedBehaviour
    {
        [Tooltip("ID of this component.")]
        public ReferenceString id; // 此对象的Id
        [Tooltip("Whether to destroy the ID when it exists in the scene.")]
        public ReferenceBool destroySelfIfIdExist = new ReferenceBool() { value = true }; // 是否当id在场景中存在时销毁自身。

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