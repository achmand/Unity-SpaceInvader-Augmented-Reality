using UnityEngine;

namespace Assets.Scripts
{
    public sealed class RPCDamageManager : MonoBehaviour
    {
        private PhotonView photonView;
        private DamageManager damageManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            damageManager = globalReferenceManager.damageManager;

            photonView = GetComponent<PhotonView>();
        }

        public void EnemyHit(int playerId, int enemyId, float damage)
        {
            photonView.RPC("RPC_EnemyHit", PhotonTargets.All, playerId, enemyId, damage);
        }

        [PunRPC]
        private void RPC_EnemyHit(int playerId, int enemyId, float damage)
        {
            damageManager.EnemyTakesDamage(playerId, enemyId, damage);
        }

        public void PlayerHit(int playerId, int damage)
        {
            photonView.RPC("RPC_PlayerHit", PhotonTargets.Others, playerId, damage);
        }

        [PunRPC]
        private void RPC_PlayerHit(int playerId, int damage)
        {
            damageManager.PlayerTakesDamage(playerId, damage);
        }
    }
}
