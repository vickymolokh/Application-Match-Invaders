using System;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class StandardPlayerShipCannon : GenericObjectPool<Projectile>, IPlayerCannon
	{
		private Transform _projectileOrigin;
		private BattleConfiguration _config;
		private float _nextPossibleShotTime;
		public StandardPlayerShipCannon(BattleConfiguration config, Transform projectileOriginatesFrom) : base(config.PlayerProjectilePrefab)
		{
			_projectileOrigin = projectileOriginatesFrom;
			_config = config;
		}

		public void TryShoot()
		{
			bool maxProjectilesSatisfied = ActiveObjectsCount > _config.MaxActivePlayerProjectiles;
			bool cooldownSatisfied = Time.time >= _nextPossibleShotTime;
			if (maxProjectilesSatisfied && cooldownSatisfied)
			{
				DoShoot();
			}
		}

		private void DoShoot()
		{
			Vector3 originPoint = _projectileOrigin.transform.position;
			Projectile projectile = ProvideObject(null, originPoint, true);
			projectile.Velocity = Vector3.forward * _config.PlayerProjectileSpeed;
			projectile.ReturnToPool = this;
			projectile.Config = _config;
			projectile.HP = _config.PlayerProjectileDamage;
			_nextPossibleShotTime = Time.time + _config.MinPlayerShootDelay;
		}
	}
}