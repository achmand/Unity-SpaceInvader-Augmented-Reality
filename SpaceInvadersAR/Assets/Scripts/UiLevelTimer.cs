using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiLevelTimer : MonoBehaviour
    {
        [Header("Time References")]
        public RectTransform timerContainer;
        public Text timerMinutesText;
        public Text timerSecondsText;

        private GameTimerManager gameTimerManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            gameTimerManager = globalReferenceManager.gameTimerManager;
        }

        void Update()
        {
            var secondsTillLevelEnd = gameTimerManager.SecondsTillLevelEnd;
            var showTime = secondsTillLevelEnd >= 0;
            timerContainer.gameObject.SetActive(showTime);

            if (!showTime)
            {
                return;
            }

            var timeSpan = TimeSpan.FromSeconds(secondsTillLevelEnd);
            var minutesString = NumberStringsCollection.ResolveString(timeSpan.Minutes, true);
            var secondsString = NumberStringsCollection.ResolveString(timeSpan.Seconds, true);

            timerMinutesText.text = minutesString;
            timerSecondsText.text = secondsString;
        }
    }
}
