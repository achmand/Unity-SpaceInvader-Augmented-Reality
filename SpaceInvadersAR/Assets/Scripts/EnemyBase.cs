using UnityEngine;

namespace Assets.Scripts
{
    public enum EnemyType
    {
        SimpleDroid = 0,
        WarriorDroid = 1,
        HeavyDroid = 2,
        BomberDroid = 3
    }

    public abstract class EnemyBase : MonoBehaviour, IPoolableObject
    {
        [HideInInspector] public int enemyId;
        protected abstract float Health { get; }
        public abstract EnemyType EnemyType { get; }
        public bool isDeployedToTarget;

        //public PlayerOwner targetPlayer; 

        private float enemyHealth { get; set; }

        void Awake()
        {
            enemyHealth = Health;
        }

        void Update()
        {
            //if (PhotonNetwork.isMasterClient)
            //{
            //    if (targetPlayer != null)
            //    {
            //        transform.LookAt(2*transform.position - targetPlayer.transform.position);
            //    }
            //}
        }

        public abstract void _TakeDamage();

        // returns true if this enemy dies
        public bool TakeDamage(float damage)
        {
            _TakeDamage();

            enemyHealth -= damage;
            if (enemyHealth <= 0f)
            {
                EnemyDied();
                return true;
            }

            return false;
        }

        public void EnemyDied()
        {
        }

        public void ResetObject()
        {
            enemyId = 0;
            isDeployedToTarget = false;
            enemyHealth = Health;
            //targetPlayer = null;  
        }
    }
}