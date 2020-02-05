using Cindy.Util;
using System;
using UnityEngine;

namespace Cindy.Media.Audio
{
    [AddComponentMenu("Cindy/Media/Audio/AudioAttachment")]
    public class AudioAttachment : Attachment
    {
        public Data data;

        protected override Type GetAttachableType()
        {
            return typeof(Attachable);
        }

        [Serializable]
        public struct Data
        {
            public AudioClip clip;
            public bool loop;

            public override bool Equals(object obj)
            {
                if(obj is Data d)
                {
                    if (clip == null && d.clip != null)
                        return false;
                    return clip.Equals(d.clip) && loop == d.loop;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                if (clip == null)
                    return loop.GetHashCode();
                return clip.GetHashCode() ^ loop.GetHashCode();
            }
        }
    }
}
