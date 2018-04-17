namespace Assets.Scripts
{
    public sealed class HeavyDroidEnemy : EnemyBase
    {
        protected override float Health
        {
            get { return 70f; }
        }

        public override EnemyType EnemyType
        {
            get { return EnemyType.HeavyDroid; }
        }

        // TODO -> Add shake effect on damage !!
        public override void _TakeDamage()
        {

        }
    }
}
