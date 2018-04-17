using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiGameScore : MonoBehaviour
    {
        public Text globalScoreText;
        public Text playerScoreText;

        private int lastGlobalScore;
        private int lastPlayerScore;

        private GameScoreManager gameScoreManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            gameScoreManager = globalReferenceManager.gameScoreManager;

            globalScoreText.text = "0";
            playerScoreText.text = "0";
        }

        void Update()
        {
            var currentGlobalScore = gameScoreManager.globalScore;
            if (lastGlobalScore != currentGlobalScore)
            {
                lastGlobalScore = currentGlobalScore;
                globalScoreText.text = lastGlobalScore.ToString();
            }

            var currentPlayerScore = gameScoreManager.LocalPlayerScore;
            if (lastPlayerScore != currentPlayerScore)
            {
                lastPlayerScore = currentPlayerScore;
                playerScoreText.text = lastPlayerScore.ToString();
            }
        }
    }
}
