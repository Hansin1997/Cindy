using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Strings/SceneName")]
    public class SceneName : StringObject
    {
        [Header("Scene Name")]
        public bool loadFromStorage = false;

        protected override void Start()
        {
            base.Start();
            GetValue();
        }
        public override void SetValue(string value)
        {
            
        }

        public override string GetValue()
        {
            value = loadFromStorage ? value : gameObject.scene.name;
            return value;
        }
    }
}
