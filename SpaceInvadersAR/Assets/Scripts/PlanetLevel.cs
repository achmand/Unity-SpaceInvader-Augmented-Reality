using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PlanetLevel : MonoBehaviour
    {
        public int levelNo;
        public float levelTimeSeconds;

        public string levelName;
        public PlanetType planetType;
        public LevelDifficulty levelDifficulty;

        public Planet planet; 
    }
}
