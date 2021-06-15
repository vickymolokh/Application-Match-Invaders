using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Match_Invaders.Logic
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class AbstractSpaceObject<TConcreteImplementation> : MonoBehaviour where TConcreteImplementation : AbstractSpaceObject<TConcreteImplementation>
	{
		public int HP { get; set; } = 1;
		public Affiliations Affiliation; // not a property for ease of assignment in inspector
		public delegate void CollisionEvent(AbstractSpaceObject<TConcreteImplementation> sender, Collision collision);
		public event CollisionEvent OnCollision;
		public delegate void EventWithConcreteSender(TConcreteImplementation sender);
		public event EventWithConcreteSender OnHPChangedDueToDamage;
		public event EventWithConcreteSender OnKilled;

		public void OnCollisionEnter(Collision collision) => OnCollision?.Invoke(this, collision);

		public Vector3 Velocity
		{
			get => GetComponent<Rigidbody>().velocity;
			set => GetComponent<Rigidbody>().velocity = value;
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
				OnKilled?.Invoke((TConcreteImplementation)this);
			}
		}

	}
}