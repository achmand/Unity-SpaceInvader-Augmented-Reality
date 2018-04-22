using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UiRoomLobbyPanel : MonoBehaviour
    {
        public Button backMenu;
        public RectTransform roomLobbyRectTransform;
        public Text usernameText;

        [SerializeField] private UiRoomLayoutGroup uiRoomLayoutGroup;
        private UiRoomLayoutGroup UiRoomLayoutGroup
        {
            get { return uiRoomLayoutGroup; }
        }

        void Awake()
        {
            backMenu.onClick.AddListener(MenuManager.Instance.ShowMenu);
        }

        public void OnClickJoinRoom(string roomName)
        {
            if (PhotonNetwork.JoinRoom(roomName))
            {
                if (!string.IsNullOrEmpty(usernameText.text))
                {
                    PhotonNetwork.playerName = usernameText.text;
                }

                print("Joined room.");
            }
            else
            {
                print("Join room failed.");
            }
        }
    }
}
