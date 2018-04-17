using UnityEngine;

namespace Assets.Scripts
{
    public sealed class FloatEasingApplier: MonoBehaviour
    {
        [HideInInspector] public float currentValue;

        [HideInInspector] private float destinationValue;
        private EasingTypes easeType;
        private float duration;
        private float counter;
        private EasingFunction.Function easingFunction;
        private bool startCalculation;

        public bool IsEasing
        {
            get { return counter <= 1 && startCalculation; }
        }

        public void ManualUpdate()
        {
            if (IsEasing)
            {
                counter += Time.fixedDeltaTime / duration;
                Calculate();
            }
            else
            {
                counter = 0;
                startCalculation = false;
            }
        }

        public void StartEase(float start, float destination, float timeInSeconds, EasingTypes easingType)
        {
            currentValue = start;
            destinationValue = destination;
            easeType = easingType;
            duration = timeInSeconds;
            easingFunction = EasingFunction.GetEasingFunction(easeType);
            startCalculation = true;
        }

        private void Calculate()
        {
            currentValue = easingFunction(currentValue, destinationValue, counter);

        }

        public void ResetObject()
        {
            currentValue = 0f;
            destinationValue = 0f;
            duration = 0;
        }
    }
}
