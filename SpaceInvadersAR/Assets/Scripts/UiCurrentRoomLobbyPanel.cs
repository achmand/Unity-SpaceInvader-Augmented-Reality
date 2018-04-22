using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiCurrentRoomLobbyPanel : MonoBehaviour
    {
        public RectTransform currentRoomRectTransform;
        public Button startGameDelayButton;

        void Awake()
        {
            startGameDelayButton.onClick.AddListener(OnClickStartDelayed);
        }

        void Update()
        {
            startGameDelayButton.interactable = PhotonNetwork.isMasterClient;
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
