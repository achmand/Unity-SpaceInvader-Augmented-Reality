using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    // TODO -> Add more weapons (Projectiles too)
    public sealed class WeaponHolder : MonoBehaviour
    {
        //public event EventHandler OnFiringActiveWeapon;

        public int ActiveWeaponIdentitifier { get; private set; }

        public Weapon ActiveWeapon { get { return weaponsCollection[(WeaponType)ActiveWeaponIdentitifier]; } }

        private Dictionary<WeaponType, Weapon> weaponsCollection;

        private AmmoRepository ammoRepository;
        private CooldownTimer nextFireCooldownTimer;

        void Awake()
        {
            var components = ClientReferenceManager.ClientInstance;
            ammoRepository = components.ammoRepository;

            // TODO -> For now only 
            ActiveWeaponIdentitifier = 1;

            weaponsCollection = new Dictionary<WeaponType, Weapon>(WeaponTypeEqualityComparer.Default);
            var weapons = GetComponentsInChildren<Weapon>();
            for (int i = 0; i < weapons.Length; i++)
            {
                var weapon = weapons[i];
                weapon.Initialize();
                weaponsCollection[weapon.weaponType] = weapon;
            }

            // TODO -> For now only 
            ActiveWeapon.gameObject.SetActive(true);
            nextFireCooldownTimer = new CooldownTimer(ActiveWeapon.fireRate);
        }

        // true if enemy shoot weapon 
        public bool ShootActiveWeapon()
        {
            if (ActiveWeapon.currentAmmo <= 0)
            {
                return false;
            }

            if (nextFireCooldownTimer.CanWeDoAction())
            {
                nextFireCooldownTimer.UpdateActionTime();

                //var weaponType = ActiveWeapon.weaponType;
                //var shootHook = ActiveWeapon.shootHook;

                //var shootHookTransform = shootHook.transform;
                //var activeWeaponShootType = ActiveWeapon.weaponShootType;

                //if (activeWeaponShootType == WeaponShootType.Projectile)
                //{
                //    var bulletSpeed = ActiveWeapon.bulletspeed;
                //    ammoRepository.SpawnAmmo(weaponType, shootHookTransform.position, shootHookTransform.rotation, bulletSpeed);
                //}

                //if (activeWeaponShootType == WeaponShootType.Hitscan)
                //{
                //    ActiveWeapon.lineRenderer.SetPosition(0, shootHookTransform.position);
                //    ActiveWeapon.lineRenderer.SetPosition(0, shootHookTransform.forward);
                //}

                ActiveWeapon.muzzleFlashEffect.Play();
                ActiveWeapon.currentAmmo--;
                //var arCameraTransform = vuforiaManager.arCamera.transform;

                //RaycastHit hit;
                //Debug.DrawRay(arCameraTransform.position, arCameraTransform.forward*100f, Color.green, 2f);
                //if (Physics.Raycast(arCameraTransform.position, arCameraTransform.forward, out hit, 100f))
                //{
                //    var enemyTarget = hit.transform.GetComponent<EnemyBase>();
                //    if (enemyTarget != null)
                //    {
                //        enemyTarget.TakeDamage(ActiveWeapon.damage);
                //        Debug.Log(hit.transform.name);
                //    }
                //}


                return true;
            }

            return false;
        }
    }
}