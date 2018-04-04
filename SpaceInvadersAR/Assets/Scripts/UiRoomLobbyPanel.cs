using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class UiRoomLobbyPanel : MonoBehaviour
    {
        [SerializeField] private UiRoomLayoutGroup uiRoomLayoutGroup;
        private UiRoomLayoutGroup UiRoomLayoutGroup
        {
            get { return uiRoomLayoutGroup; }
        }

     

        public void OnClickJoinRoom(string roomName)
        {

        }
    }
}
