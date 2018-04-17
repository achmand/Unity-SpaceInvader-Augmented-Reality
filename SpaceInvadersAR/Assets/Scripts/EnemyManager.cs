using UnityEngine;

namespace Assets.Scripts
{
    // TODO -> Add current game mode details ... 
    // TODO -> Refactor some code from enemy cluster and here, must be more elegant than this !!!
    public sealed class EnemyManager : MonoBehaviour
    {
        public EnemyCluster enemyCluster;
        public bool CurrentlySpawningEnemies { get { return enemyCluster.SpawningCluster; } }

        private RPCEnemyManager rpcEnemyManager;
        private ClientGameManager clientGameManager;

        private bool initialized;

        public void InitializeGame()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            rpcEnemyManager = globalReferenceManager.rpcEnemyManager;
            clientGameManager = globalReferenceManager.clientGameManager;

            enemyCluster.Initialize();
            //CreateNewClusterMaster();

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

        public EnemyClusterType CreateNewClusterMaster(bool sendProxy = true)
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return EnemyClusterType.None;
            }

            var currentGameDetails = clientGameManager.currentGameDetails;
            var currentLevelDifficulty = currentGameDetails.CurrentLevelDifficulty;
            var enemyClusterType = enemyCluster.CreateCluster(currentLevelDifficulty, true);

            if (sendProxy)
            {
                rpcEnemyManager.CreateEnemyCluster(enemyClusterType);
            }

            return enemyClusterType;
        }

        public void CreateNewCluster(EnemyClusterType enemyClusterType)
        {
            var currentGameDetails = clientGameManager.currentGameDetails;
            var currentLevelDifficulty = currentGameDetails.CurrentLevelDifficulty;
            enemyCluster.CreateCluster(currentLevelDifficulty, false, enemyClusterType);
        }

        public void EndLevelCleanUp()
        {
            enemyCluster.ClearCurrentCluster();
        }
    }
}