using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 用于控制AudioSource音量的浮点变量对象。
    /// </summary>
    [AddComponentMenu("Cindy/Media/Audio/AudioVolumeSetter (Float)")]
    public class AudioSourceVolume : FloatObject
    {
        /// <summary>
        /// AudioSource数组
        /// </summary>
        [Header("AudioVolume")]
        public List<AudioSource> audioSources;
        /// <summary>
        /// 扫描指定物体管理其中的AudioSource
        /// </summary>
        public Target target = Target.SlefAndChildren;

        protected virtual void Start()
        {
            audioSources.AddRange(DoScan());
        }

        public override void SetValue(float value)
        {
            foreach (AudioSource source in audioSources)
                source.volume = value;
        }

        protected override void OnValueChanged(bool save = true,bool notify = true)
        {
            foreach (AudioSource source in audioSources)
                source.volume = value;
            base.OnValueChanged(save,notify);
        }

        protected virtual List<AudioSource> DoScan()
        {
            List<AudioSource> result = new List<AudioSource>();
            switch (target)
            {
                case Target.Self:
                    AudioSource source = GetComponent<AudioSource>();
                    if (source != null)
                        result.Add(source);
                    break;
                case Target.SlefAndChildren:
                    AudioSource[] sources = GetComponentsInChildren<AudioSource>();
                    result.AddRange(sources);
                    break;
                case Target.All:
                    AudioSource[] sources_ = FindObjectsOfType<AudioSource>();
                    result.AddRange(sources_);
                    break;
            }
            return result;
        }

        public enum Target
        {
            Self,
            SlefAndChildren,
            All
        }
    }
}
