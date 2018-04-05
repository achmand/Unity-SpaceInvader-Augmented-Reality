using UnityEngine;

namespace Assets.Scripts
{
    public class UiRoomLobbyPanel : MonoBehaviour
    {
        public RectTransform roomLobbyRectTransform;

        [SerializeField] private UiRoomLayoutGroup uiRoomLayoutGroup;
        private UiRoomLayoutGroup UiRoomLayoutGroup
        {
            get { return uiRoomLayoutGroup; }
        }

        public void OnClickJoinRoom(string roomName)
        {
            if (PhotonNetwork.JoinRoom(roomName))
            {
                print("Joined room.");
            }
            else
            {
                print("Join room failed.");
            }
        }
    }
}
