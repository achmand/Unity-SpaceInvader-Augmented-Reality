using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ScorableActionTypeEqualityComparer : IEqualityComparer<ScorableActionType>
    {
        private static ScorableActionTypeEqualityComparer defaultInstance;

        public static ScorableActionTypeEqualityComparer Default
        {
            get { return defaultInstance ?? (defaultInstance = new ScorableActionTypeEqualityComparer()); }
        }

        public bool Equals(ScorableActionType x, ScorableActionType y)
        {
            return x == y;
        }

        public int GetHashCode(ScorableActionType obj)
        {
            return (int)obj;
        }
    }
}
