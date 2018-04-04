using UnityEngine;

namespace Assets.Scripts
{
    public enum PlayerActions
    {
        None,
        FireLaser
    }

    public enum FingerTouch
    {
        One = 1,
        Two = 2
    }

    /// <summary>
    /// Phone input manager.
    /// </summary>
    public sealed class PhoneInputManager : MonoBehaviour
    {
        #region touch phases

        // TODO -> Use enums instead of passing index as int !!!
        // TODO -> Use enums instead of passing index as int !!!
        // TODO -> Use enums instead of passing index as int !!!

        public bool TouchPhaseBegan(int index)
        {
            return Input.GetTouch(index).phase == TouchPhase.Began;
        }

        public bool TouchPhaseMoved(int index)
        {
            return Input.GetTouch(index).phase == TouchPhase.Moved;
        }

        public bool TouchPhaseEnded(int index)
        {
            return Input.GetTouch(index).phase == TouchPhase.Ended;
        }

        public bool TouchPhaseStationary(int index)
        {
            return Input.GetTouch(index).phase == TouchPhase.Stationary;
        }

        public bool TouchPhaseCancelled(int index)
        {
            return Input.GetTouch(index).phase == TouchPhase.Canceled;
        }

        public bool AnyTouchPressed()
        {
            return Input.touchCount > 0;
        }

        public bool TouchPressed(FingerTouch fingerTouch)
        {
            return Input.touchCount == (int) fingerTouch;
        }

        #endregion
    }
}
