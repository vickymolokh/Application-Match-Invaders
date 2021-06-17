using UnityEngine;
namespace Match_Invaders.Logic
{
	public class Projectile : AbstractSpaceObject<Projectile>
	{
		public BattleConfiguration Config;
		public GenericObjectPool<Projectile> TargetPoolToReturnToAutomatically;

		public void Update()
		{
			bool outsideBoundsX = Mathf.Abs(transform.position.x) > Mathf.Abs(Config.BattlefieldWidth/2f);
			bool outsideBoundsZ = Mathf.Abs(transform.position.z) > Mathf.Abs(Config.BattlefieldHeight/2f);
			if (outsideBoundsX || outsideBoundsZ)
			{
				ReturnToParentPool(this);
			}
		}

		public void SetPoolToReturnToAutomatically(GenericObjectPool<Projectile> pool)
		{
			TargetPoolToReturnToAutomatically = pool;
			OnKilled += ReturnToParentPool;
		}
		public void ReturnToParentPool(Projectile projectile)
		{
			if (null != TargetPoolToReturnToAutomatically)
			{
				OnKilled -= ReturnToParentPool;
				TargetPoolToReturnToAutomatically.StashUnusedObject(this);
				TargetPoolToReturnToAutomatically = null; 
			}
		}
		public Vector3 Velocity
		{
			get => CachedRigidbody.velocity;
			set => CachedRigidbody.velocity = value;
		}

		private Rigidbody _cachedRigidbody;
		private Rigidbody CachedRigidbody // there seems to be disagreement about whether catchign or non-caching GetComponents is faster, but for the purposes of this test I will implement it
		{
			get
			{
				if (null == _cachedRigidbody)
				{
					_cachedRigidbody = GetComponent<Rigidbody>();
				}
				return _cachedRigidbody;
			}
		}

	}
}