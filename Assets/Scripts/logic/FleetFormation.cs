using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Match_Invaders.Logic
{
	class FleetFormation : AbstractGenericFormation<EnemyShipBehaviour, FleetFormation>
	{
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

		public Vector2Int GetCoords(EnemyShipBehaviour ship) => _coordMap.First(o => o.Value == ship).Key;

	}
}
