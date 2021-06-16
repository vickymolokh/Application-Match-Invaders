using UnityEngine;

namespace Match_Invaders.Logic
{
	public class ExplosionPool : GenericObjectPool<Explosion>, IExplosionPool
	{
		public ExplosionPool(Explosion prototype) : base(prototype) { }

		public Explosion ExplodeHere(Vector3 position)
		{
			Explosion explosion = ProvideObject(null, position, true);
			explosion.OnExpired += RecycleExplosion;
			explosion.BeginExploding();
			return explosion;
		}
		private void RecycleExplosion(Explosion sender)
		{
			StashUnusedObject(sender);
			sender.OnExpired -= RecycleExplosion;
		}

	}
}
