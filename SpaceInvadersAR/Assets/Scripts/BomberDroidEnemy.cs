namespace Assets.Scripts
{
    public sealed class BomberDroidEnemy : EnemyBase
    {
        protected override float Health
        {
            get { return 100f; }
        }

        public override EnemyType EnemyType
        {
            get { return EnemyType.BomberDroid; }
        }

        public override void _TakeDamage()
        {
        }
    }
}
