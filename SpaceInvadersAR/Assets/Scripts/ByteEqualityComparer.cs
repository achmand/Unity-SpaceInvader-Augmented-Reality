using System.Collections.Generic;

namespace Assets.Scripts
{
    public sealed class ByteEqualityComparer : IEqualityComparer<byte>
    {
        private static ByteEqualityComparer defaultInstance;

        public static ByteEqualityComparer Default
        {
            get { return defaultInstance ?? (defaultInstance = new ByteEqualityComparer()); }
        }

        public bool Equals(byte x, byte y)
        {
            return x == y;
        }

        public int GetHashCode(byte obj)
        {
            return obj;
        }
    }
}
