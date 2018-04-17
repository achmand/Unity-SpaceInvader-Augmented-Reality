using UnityEngine;

namespace Assets.Scripts
{
    public sealed class UiManager : MonoBehaviour
    {
        public UiScoreBreakdown uiScoreBreakdown;
        // TODO -> Add UiPlayerHUD

        void Awake()
        {
            uiScoreBreakdown = GetComponentInChildren<UiScoreBreakdown>();
            uiScoreBreakdown.ShowPanel(false);
        }
    }
}
