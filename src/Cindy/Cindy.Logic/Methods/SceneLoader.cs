using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cindy.Logic.Methods
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoader")]
    public class SceneLoader : LogicNode
    {
        public ReferenceString sceneName;

        protected AsyncOperation operation;

        public bool async = true;

        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        public LocalPhysicsMode localPhysicsMode = LocalPhysicsMode.None;

        public float Progress { get { return operation != null ? operation.progress : 0; } }

        public bool IsLoading { get { return operation != null && !operation.isDone; } }

        public bool Async { get { return async; } set { async = value; } }

        public LoadSceneMode LoadSceneMode { get { return loadSceneMode; } set { loadSceneMode = value; } }

        public LocalPhysicsMode LocalPhysicsMode { get { return localPhysicsMode; } set { localPhysicsMode = value; } }

        protected override void Run()
        {
            if (IsLoading)
                return;

            if (async)
                StartCoroutine("LoadScene");
            else
                SceneManager.LoadScene(sceneName.Value, new LoadSceneParameters(loadSceneMode, localPhysicsMode));
        }

        public virtual IEnumerator LoadScene()
        {
            operation = SceneManager.LoadSceneAsync(sceneName.Value, new LoadSceneParameters(loadSceneMode, localPhysicsMode));
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

        public void SetSceneName(StringObject stringObject)
        {
            sceneName.Value = stringObject != null ? stringObject.Value : "";
        }
    }

    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoaderProxy")]
    public class SceneLoaderProxy : LogicNode
    {
        public ReferenceString sceneName;
        public ReferenceString sceneLoaderName;
        public bool anyLoader = true;

        public bool async = true;

        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        public LocalPhysicsMode localPhysicsMode = LocalPhysicsMode.None;

        public bool Async { get { return async; } set { async = value; } }

        public LoadSceneMode LoadSceneMode { get { return loadSceneMode; } set { loadSceneMode = value; } }

        public LocalPhysicsMode LocalPhysicsMode { get { return localPhysicsMode; } set { localPhysicsMode = value; } }

        protected override void Run()
        {
            SceneLoader loader = FindLoader();
            if (loader == null)
                Debug.LogError("SceneLoaderProxy can not find a SceneLoader!", this);
            else
            {
                loader.SetSceneName(sceneName.Value);
                loader.async = async;
                loader.loadSceneMode = loadSceneMode;
                loader.localPhysicsMode = localPhysicsMode;
                loader.Execute();
            }
        }

        protected virtual SceneLoader FindLoader()
        {
            if (anyLoader)
            {
                return FindObjectOfType<SceneLoader>();
            }
            else
            {
                if (sceneLoaderName.Value.Trim().Length == 0)
                    return null;
                SceneLoader[] loaders = FindObjectsOfType<SceneLoader>();
                foreach (SceneLoader loader in loaders)
                    if (loader.gameObject.name.Equals(sceneLoaderName.Value))
                        return loader;
                return null;
            }
        }

        public void SetSceneLoaderName(string sceneLoaderName)
        {
            this.sceneLoaderName.Value = sceneLoaderName;
        }

        public void SetSceneLoaderName(StringObject stringObject)
        {
            sceneLoaderName.Value = stringObject != null ? stringObject.Value : "";
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

    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoaderProgress")]
    public class SceneLoaderProgress : FloatObject
    {
        public SceneLoader sceneLoader;

        protected override void Start()
        {
            if (sceneLoader == null)
                SetSceneLoader(GetComponent<SceneLoader>());
        }

        public override float GetValue()
        {
            if (sceneLoader != null)
            {
                value = sceneLoader.Progress;
            }
            return base.GetValue();
        }

        public override void SetValue(float value)
        {

        }

        public void SetSceneLoader(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            if (sceneLoader != null)
            {
                value = sceneLoader.Progress;
            }
        }
    }

    [AddComponentMenu("Cindy/Logic/Methods/SceneLoaders/SceneLoaderLoadingFlag")]
    public class SceneLoaderLoadingFlag : BoolObject
    {
        public SceneLoader sceneLoader;
        protected override void Start()
        {
            if (sceneLoader == null)
                SetSceneLoader(GetComponent<SceneLoader>());
        }

        public override bool GetValue()
        {

            if (sceneLoader != null)
            {
                value = sceneLoader.IsLoading;
            }
            return base.GetValue();
        }

        public override void SetValue(bool value)
        {

        }

        public void SetSceneLoader(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            if (sceneLoader != null)
            {
                value = sceneLoader.IsLoading;
            }
        }
    }
}
