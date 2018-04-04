using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class BulletPool : BaseObjectPool<BulletBase>
    {
    }

    public sealed class GamePoolManager : MonoBehaviour
    {
        [Header("References")]
        public GameObject rootSpawnPoolsGameObject;

        [Header("Spawn Pools")]
        public BulletPool bulletObjectPool;

        void Awake()
        {
            var rootTransform = rootSpawnPoolsGameObject.transform;
            bulletObjectPool.objectPooler.Initialize(bulletObjectPool.poolOptions, rootTransform);
        }

        //private ObjectPooler<BulletBase> bulletPool; 
    }
}
