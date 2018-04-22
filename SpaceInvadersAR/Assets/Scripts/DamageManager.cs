using UnityEngine;

namespace Assets.Scripts
{
    public sealed class DamageManager : MonoBehaviour
    {
        private RPCDamageManager rpcDamageManager;
        private EnemyManager enemyManager;
        private GameScoreManager gameScoreManager;
        private PlayerManager playerManager;
        private AudioManager audioManager;

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            rpcDamageManager = components.rpcDamageManager;
            enemyManager = components.enemyManager;
            gameScoreManager = components.gameScoreManager;
            playerManager = components.playerManager;
            audioManager = components.audioManager;
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

        public void PlayerTakesDamage(int playerId, int damage)
        {
            var playerOwner = playerManager.ResolvePlayerOwner(playerId);
            if (playerOwner == null)
            {
                return; 
            }

            audioManager.Play("Player Damage Hit");
            playerOwner.ApplyDamage(damage);
        }

        public void ApplyPlayerDamage(int playerHit, int damageApplied)
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            var playerOwner = playerManager.ResolvePlayerOwner(playerHit);
            if (playerOwner != null)
            {
                playerOwner.ApplyDamage(damageApplied);

                var playerId = playerOwner.PlayerId;
                var isLocalPlayer = playerManager.IsLocalPlayer(playerOwner.PlayerId);
                if (!isLocalPlayer)
                {
                    rpcDamageManager.PlayerHit(playerId, damageApplied);
                }
                else
                {
                    audioManager.Play("Player Damage Hit");
                }

                var isPlayerHitAliveAfterDamage = playerOwner.IsAlive;
                if (!isPlayerHitAliveAfterDamage)
                {
                    playerManager.PlayerDied(playerId);
                }
            }
        }
    }
}
