using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiScoreBreakdown : MonoBehaviour
    {
        [Header("References")]
        public RectTransform scoreBreakdownContainer;
        public Text levelNumberText;
        public Text planetNameText;
        public Text difficultyText;
        public Text totalScoreText;
        public float breakDownScoreOnScreenSeconds;

        public bool showDisplayTransitionEnded; 
        //public bool debug_ShowbreakdownScore;

        private ClientGameManager clientGameManager;
        private GameScoreManager gameScoreManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            clientGameManager = globalReferenceManager.clientGameManager;
            gameScoreManager = globalReferenceManager.gameScoreManager;
        }

        //void Update()
        //{
        //    if (debug_ShowbreakdownScore)
        //    {
        //        debug_ShowbreakdownScore = false;
        //        DisplayBreakdownScores();
        //    }
        //}

        public void DisplayBreakdownScores()
        {
            StartCoroutine("ShowBreakdown");
        }

        private IEnumerator ShowBreakdown()
        {
            showDisplayTransitionEnded = false;
            ShowScoreBreakdown();
            ShowPanel(true);
            yield return new WaitForSeconds(breakDownScoreOnScreenSeconds);
            ShowPanel(false);
            showDisplayTransitionEnded = true;
        }

        private void ShowScoreBreakdown()
        {
            var currentLevel = clientGameManager.currentGameDetails.currentLevel;

            levelNumberText.text = "#" + currentLevel.levelNo;
            planetNameText.text = currentLevel.levelName;
            difficultyText.text = currentLevel.levelDifficulty.ToString();
            totalScoreText.text = gameScoreManager.globalScore.ToString();
        }

        public void ShowPanel(bool isShowing)
        {
            scoreBreakdownContainer.gameObject.SetActive(isShowing);
        }
    }
}
