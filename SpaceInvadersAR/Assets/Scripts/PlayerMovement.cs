using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Player movement, which is dependent on the AR camera position. 
    /// </summary>
    public sealed class PlayerMovement : MonoBehaviour 
    {
        private Camera arCamera;

        public Vector3 positionOffset;
        public Vector3 rotationOffset;

        private Vector3 targetPosition;
        private Quaternion targetRotation;

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            arCamera = components.vuforiaManager.arCamera;
        }

        public void MovePlayer()
        {
            if (arCamera == null)
            {
                return;
            }

            transform.position = arCamera.transform.position + positionOffset;
            transform.eulerAngles = arCamera.transform.eulerAngles + rotationOffset;
        }
    }
}
