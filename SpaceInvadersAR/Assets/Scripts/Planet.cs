using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Planet : MonoBehaviour
    {
        //public float rotateAngle;
        //public Vector3 rotateAxis;

        public float offsetY;
        public Vector3 axis = Vector3.up;
        public float radius = 2.0f;
        public float radiusSpeed = 0.5f;
        public float rotationSpeed = 80.0f;

        public GameObject currentLevelRing; 

        [HideInInspector] public bool isLevelComplete;

        private Vector3 desiredPosition;
        private Vector3 centerPosition;

        void Awake()
        {
            centerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            transform.position = (transform.position - centerPosition).normalized * radius + centerPosition;
            SetCurrentLevelRingActive(false);
        }

        void Update()
        {
            transform.RotateAround(centerPosition, axis, rotationSpeed * Time.deltaTime);
            desiredPosition = (transform.position - centerPosition).normalized * radius + centerPosition;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }

        public void SetCurrentLevelRingActive(bool isActive)
        {
            currentLevelRing.SetActive(isActive);
        }
    }
}
