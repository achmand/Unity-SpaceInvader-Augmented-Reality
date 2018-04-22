using UnityEngine;

namespace Assets.Scripts
{
    public enum ProjectileType
    {
        SimpleProjectile,
        HeavyProjectile,
        DemonProjectile
    }

    // TODO -> Add can be destroyed functionality 
    public abstract class Projectile : MonoBehaviour, IPoolableObject
    {
        public float timeToDespawn; 
        public abstract float timeBeforeDespawnSeconds { get; }

        public int? hitTargetId;
        public abstract ProjectileType projectileType { get; }
        protected abstract float projectileForce { get; }
        public abstract int projectileDamage { get; }
        protected Rigidbody rigidbody;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 startingPostion, Vector3 target)
        {
            rigidbody.AddForce((target - startingPostion).normalized * projectileForce);
        }

        public void ResetObject()
        {
            timeToDespawn = 0f;
            hitTargetId = null; 
            rigidbody.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hitTargetId != null)
            {
                return;
            }

            var playerOwner = other.gameObject.GetComponent<PlayerOwner>();
            if (playerOwner != null)
            {
                hitTargetId = playerOwner.PlayerId;
            }
        }
    }
}
