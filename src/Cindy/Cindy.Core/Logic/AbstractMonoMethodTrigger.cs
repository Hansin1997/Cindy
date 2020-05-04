namespace Cindy.Logic
{
    /// <summary>
    /// 抽象MonoBehaviour方法触发器，指定一个MonoBehaviour方法，在其被调用时触发事件。
    /// </summary>
    public abstract class AbstractMonoMethodTrigger : LogicTrigger
    {
        /// <summary>
        /// MonoBehaviour方法
        /// </summary>
        public MonoMethods monoMethod = MonoMethods.Update;

        protected virtual void Awake()
        {
            if (monoMethod == MonoMethods.Awake)
                Execute();
        }

        protected virtual void OnEnable()
        {
            if (monoMethod == MonoMethods.OnEnable)
                Execute();
        }

        protected virtual void Start()
        {
            if (monoMethod == MonoMethods.Start)
                Execute();
        }

        protected virtual void FixedUpdate()
        {
            if (monoMethod == MonoMethods.FixedUpdate)
                Execute();
        }

        protected virtual void Update()
        {
            if (monoMethod == MonoMethods.Update)
                Execute();
        }

        protected virtual void LateUpdate()
        {
            if (monoMethod == MonoMethods.LateUpdate)
                Execute();
        }

        protected virtual void OnGUI()
        {
            if (monoMethod == MonoMethods.OnGUI)
                Execute();
        }

        protected virtual void OnApplicationPause()
        {
            if (monoMethod == MonoMethods.OnApplicationPause)
                Execute();
        }

        protected virtual void OnDisable()
        {
            if (monoMethod == MonoMethods.OnDisable)
                Execute();
        }

        protected virtual void OnDestroy()
        {
            if (monoMethod == MonoMethods.OnDestroy)
                Execute();
        }

        protected virtual void OnApplicationQuit()
        {
            if (monoMethod == MonoMethods.OnApplicationQuit)
                Execute();
        }
    }
}
