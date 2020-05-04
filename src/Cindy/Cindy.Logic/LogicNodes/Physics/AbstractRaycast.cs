using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes.Physics
{
    /// <summary>
    /// 抽象射线碰撞检测
    /// </summary>
    public abstract class AbstractRaycast : LogicNode
    {
        [Header("Raycast Options")]
        public LayerMask layerMask;

        public QueryTriggerInteraction queryTriggerInteraction;

        [Header("Hit Output")]
        public ReferenceBool isHit;
        public ReferenceVector3 point;
        public ReferenceVector3 normal;
        public ReferenceVector3 targetPosition;
        public ReferenceTransfrom targetTransform;
        public ReferenceFloat distance;

        protected abstract bool Raycast(out UnityEngine.RaycastHit hit, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction);

        protected override void Run()
        {

            UnityEngine.RaycastHit hit;
            bool flag = Raycast(out hit, layerMask, queryTriggerInteraction);
            if (flag)
            {
                point.Value = hit.point;
                normal.Value = hit.normal;
                targetPosition.Value = hit.collider.transform.position;
                targetTransform.Value = hit.transform;
                distance.Value = hit.distance;
            }
            isHit.Value = flag;
        }
    }
}
