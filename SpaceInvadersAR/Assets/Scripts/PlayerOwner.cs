using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PlayerOwner : MonoBehaviour
    {
        [ShowOnly] public bool Spawned;
        [ShowOnly] public float Health;

        public bool IsAlive { get { return Health > 0f; } }

        public bool SpawnedAndAlive
        {
            get { return Spawned && IsAlive; }
        }

        public Transform ArActiveCameraTransform
        {
            get
            {
                return arActiveCamera == null ? null : arActiveCamera.transform;
            }
        }

        public WeaponHolder weaponHolder;

        private PlayerMovement playerMovement;
        private PhoneInputManager phoneInputManager;
        private Camera arActiveCamera;
        private PhotonView photonView;
        private RPCPlayerManager rpcPlayerManager;

        private bool initialized;

        private void FindReferences()
        {
            var components = ClientReferenceManager.ClientInstance;
            playerMovement = GetComponent<PlayerMovement>();
            weaponHolder = GetComponent<WeaponHolder>();
            photonView = GetComponent<PhotonView>();

            var vuforiaManager = components.vuforiaManager;
            arActiveCamera = vuforiaManager.arCamera;
            phoneInputManager = components.phoneInputManager;

            var globalComponents = GlobalReferenceManager.GlobalInstance;
            rpcPlayerManager = globalComponents.rpcPlayerManager;
        }

        public void Initialize()
        {
            FindReferences();
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
                rpcPlayerManager.PlayerShootWeapon();
            }
        }

        // when a new player owner get instantiated
        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            var playerManager = globalReferenceManager.playerManager;
            var photonPlayerId = info.sender.ID;

            playerManager.AddPlayer(photonPlayerId, this);
            Initialize();
        }

        //[PunRPC]
        //private void RPC_PlayerShootWeapon(int playerId)
        //{
        //    var photonPlayer = PhotonNetwork.playerList.SingleOrDefault(p => p.ID == playerId);
        //    if (photonPlayer == null)
        //    {
        //        return;
        //    }

        //    var arCameraTransform = arActiveCamera.transform;

        //    RaycastHit hit;
        //    Debug.DrawRay(arCameraTransform.position, arCameraTransform.forward * 100f, Color.green, 2f);
        //    if (Physics.Raycast(arCameraTransform.position, arCameraTransform.forward, out hit, 100f))
        //    {
        //        var enemyTarget = hit.transform.GetComponent<EnemyBase>();
        //        if (enemyTarget != null)
        //        {
        //            var damage = weaponHolder.ActiveWeapon.damage;
        //           enemyTarget.TakeDamage(damage);
        //            Debug.Log(hit.transform.name);
        //        }
        //    }
        //}
    }
}