using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ParticleFx : MonoBehaviour, IPoolableObject
    {
        public float lifeTimeInSeconds;

        [HideInInspector] public float timeToDespawn; 
        private ParticleSystem particleSystem;

        void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        public void PlayParticleSystem()
        {
            particleSystem.Play(true);
        }

        public void StopParticleSystem()
        {
            particleSystem.Stop(true);
        }

        public void ResetParticleSystem()
        {
            StopParticleSystem();
            particleSystem.Clear(true);
        }

        public void ResetObject()
        {
            timeToDespawn = 0;
            ResetParticleSystem();
        }
    }
}