﻿using System.Collections.Generic;
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

        protected override void OnValueLoadEmpty()
        {
            foreach (AudioSource source in audioSources)
                source.volume = 1;
            value = 1;
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