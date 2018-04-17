using UnityEngine;

namespace Assets.Scripts
{
    public sealed class RPCGameManager : MonoBehaviour
    {
        private PhotonView photonView;
        private ClientGameManager clientGameManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            clientGameManager = globalReferenceManager.clientGameManager;

            photonView = GetComponent<PhotonView>();
        }

        public void StartLevelAck(int levelNo, double levelStartTime, EnemyClusterType startingEnemyClusterType)
        {
            photonView.RPC("RPC_StartLevelAck", PhotonTargets.Others, levelNo, levelStartTime, startingEnemyClusterType);
        }

        [PunRPC]
        private void RPC_StartLevelAck(int levelNo, double levelStartTime, EnemyClusterType startingEnemyClusterType)
        {
            clientGameManager.StartLevelClient(levelNo, levelStartTime, startingEnemyClusterType);
        }
    }
}
