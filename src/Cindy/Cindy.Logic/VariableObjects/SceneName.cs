using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 场景名
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Util/SceneName (String)")]
    public class SceneName : StringObject
    {
        [Header("Scene Name")]
        public SceneType sceneType = SceneType.ActiveScene;

        protected virtual void Start()
        {
            GetValue();
        }
        public override void SetValue(string value)
        {
            
        }

        public override string GetValue()
        {
            switch (sceneType)
            {
                default:
                case SceneType.ActiveScene:
                    value = SceneManager.GetActiveScene().name;
                    break;
                case SceneType.ObjectScene:
                    value = gameObject.scene.name;
                    break;
            }
            return value;
        }

        public enum SceneType
        {
            ActiveScene,
            ObjectScene
        }
    }
}
