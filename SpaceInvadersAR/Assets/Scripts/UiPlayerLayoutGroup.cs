using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiPlayerLayoutGroup : MonoBehaviour
    {
        [SerializeField]
        private RectTransform layoutGroupTransform;
        private RectTransform LayoutGroupTransform
        {
            get { return layoutGroupTransform; }
        }

        [SerializeField]
        private GameObject playerListingPrefab;
        private GameObject PlayerListingPrefab
        {
            get { return playerListingPrefab; }
        }

        public Button roomStateButton; 
        public Button leaveRoomButton; 

        private readonly Dictionary<string, UiPlayerListing> uiPlayerListings = new Dictionary<string, UiPlayerListing>();
        private Dictionary<string, UiPlayerListing> UiPlayerListing
        {
            get { return uiPlayerListings; }
        }

        void Awake()
        {
            roomStateButton.onClick.AddListener(OnClickRoomState);
            leaveRoomButton.onClick.AddListener(OnClickLeaveRoom);
        }

        // called by photon whenever the master client is switched
        private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
        {
            PhotonNetwork.LeaveRoom();
        }

        // called by photon whenever you join a room
        private void OnJoinedRoom()
        {
            // TODO -> Fix this shitty structure which Im following from a tutorial !!!
            // TODO -> Fix this shitty structure which Im following from a tutorial !!!
            // TODO -> Fix this shitty structure which Im following from a tutorial !!!

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var uiCurrentRoomLobbyPanel = MenuManager.Instance.uiMenuPanel.UiCurrentRoomLobbyPanel;
            uiCurrentRoomLobbyPanel.currentRoomRectTransform.transform.SetAsLastSibling();

            var photonPlayers = PhotonNetwork.playerList;
            for (int i = 0; i < photonPlayers.Length; i++)
            {
                var photonPlayer = photonPlayers[i];
                PlayerJoinedRoom(photonPlayer);
            }
        }

        // called by photon whenever a player is connected to room
        private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
        {
            PlayerJoinedRoom(photonPlayer);
        }

        // called by photon whenever a player leaves a room
        private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
        {
            PlayerLeftRoom(photonPlayer);
        }

        private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
        {
            if (photonPlayer == null)
            {
                return;
            }

            PlayerLeftRoom(photonPlayer);

            var playerListingEntry = Instantiate(PlayerListingPrefab).GetComponent<UiPlayerListing>();
            playerListingEntry.transform.SetParent(LayoutGroupTransform.transform, false);
            playerListingEntry.ApplyPhotonPlayer(photonPlayer);

            UiPlayerListing.Add(photonPlayer.NickName, playerListingEntry);
        }

        private void PlayerLeftRoom(PhotonPlayer photonPlayer)
        {
            var playerName = photonPlayer.NickName;
            if (UiPlayerListing.ContainsKey(playerName))
            {
                var playerListing = UiPlayerListing[playerName];
                Destroy(playerListing.gameObject);
                UiPlayerListing.Remove(playerName);
            }
        }

        private void OnClickRoomState()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
            PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
        }

        public void OnClickLeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
