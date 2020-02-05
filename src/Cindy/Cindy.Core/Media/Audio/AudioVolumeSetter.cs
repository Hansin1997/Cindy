using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Media/Audio/AudioVolumeSetter")]
    public class AudioVolumeSetter : FloatObject
    {
        [Header("AudioVolume")]
        public List<AudioSource> audioSources;
        public bool asScale;
        public Target target = Target.SlefAndChildren;

        protected override void Start()
        {
            audioSources.AddRange(DoScan());
            base.Start();
        }

        protected override void OnValueLoad(float val)
        {
            foreach (AudioSource source in audioSources)
                source.volume = val;
            base.OnValueLoad(val);
        }

        protected override void OnValueChanged()
        {
            foreach (AudioSource source in audioSources)
                source.volume = value;
            base.OnValueChanged();
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
