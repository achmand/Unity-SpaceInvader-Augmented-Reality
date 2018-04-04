using System.Collections.Generic;

namespace Assets.Scripts
{
    public class WeaponTypeEqualityComparer : IEqualityComparer<WeaponType>
    {
        private static WeaponTypeEqualityComparer defaultInstance;

        public static WeaponTypeEqualityComparer Default
        {
            get { return defaultInstance ?? (defaultInstance = new WeaponTypeEqualityComparer()); }
        }

        public bool Equals(WeaponType x, WeaponType y)
        {
            return x == y;
        }

        public int GetHashCode(WeaponType obj)
        {
            return (int)obj;
        }
    }
}
