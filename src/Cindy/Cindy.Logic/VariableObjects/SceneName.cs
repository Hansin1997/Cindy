﻿using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/SceneName")]
    public class SceneName : StringObject
    {
        public bool loadFromStorage = false;

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
