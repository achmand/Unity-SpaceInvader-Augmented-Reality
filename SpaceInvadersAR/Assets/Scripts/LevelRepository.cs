using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class LevelRepository : MonoBehaviour
    {
        private Dictionary<int, PlanetLevel> planetLevelCollection;
        public int lastLevel;

        void Awake()
        {
            planetLevelCollection = new Dictionary<int, PlanetLevel>();
            var planetLevels = GetComponentsInChildren<PlanetLevel>();

            for (int i = 0; i < planetLevels.Length; i++)
            {
                var planetLevel = planetLevels[i];
                var levelNo = planetLevel.levelNo;

                planetLevelCollection.Add(levelNo, planetLevel);

                if (lastLevel < levelNo)
                {
                    lastLevel = levelNo;
                }
            }
        }

        public PlanetLevel GetPlanetLevel(int levelNo)
        {
            if (!planetLevelCollection.ContainsKey(levelNo))
            {
                return null;
            }

            return planetLevelCollection[levelNo];
        }
    }
}
