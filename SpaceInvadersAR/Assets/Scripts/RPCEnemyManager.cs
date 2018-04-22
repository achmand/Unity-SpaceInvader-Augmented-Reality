using UnityEngine;

namespace Assets.Scripts
{
    // TODO -> Should I make a base class for all these rpc enemy managers ? and have something which adds a photon view automatically ?

    // handles rpc methods for enemies
    public sealed class RPCEnemyManager : MonoBehaviour
    {
        private PhotonView photonView;
        private EnemyManager enemyManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            enemyManager = globalReferenceManager.enemyManager;

            photonView = GetComponent<PhotonView>();
        }

        public void CreateEnemyCluster(EnemyClusterType enemyClusterType)
        {
            photonView.RPC("RPC_CreateEnemyCluster", PhotonTargets.Others, enemyClusterType);
        }

        [PunRPC]
        private void RPC_CreateEnemyCluster(EnemyClusterType enemyClusterType)
        {
            enemyManager.CreateNewCluster(enemyClusterType);
        }

        public void EnemyFire(int attackingEnemyId, int playerTarget)
        {
            photonView.RPC("RPC_EnemyFire", PhotonTargets.Others, attackingEnemyId, playerTarget);
        }

        [PunRPC]
        private void RPC_EnemyFire(int attackingEnemyId, int playerTarget)
        {
            enemyManager.EnemyAttackClient(attackingEnemyId, playerTarget);
        }
    }
}
