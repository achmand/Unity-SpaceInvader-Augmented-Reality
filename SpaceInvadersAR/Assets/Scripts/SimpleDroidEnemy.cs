namespace Assets.Scripts
{
    public sealed class SimpleDroidEnemy : EnemyBase
    {
        protected override float Health
        {
            get { return 20f; }
        }

        public override EnemyType EnemyType
        {
            get { return EnemyType.SimpleDroid; }
        }

        public override void _TakeDamage()
        {
        }
    }
}
