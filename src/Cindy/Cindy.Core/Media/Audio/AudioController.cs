using Cindy.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Media.Audio
{
    [AddComponentMenu("Cindy/Media/Audio/AudioController")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : Attachable
    {

        public bool dontDestroyOnLoad;

        public bool stack = true;

        public List<AudioAttachment.Data> queue;

        protected AudioSource source;

        protected AudioAttachment.Data current;

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is AudioAttachment;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            return attachment is AudioAttachment audioAttachment && audioAttachment.enabled && audioAttachment.gameObject.activeSelf;
        }

        public override bool Attach(Attachment attachment)
        {
            if (attachment is AudioAttachment audioAttachment)
            {
                if (!stack)
                    queue.Clear();
                if (!queue.Contains(audioAttachment.data))
                    queue.Add(audioAttachment.data);
                else
                    return false;
                return true;
            }
            else
                return false;
        }

        public override bool Detach(Attachment attachment)
        {
            if (attachment is AudioAttachment audioAttachment)
            {
                if (queue.Contains(audioAttachment.data))
                {
                    while(queue.Count > 0)
                    {
                        AudioAttachment.Data data = queue[queue.Count - 1];
                        queue.Remove(data);
                        if (data.Equals(audioAttachment.data))
                        {
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        protected virtual void Start()
        {
            AudioController[] controllers = FindObjectsOfType<AudioController>();
            foreach(AudioController controller in controllers)
            {
                if (controller != this)
                    Destroy(gameObject);
            }
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            if(queue == null || queue.Count == 0)
            {
                source.clip = null;
                source.loop = false;
            }
            else
            {
                AudioAttachment.Data top = queue[queue.Count - 1];
                if(!top.Equals(current))
                {
                    source.clip = top.clip;
                    source.loop = top.loop;
                    source.Play();
                }
                else
                {
                    if(!source.isPlaying)
                    {
                        queue.Remove(top);
                    }
                }
                current = top;
            }
        }
    }
}
