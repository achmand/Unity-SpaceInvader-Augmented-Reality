using UnityEngine;

namespace Assets.Scripts
{
    public class LobbyNetwork : MonoBehaviour
    {
        public bool autoSyncScene; 

        private void Start ()
        {
            print("Connecting to server...");
            PhotonNetwork.ConnectUsingSettings("0.0.1");
        }

        private void OnConnectedToMaster()
        {
            print("Connected to master.");
            PhotonNetwork.automaticallySyncScene = autoSyncScene;
            PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        private void OnJoinedLobby()
        {
            print("Joined Lobby.");
            if (!PhotonNetwork.inRoom)
            {
                MenuManager.Instance.uiMenuPanel.UiRoomLobbyPanel.roomLobbyRectTransform.transform.SetAsLastSibling();
            }
        }
    }
}

