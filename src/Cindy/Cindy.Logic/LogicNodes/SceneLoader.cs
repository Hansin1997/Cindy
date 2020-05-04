using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cindy.Logic.LogicNodes
{
    /// <summary>
    /// 场景加载器
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodes/SceneLoader")]
    public class SceneLoader : CoroutineLogicNode
    {
        [Header("SceneLoader")]
        public ReferenceString sceneName;

        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        public LocalPhysicsMode localPhysicsMode = LocalPhysicsMode.None;

        public OnLoadCompleted onLoadCompleted;

        [Header("Status")]
        public ReferenceFloat progress;

        public ReferenceBool isLoading;

        protected override IEnumerator DoRun()
        {
            if (!isLoading.Value)
            {
                AsyncOperation operation;
                isLoading.Value = true;
                progress.Value = 0;
                operation = SceneManager.LoadSceneAsync(sceneName.Value, new LoadSceneParameters(loadSceneMode, localPhysicsMode));
                operation.allowSceneActivation = false;
                while (operation.progress < 0.9f)
                {
                    progress.Value = operation.progress;
                    yield return null;
                }
                progress.Value = 0.999999f;

                onLoadCompleted.events.Invoke();
                yield return new WaitForSeconds(onLoadCompleted.waitForSecond.Value);
                yield return new WaitUntil(() =>
                {
                    if (onLoadCompleted.waitUntil != null)
                        return onLoadCompleted.waitUntil.Check();
                    return true;
                });

                progress.Value = 1;
                isLoading.Value = false;
                operation.allowSceneActivation = true;
                while (operation.progress < 1f)
                {
                    yield return null;
                }
            }
        }

        public void SetSceneName(string sceneName)
        {
            this.sceneName.Value = sceneName;
        }

        public void SetSceneName(StringObject stringObject)
        {
            sceneName.Value = stringObject != null ? stringObject.Value : "";
        }

        [Serializable]
        public class OnLoadCompleted
        {
            public ReferenceFloat waitForSecond;

            public Condition waitUntil;

            public UnityEvent events;
        }
    }
}
