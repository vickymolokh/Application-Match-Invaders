using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;


namespace Match_Invaders.Logic
{
	public class StandardCollisionDamageLogic : ICollisionDamageLogic
	{
		public void HandleCollisionEvent<T>(AbstractSpaceObject<T> sender, Collider otherCollider) where T : AbstractSpaceObject<T>
		{
			IDamageable obj1 = sender;
			IDamageable obj2 = otherCollider.transform.GetComponent<IDamageable>();

			if (obj1.GetAffiliation() == obj2.GetAffiliation())
			{
				return; // no colliding on the same side; maybe other game variants will want to implement it later
			}
			// we might want to implement non-mutual annihilations here later if the client requests an expansion of interaction types.

			if (DoTheseTwoAnnihilateEachOther(obj1, obj2)
				)
			{
				ApplyMutualDamage(obj1, obj2);
			}
		}

		public static bool DoTheseTwoAnnihilateEachOther(IDamageable obj1, IDamageable obj2) => AnnihilatingPairs.Any
						(o => o.Contains(obj1.GetAffiliation()) && o.Contains(obj2.GetAffiliation()));

		private void ApplyMutualDamage(IDamageable obj1, IDamageable obj2)
		{
			if (obj1.HP <= 0 || obj2.HP <= 0)
			{
				return; // collisions with 'dead' objects are considered harmless
			}
			int damage = Mathf.Min(obj1.HP, obj2.HP);
			obj1.ApplyDamage(damage);
			obj2.ApplyDamage(damage);
		}

		// a ruleset listing which pairs of affiliations lead to mutual annihilation (damage)
		static readonly List<List<Affiliations>> AnnihilatingPairs = new List<List<Affiliations>>()
			{
			// bullets of different alignments annihilate each other
			new List<Affiliations>{ Affiliations.PlayerProjectile, Affiliations.EnemyProjectile},
			// player bullets damage protectors and enemies
			new List<Affiliations>{ Affiliations.PlayerProjectile, Affiliations.ProtectorNeutral},
			new List<Affiliations>{ Affiliations.PlayerProjectile, Affiliations.EnemyShip},
			// enemy bullets damage protectors and players
			new List<Affiliations>{ Affiliations.EnemyProjectile, Affiliations.PlayerShip},
			new List<Affiliations>{ Affiliations.EnemyProjectile, Affiliations.ProtectorNeutral},
			// player-enemy ship collisions should also lead to mutual damage
			new List<Affiliations>{ Affiliations.EnemyShip, Affiliations.PlayerShip}
			};
	}
}
