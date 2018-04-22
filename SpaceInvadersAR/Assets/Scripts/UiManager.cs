using UnityEngine;

namespace Assets.Scripts
{
    public sealed class UiManager : MonoBehaviour
    {
        [HideInInspector] public UiScoreBreakdown uiScoreBreakdown;
        [HideInInspector] public UiGameOver uiGameOver;

        // TODO -> Add UiPlayerHUD

        void Awake()
        {
            uiScoreBreakdown = GetComponentInChildren<UiScoreBreakdown>();
            uiGameOver = GetComponentInChildren<UiGameOver>();

            uiScoreBreakdown.ShowPanel(false);
            uiGameOver.ShowPanel(false);
        }

        public void ShowGameOverPanel()
        {
            // TODO -> Disable HUD
            uiGameOver.ShowPanel(true);
        }
    }
}
