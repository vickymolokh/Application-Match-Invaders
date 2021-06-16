using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Match_Invaders.Logic
{

	[RequireComponent(typeof(Rigidbody))]
	public class FleetFormation : AbstractGenericFormation<EnemyShipBehaviour, FleetFormation>
	{
		public Vector3 Velocity { get; internal set; }

		public List<EnemyShipBehaviour> GetPlusShapedPatternMembers(EnemyShipBehaviour originShip)
		{
			List<EnemyShipBehaviour> patternMembers = new List<EnemyShipBehaviour>();

			Vector2Int originCoords = GetCoords(originShip);
			// x y, x+1 y, x-1 y, x y+1, xy-1
			Vector2Int up = originCoords + Vector2Int.up;
			Vector2Int down = originCoords + Vector2Int.down;
			Vector2Int left = originCoords + Vector2Int.left;
			Vector2Int right = originCoords + Vector2Int.right;
			Vector2Int[] cross = { up, down, left, right };
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> adjacentShips = _coordMap.Where(coordShipPair => cross.Any(crossCoord => crossCoord == coordShipPair.Key));
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> ofThemAlive = adjacentShips.Where(o => o.Value.HP > 0);
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> ofThemMatched = ofThemAlive.Where(o => o.Value.VariantModelIndex == originShip.VariantModelIndex);
			patternMembers.AddRange(ofThemMatched.Select(o => o.Value));
			return patternMembers;
		}

		internal EnemyShipBehaviour GetRandomFrontlineShip()
		{
			List<int> columnsWithLiveShips = _coordMap.Where(o => o.Value.HP > 0).Select(o => o.Key.x).ToList();
			int randomColumn = Random.Range(0, columnsWithLiveShips.Count);
			IEnumerable<KeyValuePair<Vector2Int, EnemyShipBehaviour>> aliveInColumn = _coordMap.Where(o => o.Key.x == randomColumn && o.Value.HP > 0);
			int lowestRow = aliveInColumn.Min(o => o.Key.y);
			return _coordMap[new Vector2Int(randomColumn, lowestRow)];
		}

		public Vector2Int GetCoords(EnemyShipBehaviour ship) => _coordMap.First(o => o.Value == ship).Key;

		public void OnEnable()
		{
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = true;
			rb.useGravity = false;
			rb.freezeRotation = true;
		}
	}
}
