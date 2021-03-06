﻿using UnityEngine;

namespace Assets.Scripts
{
    public sealed class GlobalReferenceManager : MonoBehaviour
    {
        public static GlobalReferenceManager GlobalInstance;

        [HideInInspector] public VuforiaManager vuforiaManager;
        [HideInInspector] public PhoneInputManager phoneInputManager;
        [HideInInspector] public ClientGameManager clientGameManager;
        [HideInInspector] public GameTimerManager gameTimerManager;
        [HideInInspector] public AudioManager audioManager;
        [HideInInspector] public PlayerManager playerManager;
        [HideInInspector] public GameScoreManager gameScoreManager;
        [HideInInspector] public DamageManager damageManager;
        [HideInInspector] public GamePoolManager gamePoolManager;
        [HideInInspector] public EnemyPoolManager enemyPoolManager;
        [HideInInspector] public ProjectilePoolManager projectilePoolManager; 
        [HideInInspector] public EnemyManager enemyManager;
        [HideInInspector] public ProjectileManager projectileManager; 
        [HideInInspector] public EnemyClusterFormationRepository enemyClusterFormationRepository;
        [HideInInspector] public LevelRepository levelRepository;
        [HideInInspector] public UiManager uiManager;

        [HideInInspector] public RPCGameManager rpcGameManager;
        [HideInInspector] public RPCPlayerManager rpcPlayerManager;
        [HideInInspector] public RPCEnemyManager rpcEnemyManager;
        [HideInInspector] public RPCDamageManager rpcDamageManager;

        private void FindReferences()
        {
            vuforiaManager = FindObjectOfType<VuforiaManager>();
            phoneInputManager = FindObjectOfType<PhoneInputManager>();
            clientGameManager = FindObjectOfType<ClientGameManager>();
            gameTimerManager = FindObjectOfType<GameTimerManager>();
            audioManager = FindObjectOfType<AudioManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            gameScoreManager = FindObjectOfType<GameScoreManager>();
            damageManager = FindObjectOfType<DamageManager>();
            gamePoolManager = FindObjectOfType<GamePoolManager>();
            enemyPoolManager = FindObjectOfType<EnemyPoolManager>();

            enemyManager = FindObjectOfType<EnemyManager>();
            projectilePoolManager = FindObjectOfType<ProjectilePoolManager>();
            projectileManager = FindObjectOfType<ProjectileManager>();
            enemyClusterFormationRepository = FindObjectOfType<EnemyClusterFormationRepository>();
            levelRepository = FindObjectOfType<LevelRepository>();
            uiManager = FindObjectOfType<UiManager>();

            rpcGameManager = FindObjectOfType<RPCGameManager>();
            rpcPlayerManager = FindObjectOfType<RPCPlayerManager>();
            rpcEnemyManager = FindObjectOfType<RPCEnemyManager>();
            rpcDamageManager = FindObjectOfType<RPCDamageManager>();
        }

        void Awake()
        {
            GlobalInstance = this;
            FindReferences();
        }
    }
}
