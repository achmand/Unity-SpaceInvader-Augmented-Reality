using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ProjectilePoolManager : MonoBehaviour
    {
        private ObjectPooler<SimpleProjectile> simpleProjectilePooler;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            var gamePoolManager = globalReferenceManager.gamePoolManager;

            simpleProjectilePooler = gamePoolManager.simpleProjectilePool.objectPooler;
        }

        public Projectile SpawnProjectile(ProjectileType enemyType, Vector3 position, Quaternion rotation, bool useLocalTransform = false, Transform parent = null, bool worldPositionStays = true)
        {
            switch (enemyType)
            {
                case ProjectileType.SimpleProjectile:
                    return simpleProjectilePooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
            }

            return null;
        }
    }
}
