using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match_Invaders.Logic
{
	public class EnemyShipBehaviour : AbstractSpaceObject<EnemyShipBehaviour>
	{
		[SerializeField]
		private readonly List<GameObject> _variantModels = new List<GameObject>();

		public void RandomiseActiveVariantModel()
		{
			int randomIndex = Random.Range(0, _variantModels.Count);
			SetActiveVariantModel(randomIndex);
		}

		public void SetActiveVariantModel(int index)
		{
			if (index<=0 || index>= _variantModels.Count)
			{
				throw new System.ArgumentOutOfRangeException($"index {index} is outside range 0..{_variantModels.Count}");
			}
			for (int i = 0; i < _variantModels.Count; i++)
			{
				GameObject model = _variantModels[i];
				model.SetActive(index == i);
			}
		}
	}
}