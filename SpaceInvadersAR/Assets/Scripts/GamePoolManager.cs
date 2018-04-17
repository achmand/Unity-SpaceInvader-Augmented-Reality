using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class BulletPool : BaseObjectPool<BulletBase> { }

    [Serializable]
    public class EnemyClusterRowPool : BaseObjectPool<EnemyClusterRow> { }

    [Serializable]
    public class EnemySimpleDroidPool : BaseObjectPool<SimpleDroidEnemy> { }

    [Serializable]
    public class EnemyWarriorDroidPool : BaseObjectPool<WarriorDroidEnemy> { }

    [Serializable]
    public class EnemyBomberDroidPool : BaseObjectPool<BomberDroidEnemy> { }

    [Serializable]
    public class EnemyHeavyDroidPool : BaseObjectPool<HeavyDroidEnemy> { }

    [Serializable]
    public class EnemyHitParticlePool : BaseObjectPool<ParticleFx> { }

    [Serializable]
    public class SimpleProjectilePool : BaseObjectPool<SimpleProjectile> { }

    public sealed class GamePoolManager : MonoBehaviour
    {
        [Header("References")]
        public GameObject rootSpawnPoolsGameObject;

        [Header("Spawn Pools")]
        public BulletPool bulletObjectPool;

        [Header("Enemy Spawn Pools")]
        public EnemyClusterRowPool enemyClusterRowObjectPool;

        public EnemySimpleDroidPool enemySimpleDroidPool;
        public EnemyWarriorDroidPool enemyWarriorDroidPool;
        public EnemyBomberDroidPool enemyBomberDroidPool;
        public EnemyHeavyDroidPool enemyHeavyDroidPool;
        public EnemyHitParticlePool enemyHitParticlePool;
        public SimpleProjectilePool simpleProjectilePool;

        void Awake()
        {
            var rootTransform = rootSpawnPoolsGameObject.transform;
            bulletObjectPool.objectPooler.Initialize(bulletObjectPool.poolOptions, rootTransform);

            enemyClusterRowObjectPool.objectPooler.Initialize(enemyClusterRowObjectPool.poolOptions, rootTransform);
            enemySimpleDroidPool.objectPooler.Initialize(enemySimpleDroidPool.poolOptions, rootTransform);
            enemyWarriorDroidPool.objectPooler.Initialize(enemyWarriorDroidPool.poolOptions, rootTransform);
            enemyBomberDroidPool.objectPooler.Initialize(enemyBomberDroidPool.poolOptions, rootTransform);
            enemyHeavyDroidPool.objectPooler.Initialize(enemyHeavyDroidPool.poolOptions, rootTransform);
            enemyHitParticlePool.objectPooler.Initialize(enemyHitParticlePool.poolOptions, rootTransform);
            simpleProjectilePool.objectPooler.Initialize(simpleProjectilePool.poolOptions, rootTransform);
        }
    }
}
