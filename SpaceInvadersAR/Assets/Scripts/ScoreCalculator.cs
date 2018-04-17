using System.Collections.Generic;

namespace Assets.Scripts
{
    // different types of actions which can give score
    public enum ScorableActionType
    {
        EnemyHit,
        EnemyKilled
    }

    public sealed class ScoreCalculator
    {
        private readonly Dictionary<ScorableActionType, int> scorableActionCollection = new Dictionary<ScorableActionType, int>
        {
            { ScorableActionType.EnemyHit, 40},
            { ScorableActionType.EnemyKilled, 150}
        };

        private readonly Dictionary<EnemyType, float> enemyScorableActionMultipliers = new Dictionary<EnemyType, float>
        {
            { EnemyType.SimpleDroid, 1 },
            { EnemyType.WarriorDroid, 1.5f },
            { EnemyType.HeavyDroid, 2f },
            { EnemyType.BomberDroid, 2.7f }
        };


        public int CalculateEnemyConflictScore(ScorableActionType scorableActionType, EnemyType enemyType)
        {
            var scorableAction = scorableActionCollection[scorableActionType];
            var multiplier = enemyScorableActionMultipliers[enemyType];

            return (int)(scorableAction * multiplier);
        }
    }
}
