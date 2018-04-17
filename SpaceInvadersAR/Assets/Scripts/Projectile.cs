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
        public int? playerHit;
        public abstract ProjectileType projectileType { get; }
        public abstract float projectileForce { get; }
        public abstract int projectileDamage { get; }
        public abstract float timeBeforeDespawnSeconds { get; }

        public void ResetObject()
        {
            playerHit = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (playerHit == null)
            {
                return;
            }

            var playerOwner = other.gameObject.GetComponent<PlayerOwner>();
            if (playerOwner != null)
            {
                playerHit = playerOwner.PlayerId;
            }
        }
    }
}
