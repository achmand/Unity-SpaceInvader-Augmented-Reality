using UnityEngine;

namespace Assets.Scripts
{
    public sealed class DamageManager : MonoBehaviour
    {
        private RPCDamageManager rpcDamageManager;
        private EnemyManager enemyManager;
        private GameScoreManager gameScoreManager;

        void Awake()
        {
            var globalReference = GlobalReferenceManager.GlobalInstance;
            rpcDamageManager = globalReference.rpcDamageManager;
            enemyManager = globalReference.enemyManager;
            gameScoreManager = globalReference.gameScoreManager;
        }

        public void CheckPlayerShot(int playerId, PlayerOwner playerOwner)
        {
            if (enemyManager.CurrentlySpawningEnemies) // cannot hit enemies while spawning 
            {
                return;
            }

            var playerOwnerTransform = playerOwner.transform;

            RaycastHit hit;
            Debug.DrawRay(playerOwnerTransform.position, playerOwnerTransform.forward * 100f, Color.green, 2f);
            if (Physics.Raycast(playerOwnerTransform.position, playerOwnerTransform.forward, out hit, 100f))
            {
                var enemyTarget = hit.transform.GetComponent<EnemyBase>();
                if (enemyTarget != null)
                {
                    var enemyId = enemyTarget.enemyId;
                    var damage = playerOwner.weaponHolder.ActiveWeapon.damage;

                    rpcDamageManager.EnemyHit(playerId, enemyId, damage);
                }
            }
        }

        public void EnemyTakesDamage(int playerId, int enemyId, float damage)
        {
            var enemyHit = enemyManager.GetEnemyFromCluster(enemyId);
            if (enemyHit == null)
            {
                return;
            }

            var enemyDied = enemyManager.ApplyDamage(enemyId, damage);
            var scorableActionType = enemyDied ? ScorableActionType.EnemyKilled : ScorableActionType.EnemyHit;
            var enemyType = enemyHit.EnemyType;

            gameScoreManager.AddEnemyConflictScore(playerId, scorableActionType, enemyType);
        }
    }
}
