namespace Assets.Scripts
{
    public sealed class SimpleProjectile : Projectile
    {
        public override float timeBeforeDespawnSeconds
        {
            get { return 3f; }
        }

        public override ProjectileType projectileType
        {
            get { return ProjectileType.SimpleProjectile; }
        }

        protected override float projectileForce
        {
            get { return 40f; }
        }

        public override int projectileDamage
        {
            get { return 25; }
        }
    }
}
