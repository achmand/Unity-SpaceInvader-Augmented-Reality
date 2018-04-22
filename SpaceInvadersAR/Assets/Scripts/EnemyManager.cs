using UnityEngine;

namespace Assets.Scripts
{
    // TODO -> Refactor some code from enemy cluster and here, must be more elegant than this !!!
    public sealed class EnemyManager : MonoBehaviour
    {
        public EnemyCluster enemyCluster;
        public bool CurrentlySpawningEnemies { get { return enemyCluster.SpawningCluster; } }

        private bool CanEnemyAttack { get { return !CurrentlySpawningEnemies && !enemyCluster.IsEnemyClusterEmpty; } }
        private CurrentGameDetails CurrentGameDetails { get { return clientGameManager.currentGameDetails; } }
        private LevelDifficulty CurrentLevelDifficulty { get { return CurrentGameDetails.CurrentLevelDifficulty; } }

        private RPCEnemyManager rpcEnemyManager;
        private ClientGameManager clientGameManager;
        private CooldownTimer enemyAttackCooldownTimer;
        private PlayerManager playerManager;
        private ProjectileManager projectileManager;
        private CooldownTimer attackCooldownTimer;

        private EnemyAttackCalculator enemyAttackCalculator;
        private bool initialized;

        public void InitializeGame()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            rpcEnemyManager = components.rpcEnemyManager;
            clientGameManager = components.clientGameManager;
            playerManager = components.playerManager;
            projectileManager = components.projectileManager;

            enemyCluster.Initialize();
            attackCooldownTimer = new CooldownTimer(4f);
            enemyAttackCalculator = new EnemyAttackCalculator();

            initialized = true;
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }

            if (enemyCluster.createNewCluster)
            {
                CreateNewClusterMaster();
            }

            EnemyAttack();
        }

        // returns true if enemy dies  
        public bool ApplyDamage(int enemyId, float damage)
        {
            return enemyCluster.ApplyDamage(enemyId, damage);
        }

        public EnemyBase GetEnemyFromCluster(int enemyId)
        {
            var currentEnemyClusterCollection = enemyCluster.currentEnemyCluster;
            if (!currentEnemyClusterCollection.ContainsKey(enemyId))
            {
                return null;
            }

            return currentEnemyClusterCollection[enemyId];
        }

        public EnemyClusterType CreateNewClusterMaster(bool sendClient = true)
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return EnemyClusterType.None;
            }

            var enemyClusterType = enemyCluster.CreateCluster(CurrentLevelDifficulty, true);

            if (sendClient)
            {
                rpcEnemyManager.CreateEnemyCluster(enemyClusterType);
            }

            return enemyClusterType;
        }

        public void CreateNewCluster(EnemyClusterType enemyClusterType)
        {
            enemyCluster.CreateCluster(CurrentLevelDifficulty, false, enemyClusterType);
        }

        public void EndLevelCleanUp()
        {
            enemyCluster.ClearCurrentCluster();
        }

        // initiate enemy attack 
        private void EnemyAttack()
        {
            if (PhotonNetwork.isMasterClient)
            {
                if (!CanEnemyAttack)
                {
                    return;
                }

                if (attackCooldownTimer.CanWeDoAction())
                {
                    var newRandomInterval = enemyAttackCalculator.GetAttackIntervalEnemies(CurrentLevelDifficulty);
                    attackCooldownTimer.UpdateIntervalSeconds(newRandomInterval);
                    attackCooldownTimer.UpdateActionTime();

                    var attackingEnemyId = enemyCluster.GetRandomEnemyId();
                    var attackingEnemy = GetEnemyFromCluster(attackingEnemyId);
                    if (attackingEnemy == null)
                    {
                        return;
                    }

                    var targetPlayer = playerManager.GetRandomPlayerOwner();
                    if (targetPlayer == null)
                    {
                        return;
                    }

                    var targetPlayerPostion = targetPlayer.transform.position;
                    projectileManager.FireProjectileEnemy(attackingEnemy, targetPlayerPostion);

                    var targetPlayerId = targetPlayer.PlayerId;
                    rpcEnemyManager.EnemyFire(attackingEnemyId, targetPlayerId);
                }
            }
        }

        // initiate enemy attack on client
        public void EnemyAttackClient(int attackingEnemyId, int playerTarget)
        {
            var attackingEnemy = GetEnemyFromCluster(attackingEnemyId);
            if (attackingEnemy == null)
            {
                return;
            }

            var targetPlayer = playerManager.ResolvePlayerOwner(playerTarget);
            if (targetPlayer == null)
            {
                return;
            }

            var targetPlayerPostion = targetPlayer.transform.position;
            projectileManager.FireProjectileEnemy(attackingEnemy, targetPlayerPostion);
        }
    }
}