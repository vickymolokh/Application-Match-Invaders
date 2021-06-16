using System.Collections.Generic;
using UnityEngine;
namespace Match_Invaders.Logic
{
	public abstract class AbstractGenericFormation<TPrototype, TImplementation> : MonoBehaviour where TPrototype : MonoBehaviour where TImplementation : AbstractGenericFormation<TPrototype, TImplementation>
	{
		public bool IsFilled => _coordMap.Values.Count > 0;
		public IReadOnlyCollection<TPrototype> Members => _coordMap.Values;
		GenericObjectPool<TPrototype> Pool;
		protected readonly Dictionary<Vector2Int, TPrototype> _coordMap = new Dictionary<Vector2Int, TPrototype>();
		public static TImplementation InstantiateFormationOrigin(Vector3 originPosition, TPrototype prototype, string formationName = "Formation")
		{
			GameObject obj = new GameObject(formationName);
			obj.transform.position = originPosition;
			TImplementation formation = obj.AddComponent<TImplementation>();
			formation.Pool = new GenericObjectPool<TPrototype>(prototype);
			return formation;
		}

		public void FillFormation(float interval, Vector2Int gridSize)
		{
			ClearFormation();
			for (int xGridCoord = 0; xGridCoord < gridSize.x; xGridCoord++)
			{
				for (int yGridCoord = 0; yGridCoord < gridSize.y; yGridCoord++)
				{
					Vector3 pos = transform.position + new Vector3(xGridCoord * interval, yGridCoord * interval);
					TPrototype spawn = Pool.ProvideObject(transform, pos, true).GetComponent<TPrototype>();
					PostProcessSpawnedObject(spawn);
					_coordMap.Add(new Vector2Int(xGridCoord, yGridCoord), spawn);
				}
			}
		}

		public TPrototype this[Vector2Int coordinate] => _coordMap[coordinate];
		public void ClearFormation()
		{
			foreach (TPrototype oldSpawn in _coordMap.Values)
			{
				Pool.StashUnusedObject(oldSpawn);
			}
			_coordMap.Clear();
		}

		protected virtual void PostProcessSpawnedObject(TPrototype spawn)
		{

		}

	}
}