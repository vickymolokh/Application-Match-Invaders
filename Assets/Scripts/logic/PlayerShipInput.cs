using UnityEngine;

namespace Match_Invaders.Logic
{
	public class PlayerShipInput : MonoBehaviour, IPlayerShipInput
	{
		public float XAxis
		{
			get
			{
				float x = 0;
				if (
					Input.GetKey(KeyCode.A) ||
					Input.GetKey(KeyCode.LeftArrow) ||
					Input.GetKey(KeyCode.Keypad4)
					)
				{
					x -= 1; // yes, decrease, not assign, to make mutually cancelling keys
				}
				if (
					Input.GetKey(KeyCode.D) ||
					Input.GetKey(KeyCode.RightArrow) ||
					Input.GetKey(KeyCode.Keypad6)
					)
				{
					x += 1;
				}
				return x;
			}
		}

		public bool IsTriggerButtonHeld => Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightControl);
	}
}
