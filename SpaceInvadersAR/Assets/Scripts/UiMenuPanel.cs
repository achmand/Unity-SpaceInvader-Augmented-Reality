using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiMenuPanel : MonoBehaviour
    {
        public Button playButton;
        public Button leaderBoardButton;
        public Button tutorialButton;
        public Button exitButton;

        private void Awake()
        {
            MenuManager.Instance.ShowMenu();
            playButton.onClick.AddListener(MenuManager.Instance.ShowLobby);
            exitButton.onClick.AddListener(ExitApplication);
        }

        private void ExitApplication()
        {
            Application.Quit();
        }
    }
}
