using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
namespace Match_Invaders.Logic.Tests
{
	public class ExplosionPoolTests
	{
		public Explosion MockExplosionPrototype;
		IExplosionPool Pool;
		private const float Duration = 1f;
		private const float MaxScale = 1f;
		[SetUp]
		public void Setup()
		{
			MockExplosionPrototype = MakeMockExplosion();
			MockExplosionPrototype.Duration = Duration;
			MockExplosionPrototype.MaxScale = MaxScale;
			Pool = new ExplosionPool(MockExplosionPrototype);
		}

		private Explosion MakeMockExplosion()
		{
			GameObject go = new GameObject("Mock Explosion");
			return go.AddComponent<Explosion>();
		}

		[UnityTest]
		public IEnumerator CheckExplosionGrowsReasonably()
		{
			Explosion boom = Pool.ExplodeHere(Vector3.zero);
			float halfTime = Duration / 2f;
			float halfSize = MaxScale / 2f;
			yield return new WaitForSeconds(halfTime);
			float errorMargin = 0.1f; // no, we cannot reasonably expect Math.Epsilon level precision here.
			bool approxX = Mathf.Abs(boom.transform.localScale.x-halfSize) < errorMargin;
			bool approxY = Mathf.Abs(boom.transform.localScale.y-halfSize) < errorMargin;
			bool approxZ = Mathf.Abs(boom.transform.localScale.z-halfSize) < errorMargin;
			Assert.IsTrue(approxX && approxY && approxZ);
		}

		[UnityTest]
		public IEnumerator CheckExplosionDies()
		{
			Explosion boom = Pool.ExplodeHere(Vector3.zero);
			yield return new WaitForSeconds(Duration);
			yield return new WaitForFixedUpdate();
			Assert.IsFalse(boom.isActiveAndEnabled);
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
			if (null != MockExplosionPrototype)
			{
				Object.Destroy(MockExplosionPrototype.gameObject);
			}
		}
	}
}