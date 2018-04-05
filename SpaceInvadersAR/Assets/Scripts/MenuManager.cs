using UnityEngine;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        public UiMenuPanel uiMenuPanel;
        
        private void Awake()
        {
            uiMenuPanel = FindObjectOfType<UiMenuPanel>();
            Instance = this; 
        }
        
        //[SerializeField] private LobbyMa
    }
}