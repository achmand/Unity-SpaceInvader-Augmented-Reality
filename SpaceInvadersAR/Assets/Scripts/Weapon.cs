using UnityEngine;

namespace Assets.Scripts
{
    public enum WeaponShootType
    {
        Hitscan, 
        Projectile
    }

    // TODO -> I Should use a repository here !!! 
    public class Weapon : MonoBehaviour
    {
        public GameObject shootHook; 
        public WeaponType weaponType;
        public WeaponShootType weaponShootType;
        public ParticleSystem muzzleFlashEffect;

        public int WeaponIdentifier
        {
            get { return (int) weaponType; }
        }

        [HideInInspector] public int currentAmmo;

        // weapon details
        [Header("Weapon Details")]
        public float damage;
        public int startingAmmo;
        public int maxAmmo;
        public float fireRate; 

        [Header("Projectile Details")]
        public float bulletspeed;

        //[Header("Hitscan details")]
        //[HideInInspector] public LineRenderer lineRenderer;

        public void Initialize()
        {
            if (startingAmmo > maxAmmo)
            {
                startingAmmo = maxAmmo/2;
            }

            currentAmmo = startingAmmo;
            if (weaponShootType == WeaponShootType.Hitscan)
            {
                //lineRenderer = GetComponent<LineRenderer>();
            }

            gameObject.SetActive(false);
            //muzzleFlashEffect.gameObject.SetActive(false);
        }
    }
}