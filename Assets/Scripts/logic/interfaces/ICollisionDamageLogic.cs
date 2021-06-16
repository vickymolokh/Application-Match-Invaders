using UnityEngine;

namespace Match_Invaders.Logic
{
	public interface ICollisionDamageLogic
	{
		public void HandleCollisionEvent<T>(AbstractSpaceObject<T> sender, Collider collision) where T : AbstractSpaceObject<T>;
	}
}
