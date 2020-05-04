using Cindy.Logic.VariableObjects;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    /// <summary>
    /// 动画机控制器
    /// </summary>
    [AddComponentMenu("Cindy/Control/Controllers/AnimatorController", 1)]
    public class AnimatorController : Controller
    {
        /// <summary>
        /// 动画机
        /// </summary>
        public Animator animator;

        /// <summary>
        /// 布尔型参数表
        /// </summary>
        [Header("Animator Parameters")]
        public NamedBool[] boolParameters;
        /// <summary>
        /// 浮点型参数表
        /// </summary>
        public NamedFloat[] floatParameters;
        /// <summary>
        /// 触发器参数表
        /// </summary>
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
                animator.SetBool(namedBool.Key, false);
            }
            foreach (NamedFloat namedFloat in floatParameters)
            {
                animator.SetFloat(namedFloat.Key, 0);
            }
        }

        public override void OnControllerUpdate(float deltaTime)
        {
            foreach (NamedBool namedBool in boolParameters)
            {
                animator.SetBool(namedBool.Key, namedBool.Value.Value);
            }
            foreach (NamedBool namedTrigger in triggerParameters)
            {
                if (namedTrigger.Value.Value)
                    animator.SetTrigger(namedTrigger.Key);
            }
            foreach (NamedFloat namedFloat in floatParameters)
            {
                animator.SetFloat(namedFloat.Key, namedFloat.Value.Value);
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
