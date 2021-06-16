using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match_Invaders.Logic
{
	class FleetBehaviour : MonoBehaviour
	{
		private FleetFormation _formation;
		private FleetCannons _cannons;
		private FleetMover _mover;
		public delegate void KillsOccurredDelegate(int killsInOneGo);
		public KillsOccurredDelegate OnKillsOccurred;
		public delegate void BattlefieldClearedDelegate();
		public BattlefieldClearedDelegate OnBattlefieldCleared;


		public BattleConfiguration _config;


		public FleetBehaviour Instantiate(BattleConfiguration config)
		{
			FleetBehaviour fleet = new GameObject("FleetBehaviour").AddComponent<FleetBehaviour>();
			fleet._config = config;
			return fleet;
		}

		public void AssembleFleet(BattleConfiguration config)
		{
			_formation = AssembleFleetFormation(config);
			_formation.OnKillsAchieved += KillCountCallback;
			_cannons = new FleetCannons(config, _formation);
			_mover = new FleetMover(config, _formation);
		}

		public void DestroyFleet()
		{
			_formation.DestroyAllPoolObjects();
			_formation.OnKillsAchieved -= KillCountCallback;
			_cannons.DestroyAllProjectiles();
		}

		private void KillCountCallback(int killCount, int enemiesRemaining)
		{
			OnKillsOccurred?.Invoke(killCount);
			if (enemiesRemaining <= 0)
			{
				OnBattlefieldCleared?.Invoke();
			}
		}

		private static FleetFormation AssembleFleetFormation(BattleConfiguration config)
		{
			float fleetRowWidth = (config.FleetColumns - 1) * config.FleetFormationInterval;
			float originX = -fleetRowWidth / 2f;
			float fleetColumnHeight = (config.FleetRows - 1) * config.FleetFormationInterval;
			float originZ = (config.BattlefieldHeight / 2f) - fleetColumnHeight;
			Vector3 origin = new Vector3(originX, 0, originZ);
			FleetFormation formation = FleetFormation.InstantiateFleetFormationOrigin(config, origin, "FleetFormation");
			Vector2Int gridSize = new Vector2Int(config.FleetColumns, config.FleetRows);
			formation.FillFormation(config.FleetFormationInterval, gridSize);
			return formation;
		}

		public void Update()
		{
			_cannons?.TryShoot();
			_mover?.MaintainMovement();
		}
	}
}
