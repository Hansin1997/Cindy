using Cindy.Util;
using System;
using UnityEngine;

namespace Cindy.Media.Audio
{
    /// <summary>
    /// 音频附件，提交音频到音频栈
    /// </summary>
    [AddComponentMenu("Cindy/Media/Audio/AudioAttachment")]
    public class AudioAttachment : Attachment
    {
        /// <summary>
        /// 附件数据
        /// </summary>
        public Data data;

        protected override Type GetTargetType()
        {
            return typeof(AttachmentContainer);
        }

        /// <summary>
        /// 附件数据
        /// </summary>
        [Serializable]
        public struct Data
        {
            /// <summary>
            /// 音频
            /// </summary>
            public AudioClip clip;
            /// <summary>
            /// 是否循环
            /// </summary>
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
