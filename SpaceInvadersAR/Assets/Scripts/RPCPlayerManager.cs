using UnityEngine;

namespace Assets.Scripts
{
    public class RPCPlayerManager : MonoBehaviour
    {
        private PhotonView photonView;
        private DamageManager damageManager;
        private PlayerManager playerManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            damageManager = globalReferenceManager.damageManager;
            playerManager = globalReferenceManager.playerManager;

            photonView = GetComponent<PhotonView>();
        }

        public void PlayerShootWeapon()
        {
            photonView.RPC("RPC_PlayerShootWeapon", PhotonTargets.MasterClient, PhotonNetwork.player.ID);
        }

        [PunRPC]
        private void RPC_PlayerShootWeapon(int playerId)
        {
            // TODO -> Reduce ammo from proxies to avoid cheating !!!

            var playerOwner = playerManager.ResolvePlayerOwner(playerId);
            if (playerOwner == null)
            {
                return;
            }

            damageManager.CheckPlayerShot(playerId, playerOwner);
        }
    }
}
