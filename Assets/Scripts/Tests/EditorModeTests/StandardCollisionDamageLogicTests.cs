using NUnit.Framework;

namespace Match_Invaders.Logic.Tests
{
	public class StandardCollisionDamageLogicTests
	{
		[Test]
		public void DifferentAffiliationMutualAnnihilationTest()
		{
			IDamageable mockPlayerShip = new MockDamageable(Affiliations.PlayerShip);
			IDamageable mockPlayerProjectile = new MockDamageable(Affiliations.PlayerProjectile);
			IDamageable mockProtector = new MockDamageable(Affiliations.ProtectorNeutral);
			IDamageable mockEnemyShip = new MockDamageable(Affiliations.EnemyShip);
			IDamageable mockEnemyProjectile = new MockDamageable(Affiliations.EnemyProjectile);

			// same team - player, should be false
			Assert.IsFalse(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockPlayerProjectile, mockPlayerShip));
			// same team - enemy, should be false
			Assert.IsFalse(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockEnemyProjectile, mockEnemyShip));
			// protector - damaged by projectiles, but not by ships
			Assert.IsTrue(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockProtector, mockPlayerProjectile));
			Assert.IsTrue(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockProtector, mockEnemyProjectile));
			Assert.IsFalse(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockProtector, mockPlayerShip));
			Assert.IsFalse(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockProtector, mockEnemyShip));
			// cross-team shooting (bullet vs. either ship or bullet) - should be true
			Assert.IsTrue(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockPlayerShip, mockEnemyProjectile));
			Assert.IsTrue(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockEnemyShip, mockPlayerProjectile));
			Assert.IsTrue(StandardCollisionDamageLogic.DoTheseTwoAnnihilateEachOther(mockEnemyProjectile, mockPlayerProjectile));
		}

		private class MockDamageable : IDamageable
		{
			public int HP { get; set; }

			public MockDamageable(Affiliations affiliation) => Affiliation = affiliation;
			public void ApplyDamage(int damage) => throw new System.NotImplementedException();
			public Affiliations GetAffiliation() => Affiliation;
			public Affiliations Affiliation = Affiliations.Undefined;
		}
	}
}