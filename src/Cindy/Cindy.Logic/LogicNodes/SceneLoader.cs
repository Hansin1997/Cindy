using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cindy.Logic.LogicNodes
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Logic/LogicNodes/SceneLoader")]
    public class SceneLoader : LogicNode
    {
        [Header("SceneLoader")]
        public ReferenceString sceneName;

        public ReferenceBool async = new ReferenceBool() { value = true };

        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        public LocalPhysicsMode localPhysicsMode = LocalPhysicsMode.None;

        [Header("Status")]
        public ReferenceFloat progress;

        public ReferenceBool isLoading;

        protected AsyncOperation operation;

        protected override void Run()
        {
            if (isLoading.Value)
                return;

            if (async.Value)
                StartCoroutine("LoadScene");
            else
            {
                isLoading.Value = true;
                progress.Value = 0;
                SceneManager.LoadScene(sceneName.Value, new LoadSceneParameters(loadSceneMode, localPhysicsMode));
                progress.Value = 1;
                isLoading.Value = false;
            }
        }

        public virtual IEnumerator LoadScene()
        {
            isLoading.Value = true;
            progress.Value = 0;
            operation = SceneManager.LoadSceneAsync(sceneName.Value, new LoadSceneParameters(loadSceneMode, localPhysicsMode));
            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
                progress.Value = operation.progress;
                yield return new WaitForSeconds(0.1f);
            }
            operation.allowSceneActivation = true;
            progress.Value = 1;
            isLoading.Value = false;
        }

        public void SetSceneName(string sceneName)
        {
            this.sceneName.Value = sceneName;
        }

        public void SetSceneName(StringObject stringObject)
        {
            sceneName.Value = stringObject != null ? stringObject.Value : "";
        }
    }
}
