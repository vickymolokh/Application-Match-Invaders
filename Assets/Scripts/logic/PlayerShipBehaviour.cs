using UnityEngine;

namespace Match_Invaders.Logic
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerShipBehaviour : AbstractSpaceObject<PlayerShipBehaviour>
	{
		private BattleConfiguration _config;
		private IPlayerShipInput _inputSystem;
		private IPlayerCannon _playerShipCannon;

		private Rigidbody _cachedRigidbody;
		private Rigidbody CachedRigidbody 
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

		public void ConfigureShip(BattleConfiguration config, IPlayerShipInput inputSys, IPlayerCannon cannonWeapon)
		{
			_config = config;
			_inputSystem = inputSys;
			_playerShipCannon = cannonWeapon;
		}

		public void OnDestroy() => _playerShipCannon?.DestroyAllPoolObjects();

		public void Update()
		{
			if (null != _inputSystem)
			{
				CachedRigidbody.velocity = _config.PlayerShipSpeed * _inputSystem.XAxis * Vector3.right;
				if (_inputSystem.IsTriggerButtonHeld)
				{
					_playerShipCannon?.TryShoot();
				}
			}
			else
			{
				CachedRigidbody.velocity = Vector3.zero;
			}
		}
	}

}
