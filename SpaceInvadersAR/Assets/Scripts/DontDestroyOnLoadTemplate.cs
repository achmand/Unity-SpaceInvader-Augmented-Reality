using UnityEngine;

namespace Assets.Scripts
{
    public class DontDestroyOnLoadTemplate : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}