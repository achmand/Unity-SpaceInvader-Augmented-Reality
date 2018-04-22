using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiHealthBarHp : MonoBehaviour
    {
        [Header("Options")]
        public float progressBarSmoothSpeed = 1f;

        [Header("References")]
        public Slider healthProgressBar; 

        private PlayerManager playerManager;
        private bool initialized;

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            playerManager = components.playerManager;
            initialized = true;
        }

        void Update()
        {
            if (!initialized)
            {
                return; 
            }

            var localPlayer = playerManager.LocalPlayerOwner;
            if (localPlayer == null)
            {
                return;
            }

            // Player Health
            var healthUnitInterval = localPlayer.HealthUnitInterval;
            var interpolatedValue = Mathf.Lerp(healthProgressBar.value, healthUnitInterval, Time.deltaTime * progressBarSmoothSpeed);
            healthProgressBar.value = interpolatedValue;
        }
    }
}