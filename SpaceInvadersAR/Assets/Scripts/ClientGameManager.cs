using UnityEngine;

namespace Assets.Scripts
{
    public enum GameState
    {
        None,
        StartLevelTransition,
        LevelTransitioning,
        LevelStarted,
        LevelEnding,
        GameStarted,
        GameEnded
    }

    // TODO -> Equality Comparers !!
    // TODO -> Replace anywhere were there is a proxy naming since they are clients ... 

    public sealed class CurrentGameDetails
    {
        public int currentLevelNo;
        public PlanetLevel currentLevel;

        public LevelDifficulty CurrentLevelDifficulty { get { return currentLevel.levelDifficulty; } }
    }

    public sealed class ClientGameManager : MonoBehaviour
    {
        public CurrentGameDetails currentGameDetails;

        private GameState currentGameState;

        private RPCGameManager rpcGameManager;
        private GameTimerManager gameTimerManager;
        private EnemyManager enemyManager;
        private LevelRepository levelRepository;
        private UiManager uiManager;
        private AudioManager audioManager;
        private PlayerManager playerManager;

        private void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            rpcGameManager = components.rpcGameManager;
            gameTimerManager = components.gameTimerManager;
            enemyManager = components.enemyManager;
            levelRepository = components.levelRepository;
            uiManager = components.uiManager;
            audioManager = components.audioManager;
            playerManager = components.playerManager;

            currentGameState = GameState.None;
            currentGameDetails = new CurrentGameDetails
            {
                currentLevelNo = 1
            };
        }

        void Update()
        {
            if (currentGameState == GameState.GameStarted)
            {
                var allPlayersJoinedGame = PlayerNetwork.Instance.playersInGame == playerManager.TotalRoomPlayers;
                if (allPlayersJoinedGame)
                {
                    StartLevelMaster();
                }
            }

            if (currentGameState == GameState.StartLevelTransition)
            {
                currentGameState = GameState.LevelTransitioning;
                StartLevelMaster();
            }

            if (currentGameState == GameState.LevelStarted)
            {
                CheckCurrentLevelState();
            }

            if (currentGameState == GameState.LevelEnding)
            {
                CheckLevelEndingState();
            }

            if (currentGameState == GameState.GameEnded)
            {
                GameEnded();
            }
        }

        // starting game 
        public void StartGame()
        {
            enemyManager.InitializeGame();
            currentGameState = GameState.GameStarted;
        }

        private void StartLevelMaster()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            var currentLevelNo = currentGameDetails.currentLevelNo;
            SetCurrentLevelDetails(currentLevelNo);
            var currentLevel = currentGameDetails.currentLevel;

            var now = PhotonNetwork.time;
            var levelTimeSeconds = currentLevel.levelTimeSeconds;

            gameTimerManager.SetLevelTimeLength(levelTimeSeconds);
            gameTimerManager.SetLevelStartTime(now);

            var levelNo = currentLevel.levelNo;
            var startingClusterType = enemyManager.CreateNewClusterMaster(false);

            rpcGameManager.StartLevelAck(levelNo, now, startingClusterType);

            currentGameState = GameState.LevelStarted;
            audioManager.Play("Level Start Jingle");
        }

        public void StartLevelClient(int levelNo, double levelStartTime, EnemyClusterType startingEnemyClusterType)
        {
            currentGameDetails.currentLevelNo = levelNo;
            SetCurrentLevelDetails(currentGameDetails.currentLevelNo);
            var currentLevel = currentGameDetails.currentLevel;

            var levelTimeSeconds = currentLevel.levelTimeSeconds;

            gameTimerManager.SetLevelTimeLength(levelTimeSeconds);
            gameTimerManager.SetLevelStartTime(levelStartTime);

            enemyManager.CreateNewCluster(startingEnemyClusterType);

            currentGameState = GameState.LevelStarted;
            audioManager.Play("Level Start Jingle");
        }

        private void SetCurrentLevelDetails(int levelNo)
        {
            if (currentGameDetails.currentLevel != null)
            {
                var currenPlanet = currentGameDetails.currentLevel.planet;
                currenPlanet.SetCurrentLevelRingActive(false);
            }

            var level = levelRepository.GetPlanetLevel(levelNo);
            level.planet.SetCurrentLevelRingActive(true);
            currentGameDetails.currentLevel = level;
            audioManager.PlayLevelTheme(levelNo);
        }

        private void CheckCurrentLevelState()
        {
            var secondsTillLevelEnd = gameTimerManager.SecondsTillLevelEnd;
            var levelEnded = secondsTillLevelEnd <= 0;
            if (levelEnded)
            {
                enemyManager.EndLevelCleanUp();
                audioManager.Play("Level End Jingle");
                uiManager.uiScoreBreakdown.DisplayBreakdownScores();
                currentGameState = GameState.LevelEnding;
            }

            var totalAlive = playerManager.totalAlivePlayers;
            if (totalAlive <= 0)
            {
                currentGameState = GameState.GameEnded; 
            }
        }

        private void CheckLevelEndingState()
        {
            if (uiManager.uiScoreBreakdown.showDisplayTransitionEnded)
            {
                var nextLevel = currentGameDetails.currentLevelNo + 1;
                if (nextLevel > levelRepository.lastLevel)
                {
                    // END GAME HERE
                    currentGameState = GameState.GameEnded;
                    return;
                }

                currentGameDetails.currentLevelNo = nextLevel;
                currentGameState = GameState.StartLevelTransition;
            }
        }

        private void GameEnded()
        {

        }
    }
}
