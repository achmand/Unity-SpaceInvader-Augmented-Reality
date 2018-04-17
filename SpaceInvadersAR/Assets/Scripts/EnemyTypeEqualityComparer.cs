using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class EnemyTypeEqualityComparer : IEqualityComparer<EnemyType>
    {
        private static EnemyTypeEqualityComparer defaultInstance;

        public static EnemyTypeEqualityComparer Default
        {
            get { return defaultInstance ?? (defaultInstance = new EnemyTypeEqualityComparer()); }
        }

        public bool Equals(EnemyType x, EnemyType y)
        {
            return x == y;
        }

        public int GetHashCode(EnemyType obj)
        {
            return (int)obj;
        }
    }
}
