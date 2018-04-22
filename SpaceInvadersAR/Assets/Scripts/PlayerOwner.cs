using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PlayerOwner : MonoBehaviour
    {
        [ShowOnly] public bool Spawned;
        [ShowOnly] public float Health;
        [ShowOnly] public float maxHealth = 100f;

        //public event EventHandler OnPlayerDied;

        public bool IsAlive { get { return Health > 0f; } }

        public bool SpawnedAndAlive
        {
            get { return Spawned && IsAlive; }
        }

        public float HealthUnitInterval
        {
            get { return Health / maxHealth; }
        }

        public Transform ArActiveCameraTransform
        {
            get
            {
                return arActiveCamera == null ? null : arActiveCamera.transform;
            }
        }

        public int PlayerId
        {
            get
            {
                if (photonPlayer != null)
                {
                    return photonPlayer.ID;
                }

                return -1;
            }
        }

        public WeaponHolder weaponHolder;

        private PlayerMovement playerMovement;
        private PhoneInputManager phoneInputManager;
        private Camera arActiveCamera;
        private PhotonView photonView;
        private RPCPlayerManager rpcPlayerManager;
        private AudioManager audioManager;
        private PhotonPlayer photonPlayer;

        private bool initialized;

        private void FindReferences()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            playerMovement = GetComponent<PlayerMovement>();
            weaponHolder = GetComponent<WeaponHolder>();
            photonView = GetComponent<PhotonView>();

            var vuforiaManager = components.vuforiaManager;
            arActiveCamera = vuforiaManager.arCamera;
            phoneInputManager = components.phoneInputManager;

            var globalComponents = GlobalReferenceManager.GlobalInstance;
            rpcPlayerManager = globalComponents.rpcPlayerManager;
            audioManager = globalComponents.audioManager;
        }

        public void Initialize()
        {
            FindReferences();
            Health = maxHealth;
            initialized = true;
        }

        void LateUpdate()
        {
            if (!initialized)
            {
                return;
            }

            if (photonView.isMine)
            {
                playerMovement.MovePlayer();
            }
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }

            if (photonView.isMine)
            {
                HandleControls();
            }
        }

        private void HandleControls()
        {
#if UNITY_EDITOR && UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.A))
            {
                HandleShooting();
            }
#elif UNITY_ANDROID
            if (phoneInputManager.TouchPressed(FingerTouch.One))
            {
                HandleShooting();
            }
#endif
        }

        private void HandleShooting()
        {
            var wasWeaponShot = weaponHolder.ShootActiveWeapon();
            if (wasWeaponShot)
            {
                audioManager.Play("Laser Shot");
                rpcPlayerManager.PlayerShootWeapon();
            }
        }

        // when a new player owner get instantiated
        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var components = GlobalReferenceManager.GlobalInstance;
            var playerManager = components.playerManager;

            photonPlayer = info.sender;
            playerManager.AddPlayer(this);
            Initialize();
        }

        public void ApplyDamage(int damage)
        {
            var healthAfterDamage = Health - damage;
            if (healthAfterDamage > 0)
            {
                Health = healthAfterDamage;
            }
            else
            {             
                SetPlayerDead();
                Health = 0f;
            }
        }

        private void SetPlayerDead()
        {
            //if (OnPlayerDied != null)
            //{
            //    OnPlayerDied(this, new EventArgs());
            //}
            gameObject.SetActive(false);
        }
    }
}