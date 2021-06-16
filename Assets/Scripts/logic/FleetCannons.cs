using System.Linq;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class FleetCannons
	{
		public FleetFormation _fleetFormation;

		public BattleConfiguration _config;
		public GenericObjectPool<Projectile> ProjectilePool;

		private float _nextShotAllowedTime = 0f;
		public FleetCannons(BattleConfiguration config, FleetFormation fleetFormation)
		{
			_fleetFormation = fleetFormation;
			_config = config;
			ProjectilePool = new GenericObjectPool<Projectile>(config.EnemyProjectilePrefab);
		}

		public void TryShoot()
		{
			bool moreProjectilesAllowed = ProjectilePool.ActiveObjectsCount < _config.MaxActiveEnemyProjectiles;
			bool shootDelaySatisfied = Time.time > _nextShotAllowedTime;
			bool fleetAlive = _fleetFormation.Members.Any(o => o.HP > 0);
			if (moreProjectilesAllowed && shootDelaySatisfied)
			{
				DoShoot();
			}
		}

		private void DoShoot()
		{
			Vector3 origin = _fleetFormation.GetRandomFrontlineShip().transform.position;
			Projectile projectile = ProjectilePool.ProvideObject(null, origin, true);
			projectile.ReturnToPool = ProjectilePool;
			projectile.Config = _config;
			projectile.Velocity = Vector3.back * _config.EnemyProjectileSpeed;
			projectile.HP = _config.EnemyProjectileDamage;
			_nextShotAllowedTime = Time.time + Random.Range(_config.MinEnemyShootDelay, _config.MaxEnemyShootDelay);
		}

		public void DestroyAllProjectiles()
		{
			ProjectilePool.DestroyAllPoolObjects();
		}
	}
}
