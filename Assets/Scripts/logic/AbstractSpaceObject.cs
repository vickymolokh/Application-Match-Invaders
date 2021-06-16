using UnityEngine;

namespace Match_Invaders.Logic
{
	public abstract class AbstractSpaceObject<TConcreteImplementation> : MonoBehaviour, IDamageable where TConcreteImplementation : AbstractSpaceObject<TConcreteImplementation>
	{
		public int HP { get; set; } = 1;
		public Affiliations Affiliation; // not a property for ease of assignment in inspector
		public Affiliations GetAffiliation() => Affiliation; // this is for the interface
		public delegate void EventWithConcreteSender(TConcreteImplementation sender);
		public event EventWithConcreteSender OnHPChangedDueToDamage;
		public event EventWithConcreteSender OnKilled;
		public ICollisionDamageLogic CollisionDamageLogic = new StandardCollisionDamageLogic(); // default; can be changed if we ever need objects with different collision logic.
		public IExplosionPool ExplosionProvider; // can be added as needed externally

		public void OnTriggerEnter(Collider otherCollider)
		{
			if (null != CollisionDamageLogic)
			{
				CollisionDamageLogic.HandleCollisionEvent(this, otherCollider);
			}
		}
		public void ApplyDamage(int damage)
		{
			if (damage <= 0)
			{
				return;
			}
			HP -= damage;
			OnHPChangedDueToDamage?.Invoke((TConcreteImplementation)this);
			if (HP <= 0)
			{
				ExplosionProvider?.ExplodeHere(transform.position);
				OnKilled?.Invoke((TConcreteImplementation)this);
			}
		}

	}
}