using UnityEngine;

namespace Assets.Scripts
{
    public sealed class UiMenuPanel : MonoBehaviour
    {
        [SerializeField] private UiRoomLobbyPanel uiRoomLobbyPanel;
        public UiRoomLobbyPanel UiRoomLobbyPanel
        {
            get { return uiRoomLobbyPanel; }
        }

        [SerializeField]
        private UiCurrentRoomLobbyPanel uiCurrentRoomLobbyPanel;
        public UiCurrentRoomLobbyPanel UiCurrentRoomLobbyPanel
        {
            get { return uiCurrentRoomLobbyPanel; }
        }

        private void Awake()
        {

        }
    }
}
