using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerNetwork : MonoBehaviour
    {
        public static PlayerNetwork Instance;
        public string PlayerName { get; private set; }

        private void Awake()
        {
            Instance = this;
            PlayerName = "Dylan " + Random.Range(1000, 9999);
        }
    }
}