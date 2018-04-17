using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiCurrentRoomLobbyPanel : MonoBehaviour
    {
        public RectTransform currentRoomRectTransform;
        public Button startGameSyncButton;
        public Button startGameDelayButton;

        void Awake()
        {
            startGameSyncButton.onClick.AddListener(OnClickStartSync);
            startGameDelayButton.onClick.AddListener(OnClickStartDelayed);
        }

        public void OnClickStartSync()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            PhotonNetwork.LoadLevel("ClientScene");
        }

        public void OnClickStartDelayed()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.IsVisible = false;
            PhotonNetwork.LoadLevel("ClientScene");
        }
    }
}
