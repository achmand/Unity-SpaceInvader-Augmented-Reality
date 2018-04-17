using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public sealed class Sound
    {
        public string name;
        public AudioClip audioClip;
        [Range(0, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        public bool loop; 

        [HideInInspector] public AudioSource source;
    }
}
