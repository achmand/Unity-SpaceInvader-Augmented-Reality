using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiPlayerListing : MonoBehaviour
    {
        public PhotonPlayer PhotonPlayer { get; private set; }

        [SerializeField] private Text playerName;
        private Text PlayerName { get { return playerName; } }

        public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
        {
            PhotonPlayer = photonPlayer;
            PlayerName.text = photonPlayer.NickName;
        }
    }
}
