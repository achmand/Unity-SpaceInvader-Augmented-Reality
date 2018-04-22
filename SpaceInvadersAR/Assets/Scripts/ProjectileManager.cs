using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    // handles all projectiles in game
    public sealed class ProjectileManager : MonoBehaviour
    {
        private readonly Dictionary<EnemyType, ProjectileType[]> enemyAllowedProjectileCollection =
            new Dictionary<EnemyType, ProjectileType[]>(EnemyTypeEqualityComparer.Default)
            {
                {EnemyType.SimpleDroid, new[] {ProjectileType.SimpleProjectile}},
                {EnemyType.HeavyDroid, new[] {ProjectileType.SimpleProjectile}}
            };

        private List<Projectile> projectilesFired;
        private ProjectilePoolManager projectilePoolManager;
        private EnemyPoolManager enemyPoolManager;
        private AudioManager audioManager;
        private DamageManager damageManager;

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            projectilePoolManager = components.projectilePoolManager;
            enemyPoolManager = components.enemyPoolManager;
            audioManager = components.audioManager;
            damageManager = components.damageManager;

            projectilesFired = new List<Projectile>(10);
        }

        void FixedUpdate()
        {
            HandleFiredProjectile();
        }

        public void FireProjectileEnemy(EnemyBase enemyFiring, Vector3 target)
        {
            var enemyType = enemyFiring.EnemyType;
            if (!enemyAllowedProjectileCollection.ContainsKey(enemyType))
            {
                return;
            }

            var enemyProjectileTypes = enemyAllowedProjectileCollection[enemyType];
            var projectileType = enemyProjectileTypes[0]; // TODO -> select randomly from types

            var enemyTransform = enemyFiring.transform;
            var enemyPosition = enemyTransform.position;
            var enemyRotation = enemyTransform.rotation;

            enemyPoolManager.SpawnHitFx(EnemyAction.Shoot, enemyPosition, Quaternion.identity);
            var spawnedProjectile = projectilePoolManager.SpawnProjectile(projectileType, enemyPosition, enemyRotation);
            spawnedProjectile.timeToDespawn = Time.time + spawnedProjectile.timeBeforeDespawnSeconds;
            spawnedProjectile.AddForce(enemyPosition, target);
            audioManager.Play("Enemy Shoot");

            projectilesFired.Add(spawnedProjectile);
        }

        //  if I will be adding projectile fires for players, code must go here

        private void HandleFiredProjectile()
        {
            if (projectilesFired.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < projectilesFired.Count; i++)
            {
                var projectile = projectilesFired[i];
                var projectileType = projectile.projectileType;

                if (projectile.hitTargetId != null)
                {
                    var playerHit = projectile.hitTargetId.Value;
                    var projectileDamage = projectile.projectileDamage;
                    damageManager.ApplyPlayerDamage(playerHit, projectileDamage);

                    projectilesFired.Remove(projectile);
                    projectilePoolManager.DespawnProjectile(projectileType, projectile);
                }

                if (projectile.timeToDespawn <= Time.time)
                {
                    projectilesFired.Remove(projectile);
                    projectilePoolManager.DespawnProjectile(projectileType, projectile);
                }
            }
        }
    }
}
