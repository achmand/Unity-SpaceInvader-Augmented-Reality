using UnityEngine;

namespace Assets.Scripts
{
    public class ClientReferenceManager : MonoBehaviour
    {
        public static ClientReferenceManager ClientInstance;

        [HideInInspector] public VuforiaManager vuforiaManager;
        [HideInInspector] public PhoneInputManager phoneInputManager;
        [HideInInspector] public AmmoRepository ammoRepository;
        [HideInInspector] public GamePoolManager gamePoolManager;

        private void FindReferences()
        {
            vuforiaManager = FindObjectOfType<VuforiaManager>();
            phoneInputManager = FindObjectOfType<PhoneInputManager>();
            ammoRepository = FindObjectOfType<AmmoRepository>();
            gamePoolManager = FindObjectOfType<GamePoolManager>();
            //objectPooler = FindObjectOfType<ObjectPooler>();
        }

        void Awake()
        {
            ClientInstance = this;
            FindReferences();
        }
    }
}