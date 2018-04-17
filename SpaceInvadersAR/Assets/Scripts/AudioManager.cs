using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class AudioManager : MonoBehaviour
    {
        public Sound[] soundClips;
        private Dictionary<string, Sound> soundClipCollection;

        void Awake()
        {
            soundClipCollection = new Dictionary<string, Sound>(soundClips.Length);
            for (int i = 0; i < soundClips.Length; i++)
            {
                var clip = soundClips[i];
                clip.source = gameObject.AddComponent<AudioSource>();
                clip.source.clip = clip.audioClip;
                clip.source.volume = clip.volume;
                clip.source.pitch = clip.pitch;
                clip.source.loop = clip.loop;

                if (!soundClipCollection.ContainsKey(clip.name))
                {
                    soundClipCollection.Add(clip.name, clip);
                }
            }
        }

        public void Play(string clipName)
        {
            if (!soundClipCollection.ContainsKey(clipName))
            {
                Debug.LogError(string.Format("No clip with the specified name {0}", clipName));
            }

            var soundClip = soundClipCollection[clipName];
            soundClip.source.Play();
        }

        private Sound currentTheme;
        public void PlayLevelTheme(int levelNo)
        {
            var clipName = string.Format("Level {0} Theme", levelNo);
            if (!soundClipCollection.ContainsKey(clipName))
            {
                return;
            }

            if (currentTheme != null)
            {
                currentTheme.source.Stop();
            }

            var currentLevel = soundClipCollection[clipName];
            currentTheme = currentLevel;
            currentTheme.source.Play();
        }
    }
}
