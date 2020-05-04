using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes.Physics
{
    /// <summary>
    /// 单个射线碰撞检测
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodes/Physics/SingleRaycast")]
    public class SingleRaycast : AbstractRaycast
    {
        [Header("Points")]
        public ReferenceVector3 start;

        public ReferenceVector3 end;

        protected override bool Raycast(out RaycastHit hit, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction)
        {
            Vector3 s = start.Value, e = end.Value, dir = e - s;
            return UnityEngine.Physics.Raycast(s, dir, out hit, dir.magnitude, layerMask, queryTriggerInteraction);
        }
    }
}
