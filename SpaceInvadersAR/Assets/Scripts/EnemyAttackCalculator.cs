using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public sealed class EnemyAttackCalculator
    {
        private readonly Random random;

        public EnemyAttackCalculator()
        {
            random = new Random();
        }

        private readonly Dictionary<LevelDifficulty, int[]> enemyAttackingTotalCollection =
            new Dictionary<LevelDifficulty, int[]>(LevelTypeEqualityComparer.Default)
            {
                {LevelDifficulty.Easy, new[] {2, 3}},
                {LevelDifficulty.Medium, new[] {4, 6}},
                {LevelDifficulty.Hard, new[] {6, 9}},
                {LevelDifficulty.Insane, new[] {9, 13}}
            };

        private readonly Dictionary<LevelDifficulty, int[]> enemyAttackingIntervalCollection =
           new Dictionary<LevelDifficulty, int[]>(LevelTypeEqualityComparer.Default)
           {
                {LevelDifficulty.Easy, new[] {4, 5}},
                {LevelDifficulty.Medium, new[] {3, 5}},
                {LevelDifficulty.Hard, new[] {2, 4}},
                {LevelDifficulty.Insane, new[] {1, 2}}
           };


        public int GetTotalAttackingEnemies(LevelDifficulty levelDifficulty)
        {
            if (!enemyAttackingTotalCollection.ContainsKey(levelDifficulty))
            {
                return 0;
            }

            var totalAttackingEnemies = enemyAttackingTotalCollection[levelDifficulty];
            var randomizeTotal = random.Next(totalAttackingEnemies[0], totalAttackingEnemies[1]);
            return randomizeTotal;
        }

        public int GetAttackIntervalEnemies(LevelDifficulty levelDifficulty)
        {
            if (!enemyAttackingIntervalCollection.ContainsKey(levelDifficulty))
            {
                return 0;
            }

            var attackingInterval = enemyAttackingIntervalCollection[levelDifficulty];
            var randomizeTotal = random.Next(attackingInterval[0], attackingInterval[1]);
            return randomizeTotal;
        }
    }
}
