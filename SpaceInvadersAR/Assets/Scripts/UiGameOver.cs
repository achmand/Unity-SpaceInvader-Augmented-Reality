using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    // TODO -> Add base class for all these UI Panels 

    public sealed class UiGameOver : MonoBehaviour
    {
        [Header("References")]
        public RectTransform gameOverContainer;
        public Text totalPlayerScore;
        public Button spectateButton;

        private GameScoreManager gameScoreManager;
        private AudioManager audioManager;

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            gameScoreManager = components.gameScoreManager;
            audioManager = components.audioManager;

            spectateButton.onClick.AddListener(() => ShowPanel(false));
        }

        public void DisplayGameOverPanel()
        {
            audioManager.Play("Game Over");
            totalPlayerScore.text = gameScoreManager.LocalPlayerScore.ToString();
            ShowPanel(true);
        }

        public void ShowPanel(bool isShowing)
        {
            gameOverContainer.gameObject.SetActive(isShowing);
        }
    }
}
