using UnityEngine;

namespace Assets.Scripts
{
    public sealed class GameTimerManager : MonoBehaviour
    {
        [ShowOnly] public double currentLevelStartTime;
        [ShowOnly] public double currentLevelTimeLengthSeconds;

        public float SecondsTillLevelEnd
        {
            get
            {
                if (currentLevelStartTime <= 0 || currentLevelTimeLengthSeconds < 0)
                {
                    return -1f;
                }

                var now = PhotonNetwork.time;
                var value = (float)((currentLevelStartTime + currentLevelTimeLengthSeconds) - now);
                if (value < 0)
                {
                    return -1f;
                }

                return value;
            }
        }

        public void ResetValues()
        {
            SetLevelStartTime(-1);
            SetLevelTimeLength(-1);
        }

        public void SetLevelStartTime(double levelStartTime)
        {
            currentLevelStartTime = levelStartTime;
        }

        public void SetLevelTimeLength(float levelTimeLengthSeconds)
        {
            currentLevelTimeLengthSeconds = levelTimeLengthSeconds;
        }
    }
}
