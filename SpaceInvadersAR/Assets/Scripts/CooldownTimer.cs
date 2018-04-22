using UnityEngine;

namespace Assets.Scripts
{
    public sealed class CooldownTimer
    {
        private float LastActionDoneTime;
        public float IntervalSeconds;

        private bool lastActionDone;

        public CooldownTimer(float intervalSeconds)
        {
            IntervalSeconds = intervalSeconds;
        }

        public void UpdateActionTime()
        {
            LastActionDoneTime = Time.time;
            lastActionDone = true;
        }

        public void UpdateIntervalSeconds(float intervalSeconds)
        {
            IntervalSeconds = intervalSeconds;
        }

        public bool CanWeDoAction()
        {
            var canWeDoAction = true;
            if (lastActionDone)
            {
                var secondsSinceLastAction = Time.time - LastActionDoneTime;
                canWeDoAction = secondsSinceLastAction > IntervalSeconds;
            }

            return canWeDoAction;
        }
    }
}
