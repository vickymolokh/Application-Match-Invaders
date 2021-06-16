using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class FleetCannons
	{
		public FleetFormation _fleet;

		public BattleConfiguration _config;
		public GenericObjectPool<Projectile> ProjectilePool;

		private float _nextShotAllowedTime = 0f;
		public FleetCannons(BattleConfiguration config, FleetFormation fleetFormation)
		{
			_fleet = fleetFormation;
			_config = config;
		}

		public void TryShoot()
		{
			bool moreProjectilesAllowed = ProjectilePool.ActiveObjectsCount < _config.MaxActiveEnemyProjectiles;
			bool shootDelaySatisfied = Time.time > _nextShotAllowedTime;
			bool fleetAlive = _fleet.Members.Any(o => o.HP > 0);
			if (moreProjectilesAllowed && shootDelaySatisfied)
			{
				DoShoot();
			}
		}

		private void DoShoot()
		{
			Vector3 origin = _fleet.GetRandomFrontlineShip().transform.position;
			ProjectilePool.ProvideObject(null, origin, true).Velocity = Vector3.down * _config.EnemyProjectileSpeed;
			_nextShotAllowedTime = Time.time + UnityEngine.Random.Range(_config.MinEnemyShootDelay, _config.MaxEnemyShootDelay);
		}

	}
}
