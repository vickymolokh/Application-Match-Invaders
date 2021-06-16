using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class EnemyShipBehaviour : AbstractSpaceObject<EnemyShipBehaviour>
	{
		[SerializeField]
		private List<GameObject> _variantModels = new List<GameObject>();

		public void RandomiseActiveVariantModel()
		{
			if (_variantModels.Count <= 0)
			{
				return;
			}
			int randomIndex = Random.Range(0, _variantModels.Count);
			SetActiveVariantModel(randomIndex);
		}

		public int VariantModelIndex { get; private set; } = -1;
		public void SetActiveVariantModel(int modelIndexToSet)
		{
			if (modelIndexToSet<0 || modelIndexToSet>= _variantModels.Count)
			{
				throw new System.ArgumentOutOfRangeException($"index {modelIndexToSet} is outside range 0..{_variantModels.Count-1}");
			}
			for (int i = 0; i < _variantModels.Count; i++)
			{
				GameObject model = _variantModels[i];
				model.SetActive(modelIndexToSet == i);
			}
			VariantModelIndex = modelIndexToSet;
		}
	}
}