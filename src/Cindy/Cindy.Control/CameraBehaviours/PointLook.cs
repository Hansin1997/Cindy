using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{
    [CreateAssetMenu(fileName = "PointLookCamera", menuName = "Cindy/Control/Camera/PointLook", order = 1)]
    public class PointLook : BaseCameraBehaviour
    {
        //[Header("PointLook")]
        public const string POINT_KEY = "Point";

        protected override Vector3 GetPosition(Camera camera, Transform target, float deltaTime, IDictionary<string, object> parameters)
        {
            object obj;
            if (parameters.TryGetValue(POINT_KEY, out obj) && obj is Transform point)
                return point.position;
            else
                return camera.transform.position;
        }
    }
}
