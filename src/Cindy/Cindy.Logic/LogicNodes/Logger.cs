using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
    /// <summary>
    /// Logger
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodes/Logger")]
    public class Logger : LogicNode
    {
        [Header("Logger")]
        public ReferenceString message;
        public LogType logType = LogType.Info;

        protected override void Run()
        {
            switch (logType)
            {
                case LogType.Warning:
                    Debug.LogWarning(message.Value);
                    break;
                case LogType.Error:
                    Debug.LogError(message.Value);
                    break;
                case LogType.Info:
                default:
                    Debug.Log(message.Value);
                    break;
            }
        }

        public enum LogType
        {
            Info,
            Warning,
            Error
        }
    }
}
