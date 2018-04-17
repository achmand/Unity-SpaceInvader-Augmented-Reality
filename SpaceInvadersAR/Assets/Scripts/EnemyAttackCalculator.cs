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
    }
}
