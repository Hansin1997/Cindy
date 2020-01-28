using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cindy.Logic.Methods
{
    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoader")]
    public class SceneLoader : LogicNode
    {
        public ReferenceString sceneName;

        protected AsyncOperation operation;

        public float Progress { get { return operation != null ? operation.progress : 0; } }

        public bool IsLoading { get { return operation != null && !operation.isDone; } }

        protected override void Run()
        {
            if (IsLoading)
                return;
            StartCoroutine("LoadScene");
        }

        public virtual IEnumerator LoadScene()
        {
            operation = SceneManager.LoadSceneAsync(sceneName.Value);
            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            operation.allowSceneActivation = true;
        }

        public void SetSceneName(string sceneName)
        {
            this.sceneName.Value = sceneName;
        }
    }

    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoaderProgress")]
    public class SceneLoaderProgress : FloatObject
    {
        public SceneLoader sceneLoader;

        protected override void Start()
        {
            if (sceneLoader == null)
                sceneLoader = GetComponent<SceneLoader>();
            if (sceneLoader != null)
            {
                value = sceneLoader.Progress;
            }
        }

        protected override void Update()
        {
            if (sceneLoader != null)
            {
                value = sceneLoader.Progress;
            }
            base.Update();
        }
    }

    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoaderLoadingFlag")]
    public class SceneLoaderLoadingFlag : BoolObject
    {
        public SceneLoader sceneLoader;
        protected override void Start()
        {
            if (sceneLoader == null)
                sceneLoader = GetComponent<SceneLoader>();
            if (sceneLoader != null)
            {
                value = sceneLoader.IsLoading;
            }
        }

        protected override void Update()
        {
            if (sceneLoader != null)
            {
                value = sceneLoader.IsLoading;
            }
            base.Update();
        }
    }
}
