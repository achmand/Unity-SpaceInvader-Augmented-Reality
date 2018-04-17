
namespace Assets.Scripts
{
    public sealed class WarriorDroidEnemy : EnemyBase
    {
        protected override float Health
        {
            get { return 40f; }
        }

        public override EnemyType EnemyType
        {
            get { return EnemyType.WarriorDroid; }
        }

        public override void _TakeDamage()
        {
        }
    }
}
