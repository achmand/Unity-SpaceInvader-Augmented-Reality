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

        private void Awake()
        {

        }
    }
}
