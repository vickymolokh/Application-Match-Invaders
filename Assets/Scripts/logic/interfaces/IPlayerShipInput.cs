using UnityEngine;
namespace Match_Invaders.Logic
{
	public interface IPlayerShipInput
	{
		float XAxis { get; }
		bool IsTriggerButtonHeld { get; }
	}
}
