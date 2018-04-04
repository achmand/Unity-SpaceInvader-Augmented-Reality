using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    // TODO => Should I pool room listing ?? 
    public class UiRoomLayoutGroup : MonoBehaviour
    {
        [SerializeField]
        private RectTransform layoutGroupTransform;
        private RectTransform LayoutGroupTransform
        {
            get { return layoutGroupTransform; }
        }
        
        [SerializeField] private GameObject roomListingPrefab;
        private GameObject RoomListingPrefab
        {
            get { return roomListingPrefab; }
        }

        private readonly Dictionary<string, UiRoomListing> uiRoomListingsCollection = new Dictionary<string, UiRoomListing>();
        private Dictionary<string, UiRoomListing> UiRoomListingsCollection
        {
            get { return uiRoomListingsCollection; }
        }

        private void OnReceivedRoomListUpdate()
        {
            var roomInfos = PhotonNetwork.GetRoomList();
            for (int i = 0; i < roomInfos.Length; i++)
            {
                var roomInfo = roomInfos[i];
                RoomReceived(roomInfo);
            }

            RemovePreviousRooms();
        }

        private void RoomReceived(RoomInfo room)
        {
            var roomName = room.Name;
            if (!UiRoomListingsCollection.ContainsKey(room.Name))
            {
                if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
                {
                    var newRoomListingEntry = Instantiate(RoomListingPrefab).GetComponent<UiRoomListing>();
                    newRoomListingEntry.transform.SetParent(LayoutGroupTransform, false);
                    UiRoomListingsCollection.Add(roomName, newRoomListingEntry);
                }
            }
            else
            {
                var roomListing = UiRoomListingsCollection[roomName];
                roomListing.SetRoomName(roomName);
                roomListing.Updated = true;
            }
        }

        private void RemovePreviousRooms()
        {
            var removedRooms = new UiRoomListing[UiRoomListingsCollection.Count];
            var removeIndex = 0; 

            var enumerator = UiRoomListingsCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var roomListing = enumerator.Current;
                if (!roomListing.Value.Updated)
                {
                    removedRooms[removeIndex] = roomListing.Value;
                    removeIndex ++;
                }
                else
                {
                    roomListing.Value.Updated = false;
                }
            }

            for (int i = 0; i < removeIndex; i++)
            {
                var removeRoom = removedRooms[i];
                var removeRoomGameObj = removeRoom.gameObject;
                UiRoomListingsCollection.Remove(removeRoom.RoomName);

                Destroy(removeRoomGameObj);
            }
        }
    }
}
