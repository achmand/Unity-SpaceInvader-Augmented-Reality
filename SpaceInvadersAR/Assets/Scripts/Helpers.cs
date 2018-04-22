using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class Helpers
    {
        public static T GetRandomElement<T>(this IList<T> elements)
        {
            var index = UnityEngine.Random.Range(0, elements.Count);
            if (index >= elements.Count)
            {
                return default(T);
            }

            return elements[index];
        }
    }
}
