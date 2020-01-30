using Cindy.Logic.VariableObjects;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Control.Controllers
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Cindy/Control/Controllers/AnimatorController", 1)]
    public class AnimatorController : ControllerAttachment
    {
        [Header("Animator Parameters")]
        public NamedBool[] boolParameters;
        public NamedFloat[] floatParameters;
        public NamedBool[] triggerParameters;

        protected Animator animator;

        protected List<UnityAction> actions;

        protected override void Start()
        {
            base.Start();
            actions = new List<UnityAction>();
        }

        public override void OnControllerSelect()
        {
            animator = GetComponent<Animator>();
            foreach (NamedBool namedBool in boolParameters)
            {
                UnityAction action = () => animator.SetBool(namedBool.key, namedBool.value.value);
                namedBool.value.valueChangedEvent.AddListener(action);
                actions.Add(() =>
                {
                    namedBool.value.valueChangedEvent.RemoveListener(action);
                });
            }

            foreach (NamedBool namedTrigger in triggerParameters)
            {
                UnityAction action = () =>
                {
                    if (namedTrigger.value.value)
                        animator.SetTrigger(namedTrigger.key);
                };
                namedTrigger.value.valueChangedEvent.AddListener(action);
                actions.Add(() =>
                {
                    namedTrigger.value.valueChangedEvent.RemoveListener(action);
                });
            }
            foreach (NamedFloat namedFloat in floatParameters)
            {
                UnityAction action = () => animator.SetFloat(namedFloat.key, namedFloat.value.value);

                namedFloat.value.valueChangedEvent.AddListener(action);
                actions.Add(() =>
                {
                    namedFloat.value.valueChangedEvent.RemoveListener(action);
                });
            }
        }

        public override void OnControllerUnselect()
        {
            foreach (UnityAction action in actions)
            {
                action.Invoke();
            }
            actions.Clear();
        }

        public override void OnControllerUpdate(float deltaTime)
        {
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
