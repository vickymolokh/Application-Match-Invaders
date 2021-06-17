using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class Battlefield // note: it makes things more readable to have builder and destroyer in the same place
	{
		private readonly BattleConfiguration _config;
		private readonly FleetBehaviour _fleetBehaviour;
		private readonly ProtectorFormation _protectorFormation;
		private readonly IKillReportReceiver _killReportReceiver;
		private readonly IPlayerShipDamageReportReceiver _playerDamageReportReceiver;
		private readonly IBattlefieldClearedReceiver _battlefieldClearedReportReceiver;
		private readonly PlayerShipBehaviour _playerShip;

		public Battlefield(BattleConfiguration config, ICombinedBattlefieldReportReceiver combinedReceiver) : this(config, combinedReceiver, combinedReceiver, combinedReceiver)
		{ } // I initially considered having separate receivers, but in retrospect a different approach seems more practical.


		public Battlefield(BattleConfiguration config, IKillReportReceiver killReceiver, IPlayerShipDamageReportReceiver playerDamageReceiver, IBattlefieldClearedReceiver battlefieldClearReceiver)
		{
			_config = config;
			_fleetBehaviour = FleetBehaviour.CreateFleet(_config);
			_protectorFormation = ProtectorFormation.CreateProtectorFormation(_config);
			_playerShip = FabricatePlayerShip(_config);
			_killReportReceiver = killReceiver;
			_playerDamageReportReceiver = playerDamageReceiver;
			_battlefieldClearedReportReceiver = battlefieldClearReceiver;
			SubscribeReporters();
		}

		private void SubscribeReporters()
		{
			if (null != _killReportReceiver)
			{
				_fleetBehaviour.OnKillsOccurred += _killReportReceiver.KillsOccurredCallbackReceiver;
			}
			if (null != _battlefieldClearedReportReceiver)
			{
				_fleetBehaviour.OnBattlefieldCleared += _battlefieldClearedReportReceiver.BattlefieldClearedCallbackReceiver;
			}
			if (null != _playerDamageReportReceiver)
			{
				_playerShip.OnHPChangedDueToDamage += _playerDamageReportReceiver.PlayerDamagedCallbackReceiver;
			}
		}

		private PlayerShipBehaviour FabricatePlayerShip(BattleConfiguration config)
		{
			float lowerBattlefieldEdge = -config.BattlefieldHeight / 2f;
			float playerZpos = lowerBattlefieldEdge + config.FleetFormationInterval; // ensure a neat grid where all major positions are divisible by FleetInterval and match horizontal movement rows
			Vector3 playerPos = Vector3.forward*playerZpos;
			GameObject go = Object.Instantiate(config.PlayerShipPrefab.gameObject, playerPos, config.PlayerShipPrefab.transform.rotation);
			PlayerShipBehaviour playerShipBhv = go.GetComponent<PlayerShipBehaviour>();
			IPlayerShipInput inputSys = new StandardPlayerShipInput();
			IPlayerCannon cannon = new StandardPlayerShipCannon(config, playerShipBhv.transform);
			playerShipBhv.ConfigureShip(config, inputSys, cannon);
			return playerShipBhv;
		}

		public void DestroyBattlefield()
		{
			DestroyBattleElements();
			UnsubscribeReporters();
		}

		private void DestroyBattleElements()
		{
			if (null != _fleetBehaviour)
			{
				Object.Destroy(_fleetBehaviour.gameObject);
			}
			_protectorFormation.DestroyAllPoolObjects();
			if (null != _playerShip)
			{
				Object.Destroy(_playerShip.gameObject);
			}
		}

		private void UnsubscribeReporters()
		{
			if (null != _killReportReceiver)
			{
				_fleetBehaviour.OnKillsOccurred -= _killReportReceiver.KillsOccurredCallbackReceiver;
			}
			if (null != _battlefieldClearedReportReceiver)
			{
				_fleetBehaviour.OnBattlefieldCleared -= _battlefieldClearedReportReceiver.BattlefieldClearedCallbackReceiver;
			}
			if (null != _playerDamageReportReceiver)
			{
				_playerShip.OnHPChangedDueToDamage -= _playerDamageReportReceiver.PlayerDamagedCallbackReceiver;
			}
		}
	}
}
