using NUnit.Framework;
using UnityEngine;
namespace Match_Invaders.Logic.Tests
{
	public class ProtectorTests
	{
		public Protector MockProtectorInstance;
		private const int InitialHP = 5;
		[SetUp]
		public void Setup()
		{
			MockProtectorInstance = MakeMockProtector();
			ResetProtectorToInitialState(MockProtectorInstance);
		}

		private Protector MakeMockProtector()
		{
			GameObject go = new GameObject("Mock Protector");
			return go.AddComponent<Protector>();
		}

		private void ResetProtectorToInitialState(Protector protector)
		{
			protector.MaxHP = InitialHP;
			protector.HP = InitialHP;
			protector.gameObject.SetActive(true);
			protector.enabled = true;
		}

		[Test]
		public void CheckDamageChangesScale()
		{
			ResetProtectorToInitialState(MockProtectorInstance);
			Vector3 initialScale = MockProtectorInstance.transform.localScale;
			MockProtectorInstance.ApplyDamage(1);
			Vector3 reducedScale = MockProtectorInstance.transform.localScale;
			Assert.IsTrue(initialScale.z > reducedScale.z);
		}


		//// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
		//// `yield return null;` to skip a frame.
		//[UnityTest]
		//public IEnumerator ProtectorFormationTestWithEnumeratorPasses()
		//{
		//	// Use the Assert class to test conditions.
		//	// Use yield to skip a frame.
		//	yield return null;
		//}

		[TearDown]
		public void TearDown()
		{
			if (null != MockProtectorInstance)
			{
				Object.Destroy(MockProtectorInstance.gameObject);
			}
		}
	}
}