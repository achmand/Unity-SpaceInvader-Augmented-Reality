using UnityEngine;

namespace Assets.Scripts
{
    public enum BulletType
    {
        Hitscan, // uses raycasts
        Projectile  // uses rigidbody
    }

    // TODO -> Add base clases for hitscan bullets & projectiles 
    public abstract class BulletBase : MonoBehaviour, IPoolableObject
    {
        //protected abstract BulletType bulletType { get; }

        private bool isFired;
        private float bulletSpeed;

        [HideInInspector] public bool lifetimeExpired;

        protected abstract void _Initialize();

        private void Initialize()
        {
            _Initialize();
        }

        protected abstract void _Awake();

        void Awake()
        {
            // TODO -> INITIALIZE ONLY ONCE !! when pooling 
            Initialize();
            _Awake();
        }

        protected abstract void _Start();

        void Start()
        {
            _Start();
        }

        void Update()
        {
            if (isFired)
            {
                transform.localPosition += transform.forward * Time.deltaTime * bulletSpeed;
            }
        }

        public void FireBullet(float speed)
        {
            isFired = true;
            bulletSpeed = speed;
            //rigidbody.velocity = transform.forward * bulletSpeed;
        }

        public void ResetObject()
        {
        }
    }
}