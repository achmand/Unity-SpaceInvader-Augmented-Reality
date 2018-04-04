using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerBase : MonoBehaviour
    {
        [ShowOnly] public bool Spawned;
        [ShowOnly] public float Health;

        public bool IsAlive { get { return Health > 0f; } }
        public bool SpawnedAndAlive
        {
            get { return Spawned && IsAlive; }
        }

        private PhoneInputManager phoneInputManager;
        private WeaponHolder weaponHolder;

        void Awake()
        {
            var components = ClientReferenceManager.ClientInstance;
            phoneInputManager = components.phoneInputManager;

            //var arCamera = components.vuforiaManager.arCamera;
            //transform.SetParent(arCamera.transform, true);
            //transform.localPosition = Vector3.zero;
            //transform.localRotation = Quaternion.identity;

            weaponHolder = GetComponent<WeaponHolder>();
        }

        void Update()
        {
            HandleControls();
        }

        private void HandleControls()
        {
#if UNITY_EDITOR && UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.A))
            {
                weaponHolder.ShootActiveWeapon();
            }
#elif UNITY_ANDROID
            if (phoneInputManager.TouchPressed(FingerTouch.One))
            {
                weaponHolder.ShootActiveWeapon();
            }
#endif
        }
    }
}