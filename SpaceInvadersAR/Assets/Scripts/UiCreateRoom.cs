using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    // TODO -> USE COMMONGLOBAVARIABLES FOR MAXPLAYERS 
    public class UiCreateRoom : MonoBehaviour
    {
        public Button createRoomButton;

        [SerializeField] private Text roomName;
        private Text RoomName
        {
            get { return roomName; }
        }

        private void Awake()
        {
            createRoomButton.onClick.AddListener(OnClick_CreateRoom);
        }

        public void OnClick_CreateRoom()
        {
            if (string.IsNullOrEmpty(RoomName.text))
            {
                return;
            }

            var roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 4};
            if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
            {
                print("Create room successfully sent.");
            }
            else
            {
                print("Create room failed to send.");
            }
        }

        private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
        {
            print("Create room failed: " + codeAndMessage[1]);
        }

        private void OnCreatedRoom()
        {
            print("Room created successfully.");
        }

        private void OnDestroy()
        {
            createRoomButton.onClick.RemoveAllListeners();
        }
    }
}
