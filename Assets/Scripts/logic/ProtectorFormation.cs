using UnityEngine;
namespace Match_Invaders.Logic
{
	public class ProtectorFormation : AbstractGenericFormation<Protector, ProtectorFormation>
	{

		int _hitPointsToSet;

		public static ProtectorFormation CreateProtectorFormation(BattleConfiguration config)
		{
			ProtectorFormation formation = InstantiateFormationOrigin(Vector3.zero, config.ProtectorPrefab, "ProtectorFormation");
			//GameObject go = new GameObject("Protector Formation");
			//ProtectorFormation formation = go.AddComponent<ProtectorFormation>();
			formation.FillBasedOnConfig(config);
			return formation;
		}

		public void FillBasedOnConfig(BattleConfiguration _config) => FillBasedOnConfigData(_config.BattlefieldWidth, _config.BattlefieldHeight, _config.FleetFormationInterval, _config.ProtectorCount, _config.ProtectorHP);
		public void FillBasedOnConfigData(float boundsWidth, float boundsHeight, float mainGridInterval, int count, int hitPoints)
		{
			ClearFormation();
			_hitPointsToSet = hitPoints;
			float protectorIntervalHorizontal = boundsWidth / (count + 1); // no protectors on playzone edges
			float formationOriginX = (-boundsWidth / 2f) + protectorIntervalHorizontal;

			float formationOriginZ = (-boundsHeight / 2f) + (2*mainGridInterval);

			transform.position = new Vector3(formationOriginX, 0, formationOriginZ);
			Vector2Int countXoneGrid = new Vector2Int(count, 1);
			FillFormation(protectorIntervalHorizontal, countXoneGrid);
		}

		protected override void PostProcessSpawnedObject(Protector spawn)
		{
			base.PostProcessSpawnedObject(spawn);
			spawn.MaxHP = _hitPointsToSet;
			spawn.HP = _hitPointsToSet;
			spawn.AdjustSize();
		}
	}
}