using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Match_Invaders.Logic
{

	[RequireComponent(typeof(Rigidbody))]
	public class FleetFormation : AbstractGenericFormation<EnemyShipBehaviour, FleetFormation>
	{
		private BattleConfiguration _config;
		private IExplosionPool _explosionProvider;
		public Vector3 Velocity
		{
			get => CachedRigidbody.velocity;
			set => CachedRigidbody.velocity = value;
		}

		public delegate void KillCountReport(int killsInOneGo, int enemiesRemaining);
		public KillCountReport OnKillsAchieved;

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

		public List<EnemyShipBehaviour> GetPlusShapedPatternSameColourMembers(EnemyShipBehaviour patternOriginShip)
		{
			List<EnemyShipBehaviour> patternMembers = new List<EnemyShipBehaviour>() { patternOriginShip };

			Vector2Int originCoords = GetCoords(patternOriginShip);
			// x y, x+1 y, x-1 y, x y+1, xy-1
			Vector2Int up = originCoords + Vector2Int.up;
			Vector2Int down = originCoords + Vector2Int.down;
			Vector2Int left = originCoords + Vector2Int.left;
			Vector2Int right = originCoords + Vector2Int.right;
			Vector2Int[] cross = { up, down, left, right };
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> adjacentShips = _coordMap.Where(coordShipPair => cross.Any(crossCoord => crossCoord == coordShipPair.Key));
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> ofThemAlive = adjacentShips.Where(o => o.Value.HP > 0);
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> ofThemMatched = ofThemAlive.Where(o => o.Value.VariantModelIndex == patternOriginShip.VariantModelIndex);
			patternMembers.AddRange(ofThemMatched.Select(o => o.Value));
			return patternMembers;
		}

		internal static FleetFormation InstantiateFleetFormationOrigin(BattleConfiguration config, Vector3 origin, string formationName)
		{
			FleetFormation formation = InstantiateFormationOrigin(origin, config.EnemyShipPrefab, formationName);
			formation._config = config;
			formation._explosionProvider = new ExplosionPool(config.ExplosionPrefab);
			return formation;
		}

		internal EnemyShipBehaviour GetRandomFrontlineShip()
		{;
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> liveShips = _coordMap.Where(o => o.Value.HP > 0);
			List<int> columnsWithLiveShips = liveShips.Select(o => o.Key.x).Distinct().ToList();
			int randomIndexForColumnList = UnityEngine.Random.Range(0, columnsWithLiveShips.Count);
			int randomActualColumnIndex = columnsWithLiveShips[randomIndexForColumnList];
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> aliveInColumn = _coordMap.Where(o => o.Key.x == randomActualColumnIndex && o.Value.HP > 0);
			int lowestRow = aliveInColumn.Min(o => o.Key.y);
			return _coordMap[new Vector2Int(randomActualColumnIndex, lowestRow)];
		}

		public Vector2Int GetCoords(EnemyShipBehaviour ship) => _coordMap.First(o => o.Value == ship).Key;

		public void OnEnable()
		{
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.useGravity = false;
			rb.freezeRotation = true;
		}

		protected override void PostProcessSpawnedObject(EnemyShipBehaviour spawn)
		{
			spawn.HP = _config.EnemyHP;
			spawn.RandomiseActiveVariantModel();
			spawn.ExplosionProvider = _explosionProvider;
			spawn.OnKilled += OneShipKilledCallback;
		}

		private void OneShipKilledCallback(EnemyShipBehaviour sender)
		{
			List<EnemyShipBehaviour> patternTargets = GetPlusShapedPatternSameColourMembers(sender);
			foreach (EnemyShipBehaviour target in patternTargets)
			{
				target.OnKilled -= OneShipKilledCallback; // unsubscribe before stashing, and also avoid chain reaction
				target.ApplyDamage(target.HP); // ensure up to five targets are destroyed, causing explosions automatically
				Pool.StashUnusedObject(target); // hide the 'body' immediately
			}
			OnKillsAchieved?.Invoke(patternTargets.Count, Members.Count(o => o.HP>0));
		}

		public void OnDestroy()
		{
			Pool.DestroyAllPoolObjects();
			_explosionProvider.DestroyAllPoolObjects();
		}
	}
}
