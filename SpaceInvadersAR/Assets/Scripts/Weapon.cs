using UnityEngine;

namespace Assets.Scripts
{
    public enum WeaponShootType
    {
        Hitscan, 
        Projectile
    }

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

        // weapon details
        [Header("Weapon Details")]
        public float damage;
        public int maxAmmo;
        public float fireRate; 

        [Header("Projectile Details")]
        public float bulletspeed;

        //[Header("Hitscan details")]
        //[HideInInspector] public LineRenderer lineRenderer;

        public void Initialize()
        {
            if (weaponShootType == WeaponShootType.Hitscan)
            {
                //lineRenderer = GetComponent<LineRenderer>();
            }

            gameObject.SetActive(false);
            //muzzleFlashEffect.gameObject.SetActive(false);
        }
    }
}