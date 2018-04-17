using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    // handles all projectiles in game
    public sealed class ProjectileManager : MonoBehaviour
    {
        private Dictionary<EnemyType, ProjectileType[]> enemyAllowedProjectileCollection =
            new Dictionary<EnemyType, ProjectileType[]>(EnemyTypeEqualityComparer.Default)
            {
                {EnemyType.SimpleDroid, new[] {ProjectileType.SimpleProjectile}}
            };

        private ProjectilePoolManager projectilePoolManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            projectilePoolManager = globalReferenceManager.projectilePoolManager;
        }

        public void EnemyFireProjectile(EnemyBase enemyFiring)
        {
            var enemyType = enemyFiring.EnemyType;
            var projectileType = ProjectileType.SimpleProjectile;
        }

        //  if I will be adding projectile fires for players, code must go here
    }
}
