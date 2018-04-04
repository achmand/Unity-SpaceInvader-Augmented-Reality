using UnityEngine;

namespace Assets.Scripts
{
    public enum EnemyType
    {
        SimpleDroid,     
    }

    public abstract class EnemyBase : MonoBehaviour
    {
        [HideInInspector] public float health = 20f;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0f)
            {
                EnemyDied();
            }
        }

        public void EnemyDied()
        {
            Destroy(gameObject);
        }
    }
}