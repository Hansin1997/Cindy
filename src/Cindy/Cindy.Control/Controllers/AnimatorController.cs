using Cindy.Logic.VariableObjects;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    [AddComponentMenu("Cindy/Control/Controllers/AnimatorController", 1)]
    public class AnimatorController : Controller
    {

        public Animator animator;
        [Header("Animator Parameters")]
        public NamedBool[] boolParameters;
        public NamedFloat[] floatParameters;
        public NamedBool[] triggerParameters;

        protected override void Start()
        {
            base.Start();
        }

        public override void OnControllerSelect()
        {
            if(animator == null)
                animator = GetComponent<Animator>();
        }

        public override void OnControllerUnselect()
        {
            foreach (NamedBool namedBool in boolParameters)
            {
                animator.SetBool(namedBool.key, false);
            }
            foreach (NamedFloat namedFloat in floatParameters)
            {
                animator.SetFloat(namedFloat.key, 0);
            }
        }

        public override void OnControllerUpdate(float deltaTime)
        {
            foreach (NamedBool namedBool in boolParameters)
            {
                animator.SetBool(namedBool.key, namedBool.value.Value);
            }
            foreach (NamedBool namedTrigger in triggerParameters)
            {
                if (namedTrigger.value.Value)
                    animator.SetTrigger(namedTrigger.key);
            }
            foreach (NamedFloat namedFloat in floatParameters)
            {
                animator.SetFloat(namedFloat.key, namedFloat.value.Value);
            }
        }


        [Serializable]
        public class NamedBool : SerializedKeyValuePair<string, BoolObject>
        {
            public NamedBool(KeyValuePair<string, BoolObject> keyValuePair) : base(keyValuePair)
            {

            }
        }

        [Serializable]
        public class NamedFloat : SerializedKeyValuePair<string, FloatObject>
        {
            public NamedFloat(KeyValuePair<string, FloatObject> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
