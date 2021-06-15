using UnityEngine;

namespace Match_Invaders.Logic
{
	public class ExplosionPool : GenericObjectPool<Explosion>
	{

		public ExplosionPool(Explosion prototype) : base(prototype) { }

		public void ExplodeHere(Vector3 position)
		{
			Explosion explosion = ProvideObject(null, position, true);
			explosion.OnExpired += RecycleExplosion;
			explosion.BeginExploding();
		}
		private void RecycleExplosion(Explosion sender)
		{
			StashUnusedObject(sender);
			sender.OnExpired -= RecycleExplosion;
		}
	}
}
