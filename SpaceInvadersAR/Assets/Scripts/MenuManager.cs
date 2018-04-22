using UnityEngine;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public RectTransform menuContainer;
        public RectTransform lobbyContainer;
        public RectTransform currentRoomContainer;

        public UiRoomLobbyPanel uiRoomLobbyPanel;

        public static MenuManager Instance;

        void Awake()
        {
            Instance = this;
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlayTheme();

            lobbyContainer.gameObject.SetActive(false);
            currentRoomContainer.gameObject.SetActive(false);
        }

        public void ShowMenu()
        {
            EnableContainers(true, false, false);
        }

        public void ShowLobby()
        {
            EnableContainers(false, true, false);
        }

        public void ShowCurrentRoom()
        {
            EnableContainers(false, false, true);
        }

        private void EnableContainers(bool showMenu, bool showLobby, bool showCurrentRoom)
        {
            menuContainer.gameObject.SetActive(showMenu);
            lobbyContainer.gameObject.SetActive(showLobby);
            currentRoomContainer.gameObject.SetActive(showCurrentRoom);
        }
    }
}