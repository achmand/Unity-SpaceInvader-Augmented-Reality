using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    // TODO -> dont use .tostring here since it is called frequently and may cause a lot of GC allocation
    public sealed class UiWeaponHolder : MonoBehaviour
    {
        public Text maxAmmoText;
        public Text currentAmmoText;

        private int lastMaxAmmo;
        private int lastCurrentAmmo;

        private PlayerManager playerManager;

        void Awake()
        {
            var globalReferenceManager = GlobalReferenceManager.GlobalInstance;
            playerManager = globalReferenceManager.playerManager;
        }

        void Update()
        {
            var localPlayer = playerManager.LocalPlayerOwner;
            if (localPlayer == null)
            {
                return; 
            }

            var weaponHolder = localPlayer.weaponHolder;

            var maxAmmo = weaponHolder.ActiveWeapon.maxAmmo;
            if (lastMaxAmmo != maxAmmo)
            {
                lastMaxAmmo = maxAmmo;
                maxAmmoText.text = lastMaxAmmo.ToString();
            }

            var currentAmmo = weaponHolder.ActiveWeapon.currentAmmo;
            if (lastCurrentAmmo != currentAmmo)
            {
                lastCurrentAmmo = currentAmmo;
                currentAmmoText.text = lastCurrentAmmo.ToString();
            }
        }
    }
}
