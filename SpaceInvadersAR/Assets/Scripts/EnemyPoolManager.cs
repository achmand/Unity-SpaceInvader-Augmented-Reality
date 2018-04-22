using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class EnemyPoolManager : MonoBehaviour
    {
        private ObjectPooler<EnemyClusterRow> enemyClusterRowPooler;

        private ObjectPooler<SimpleDroidEnemy> simpleDroidPooler;
        private ObjectPooler<WarriorDroidEnemy> warriorDroidPooler;
        private ObjectPooler<BomberDroidEnemy> bomberDroidPooler;
        private ObjectPooler<HeavyDroidEnemy> heavyDroidPooler;

        private ObjectPooler<EnemyParticleFx> enemyHitFxPooler;
        private ObjectPooler<EnemyParticleFx> enemyShootAlertFxPooler;

        private Queue<EnemyParticleFx> queuedFxDespawns;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            var gamePoolManager = globalReferenceManager.gamePoolManager;

            enemyClusterRowPooler = gamePoolManager.enemyClusterRowObjectPool.objectPooler;

            simpleDroidPooler = gamePoolManager.enemySimpleDroidPool.objectPooler;
            warriorDroidPooler = gamePoolManager.enemyWarriorDroidPool.objectPooler;
            bomberDroidPooler = gamePoolManager.enemyBomberDroidPool.objectPooler;
            heavyDroidPooler = gamePoolManager.enemyHeavyDroidPool.objectPooler;

            enemyHitFxPooler = gamePoolManager.enemyHitParticlePool.objectPooler;
            enemyShootAlertFxPooler = gamePoolManager.enemyShootAlertParticlePool.objectPooler;

            queuedFxDespawns = new Queue<EnemyParticleFx>();
        }

        void Update()
        {
            HandleHitFxDespawns();
        }

        public EnemyClusterRow SpawnEnemyClusterRow(Vector3 position, Quaternion rotation, bool useLocalTransform = false, Transform parent = null, bool worldPositionStays = true)
        {
            return enemyClusterRowPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
        }

        public void DespawnEnemyClusterRow(EnemyClusterRow enemyClusterRow)
        {
            enemyClusterRowPooler.DespawnObject(enemyClusterRow);
        }

        public EnemyBase SpawnEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation, bool useLocalTransform = false, Transform parent = null, bool worldPositionStays = true)
        {
            switch (enemyType)
            {
                case EnemyType.SimpleDroid:
                    return simpleDroidPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                case EnemyType.WarriorDroid:
                    return warriorDroidPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                case EnemyType.BomberDroid:
                    return bomberDroidPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                case EnemyType.HeavyDroid:
                    return heavyDroidPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
            }

            return null;
        }

        public void DespawnEnemy(EnemyBase enemyBase)
        {
            var enemyType = enemyBase.EnemyType;
            switch (enemyType)
            {
                case EnemyType.SimpleDroid:
                    simpleDroidPooler.DespawnObject((SimpleDroidEnemy)enemyBase);
                    break;
                case EnemyType.WarriorDroid:
                    warriorDroidPooler.DespawnObject((WarriorDroidEnemy)enemyBase);
                    break;
                case EnemyType.BomberDroid:
                    bomberDroidPooler.DespawnObject((BomberDroidEnemy)enemyBase);
                    break;
                case EnemyType.HeavyDroid:
                    heavyDroidPooler.DespawnObject((HeavyDroidEnemy)enemyBase);
                    break;
            }
        }

        public ParticleFx SpawnHitFx(EnemyAction enemyAction, Vector3 position, Quaternion rotation, bool useLocalTransform = false, Transform parent = null, bool worldPositionStays = true)
        {
            EnemyParticleFx particleFx = null;
            switch (enemyAction)
            {
                case EnemyAction.DamageHit:
                    particleFx = enemyHitFxPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                    break;
                case EnemyAction.Died: // TODO -> Add Dead Particle Effects 
                    particleFx = enemyHitFxPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                    break;
                case EnemyAction.Shoot:
                    particleFx = enemyShootAlertFxPooler.SpawnFromPool(position, rotation, useLocalTransform, parent, worldPositionStays);
                    break;
            }

            if (particleFx == null)
            {
                return null;
            }

            particleFx.timeToDespawn = Time.time + particleFx.lifeTimeInSeconds;
            queuedFxDespawns.Enqueue(particleFx);
            return particleFx;
        }

        // TODO -> Should I write a generic despawner ??
        private void HandleHitFxDespawns()
        {
            if (queuedFxDespawns.Count == 0)
            {
                return;
            }

            var firstDespawn = queuedFxDespawns.Peek();
            if (firstDespawn.timeToDespawn <= Time.time)
            {
                queuedFxDespawns.Dequeue();
                if (firstDespawn.enemyAction == EnemyAction.DamageHit)
                {
                    enemyHitFxPooler.DespawnObject(firstDespawn);
                }
                if (firstDespawn.enemyAction == EnemyAction.Shoot)
                {
                    enemyShootAlertFxPooler.DespawnObject(firstDespawn);
                }
            }
        }
    }
}
