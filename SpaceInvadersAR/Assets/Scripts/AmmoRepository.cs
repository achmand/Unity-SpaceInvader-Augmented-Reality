using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class AmmoRepository : MonoBehaviour
    {
        private Dictionary<WeaponType, AmmoRepositoryItem> collection;
        //private ImageTargetBehaviour backgroundImageTargetBehaviour;
        private BulletPool bulletPool;

        void Awake()
        {
            var components = ClientReferenceManager.ClientInstance;
            //var vuforiaManager = components.vuforiaManager;
            //backgroundImageTargetBehaviour = vuforiaManager.backgroundImageTargetBehaviour;
            bulletPool = components.gamePoolManager.bulletObjectPool;

            collection = new Dictionary<WeaponType, AmmoRepositoryItem>(WeaponTypeEqualityComparer.Default);

            var items = GetComponentsInChildren<AmmoRepositoryItem>();
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                collection[item.weaponType] = item;
            }
        }

        public void SpawnAmmo(WeaponType weaponType, Vector3 position, Quaternion rotation, float bulletSpeed)
        {
            if (!collection.ContainsKey(weaponType))
            {
                Debug.LogWarning("No ammo found for " + weaponType, gameObject);
                return;
            }

            //var ammoType = collection[weaponType];
            ////var bullet = Instantiate(item, position, rotation);

            var pooledBullet = bulletPool.objectPooler.SpawnFromPool(position, rotation);
            pooledBullet.FireBullet(bulletSpeed);
            StartCoroutine(DespawnAmmo(pooledBullet)); // TODO -> Remove coroutines they cause GC alloc

            //var bulletBase = pooledBullet.GetComponent<BulletBase>();
            //bulletBase.FireBullet(bulletSpeed);
            //var bullet = Instantiate(item, position, rotation);
            //bullet.transform.parent = backgroundImageTargetBehaviour.transform;
        }

        //TODO -> Find a better way to spawn and despawn, maybe a projectile manager ???
        public IEnumerator DespawnAmmo(BulletBase bulletBase)
        {
            yield return new WaitForSeconds(2f);
            bulletPool.objectPooler.DespawnObject(bulletBase);
        }
    }
}