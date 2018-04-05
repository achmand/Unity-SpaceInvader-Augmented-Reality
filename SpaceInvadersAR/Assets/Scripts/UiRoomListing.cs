using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiRoomListing : MonoBehaviour
    {
        public Button roomListingButton;

        [SerializeField] private Text roomNameText;
        private Text RoomNameText
        {
            get { return roomNameText; }
        }

        public string RoomName { get; private set; }

        public bool Updated { get; set; }

        void Start()
        {
            var uiRoomLobbyPanel = MenuManager.Instance.uiMenuPanel.UiRoomLobbyPanel;
            if (uiRoomLobbyPanel == null)
            {
                return;
            }
            
            roomListingButton.onClick.AddListener(() => uiRoomLobbyPanel.OnClickJoinRoom(RoomName));
        }

        public void SetRoomName(string roomName)
        {
            RoomName = roomName;
            RoomNameText.text = RoomName;
        }

        private void OnDestroy()
        {
            roomListingButton.onClick.RemoveAllListeners();
        }
    }
}
