using System.Collections.Generic;

namespace Assets.Scripts
{
    public class LevelTypeEqualityComparer : IEqualityComparer<LevelDifficulty>
    {
        private static LevelTypeEqualityComparer defaultInstance;

        public static LevelTypeEqualityComparer Default
        {
            get { return defaultInstance ?? (defaultInstance = new LevelTypeEqualityComparer()); }
        }

        public bool Equals(LevelDifficulty x, LevelDifficulty y)
        {
            return x == y;
        }

        public int GetHashCode(LevelDifficulty obj)
        {
            return (int)obj;
        }
    }
}
