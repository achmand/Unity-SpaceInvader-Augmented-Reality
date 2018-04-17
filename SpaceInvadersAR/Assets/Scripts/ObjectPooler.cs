using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BaseObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        public ObjectPooler<T> objectPooler = new ObjectPooler<T>();
        public PoolOptions poolOptions;
    }

    [Serializable]
    public class PoolOptions
    {
        public GameObject poolableObject;
        public int initialSize;
    }

    public class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject
    {
        private Transform root;
        public Queue<T> objectPool;
        private GameObject poolableObject;

        public void Initialize(PoolOptions poolOptions, Transform rootTransform)
        {
            root = rootTransform;
            poolableObject = poolOptions.poolableObject;
            objectPool = new Queue<T>(poolOptions.initialSize);

            for (int i = 0; i < poolOptions.initialSize; i++)
            {
                var pooledObject = Instantiate(poolableObject).GetComponent<T>();
                pooledObject.gameObject.transform.parent = root;
                pooledObject.gameObject.SetActive(false);
                objectPool.Enqueue(pooledObject);
            }
        }

        public T SpawnFromPool(Vector3 position, Quaternion rotation, bool useLocalTransform = false, Transform parent = null, bool worldPositionStays = true)
        {
            var pooledObject = objectPool.Count <= 0 ? Instantiate(poolableObject).GetComponent<T>() : objectPool.Dequeue();
            if (!useLocalTransform)
            {
                pooledObject.transform.position = position;
                pooledObject.transform.rotation = rotation;
            }
            else
            {
                pooledObject.transform.localPosition = position;
                pooledObject.transform.localRotation = rotation;
            }
            pooledObject.transform.SetParent(parent ?? root, worldPositionStays);
            pooledObject.gameObject.SetActive(true);

            return pooledObject;
        }

        public void DespawnObject(T despawnObject)
        {
            despawnObject.transform.SetParent(root, false);
            despawnObject.ResetObject();
            despawnObject.gameObject.SetActive(false);

            objectPool.Enqueue(despawnObject);
        }
    }
}
