using NUnit.Framework;
using UnityEngine;
using System.Linq;

namespace Match_Invaders.Logic.Tests
{
	public class ProtectorFormationTest
	{
		public ProtectorFormation Formation;
		private const int BoundsSize = 25;
		private const float FleetInterval = 5;
		private const int ProtectorsToMake = 5;
		private const int InitialHP = 5;
		[SetUp]
		public void Setup() => Formation = ProtectorFormation.InstantiateFormationOrigin(Vector3.zero, MakeMockProtector());

		private Protector MakeMockProtector()
		{
			GameObject go = new GameObject("Mock Protector");
			return go.AddComponent<Protector>();
		}

		[Test]
		public void CheckFormationGetsFilled()
		{
			Formation.FillBasedOnConfigData(BoundsSize, BoundsSize, FleetInterval, ProtectorsToMake, InitialHP);
			Assert.IsTrue(Formation.Members.Count == ProtectorsToMake);
		}

		[Test]
		public void CheckProtectorsHealthy()
		{
			Formation.FillBasedOnConfigData(BoundsSize, BoundsSize, FleetInterval, ProtectorsToMake, InitialHP);
			Assert.IsTrue(Formation.Members.All(o => InitialHP == o.HP));
		}

		[Test]
		public void CheckProtectorsMaxHealthCorrect()
		{
			Formation.FillBasedOnConfigData(BoundsSize, BoundsSize, FleetInterval, ProtectorsToMake, InitialHP);
			Assert.IsTrue(Formation.Members.All(o => InitialHP == o.MaxHP));
		}

		[Test]
		public void CheckFormationGetsCleared()
		{
			Formation.FillBasedOnConfigData(BoundsSize, BoundsSize, FleetInterval, ProtectorsToMake, InitialHP);
			Formation.ClearFormation();
			Assert.IsTrue(0 == Formation.Members.Count);
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
			if (null != Formation)
			{
				Object.Destroy(Formation.gameObject);
			}
		}
	}
}