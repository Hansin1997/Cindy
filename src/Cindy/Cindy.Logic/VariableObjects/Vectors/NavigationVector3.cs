using Cindy.Logic.ReferenceValues;
using UnityEngine;
using UnityEngine.AI;

namespace Cindy.Logic.VariableObjects.Vectors
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/NavigationVector3")]
    public class NavigationVector3 : Vector3Object
    {
        public NavMeshAgent agent;
        public ReferenceVector3 target;
        public ValueType valueType;
        protected Vector3 _target;

        protected override void Start()
        {
            if(agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
            base.Start();
        }

        protected override void Update()
        {
            if (!_target.Equals(target.Value))
                OnTargetChanged();
            base.Update();
        }

        protected void OnTargetChanged()
        {
            if (agent == null)
                return;
            _target = target.Value;
            agent.SetDestination(_target);
        }

        public override void SetValue(Vector3 value)
        {

        }

        public override Vector3 GetValue()
        {
            if (agent == null)
            {
                value = Vector3.zero;
            }
            else
            {
                switch (valueType)
                {
                    case ValueType.NextPosition:
                        value = agent.nextPosition;
                        break;
                    case ValueType.Velocity:
                        value = agent.velocity;
                        break;
                    case ValueType.DesiredVelocity:
                        value = agent.desiredVelocity;
                        break;
                    default:
                        value = Vector3.zero;
                        break;
                }
            }
            return base.GetValue();
        }

        public enum ValueType
        {
            NextPosition,
            Velocity,
            DesiredVelocity
        }
    }
}
