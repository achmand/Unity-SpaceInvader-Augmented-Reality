using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class GameScoreManager : MonoBehaviour
    {
        public Dictionary<int, int> roomPlayersScore;
            
        public int LocalPlayerScore
        {
            get
            {
                var localPhotonPlayer = playerManager.LocalPhotonPlayer;
                return !roomPlayersScore.ContainsKey(localPhotonPlayer.ID) ? 0 : roomPlayersScore[localPhotonPlayer.ID];
            }
        }

        [ShowOnly] public int globalScore;

        private PlayerManager playerManager;
        private ScoreCalculator scoreCalculator;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            playerManager = globalReferenceManager.playerManager;
            roomPlayersScore = new Dictionary<int, int>();
            scoreCalculator = new ScoreCalculator();
        }
        
        public void AddEnemyConflictScore(int playerId, ScorableActionType scorableActionType, EnemyType enemyType)
        {
            var addedScore = scoreCalculator.CalculateEnemyConflictScore(scorableActionType, enemyType);
            ChangeGameScore(playerId, addedScore);
        }

        private void ChangeGameScore(int playerId, int incrementedScore)
        {
            if (!roomPlayersScore.ContainsKey(playerId))
            {
                roomPlayersScore.Add(playerId, incrementedScore);
            }
            else
            {
                roomPlayersScore[playerId] += incrementedScore;
            }

            globalScore += incrementedScore;
        }
    }
}
