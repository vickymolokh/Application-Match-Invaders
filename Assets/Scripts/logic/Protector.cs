using UnityEngine;

namespace Match_Invaders.Logic
{
	public class Protector : AbstractSpaceObject<Protector>
	{
		public int MaxHP;
		private Vector3? _baseSize;

		private void OnEnable()
		{
			if (null == _baseSize)
			{
				_baseSize = transform.localScale;
			}
			OnHPChangedDueToDamage += AdjustSize;
		}

		private void OnDisable() => OnHPChangedDueToDamage -= AdjustSize;
		public void AdjustSize(Protector sender = null)
		{
			float xSize = _baseSize.Value.x;
			float ySize = _baseSize.Value.y;
			float hpRatio = (float)HP / MaxHP;
			float zSize = _baseSize.Value.z * hpRatio;
			transform.localScale = new Vector3(xSize, ySize, zSize);
		}
	}
}
