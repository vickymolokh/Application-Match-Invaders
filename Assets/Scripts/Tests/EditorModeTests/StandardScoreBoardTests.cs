using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Match_Invaders.Logic;

namespace Match_Invaders.Logic.Tests
{
	public class StandardScoreBoardTests
	{
		// A Test behaves as an ordinary method
		[Test]
		public void FibonacciTestsSimplePasses()
		{
			List<int> FibonacciSequenceReference = new List<int>{ 0, 1, 1, 2, 3, 5, 8, 13, 21, 34};
			for (int i = 0; i < FibonacciSequenceReference.Count; i++)
			{
				int referenceNumber = FibonacciSequenceReference[i];
				int calculatedNumber = StandardScoreBoard.Fibonacci(i);
				Debug.Log($"checking refNum:{referenceNumber} vs calcNum:{calculatedNumber}");
				Assert.IsTrue(referenceNumber == calculatedNumber);
			}
		}

		[Test]
		public void ScoreIncreaseCalculationTestPasses()
		{
			//1 ship = 1 * 10 = 10 points
			//2 ships = 2 * 20 = 40 points
			//3 ships = 3 * 30 = 90 points
			//4 ships = 4 * 50 = 90 points [sic]
			//5 ships = 5 * 80 = 400 points
			Dictionary<int, int> killsAndReferenceScores = new Dictionary<int, int>
			{
				{1, 10},
				{2, 40 },
				{3, 90 },
				{4, 90 }, // this should be 200, but PRD says 90. The client will be informed.
				{5, 400 },
			};
			CompareReferenceAndCalculatedScoreIncreases(killsAndReferenceScores);
		}

		[Test]
		public void ScoreIncreaseCalculationTestPassesWithCorrectedNumbers()
		{
			Dictionary<int, int> killsAndReferenceScores = new Dictionary<int, int>
			{
				{1, 10},
				{2, 40 },
				{3, 90 },
				{4, 200 }, // with adjusted reference number
				{5, 400 },
			};
			CompareReferenceAndCalculatedScoreIncreases(killsAndReferenceScores);
		}

			private static void CompareReferenceAndCalculatedScoreIncreases(Dictionary<int, int> killsAndReferenceScores)
		{
			foreach (KeyValuePair<int, int> pair in killsAndReferenceScores)
			{
				int calculatedScoreIncrease = StandardScoreBoard.ScoreIncreaseForNKills(pair.Key);
				Debug.Log($"Comparing kill score calculation: N={pair.Key}, expected={pair.Value}, calculated={calculatedScoreIncrease}");
				Assert.IsTrue(pair.Value == calculatedScoreIncrease);
			}
		}

		[Test]
		public void PersistenceTest()
		{
			int cachedOriginalScore = -1;
			try
			{
				StandardScoreBoard boardA = new StandardScoreBoard();
				cachedOriginalScore = boardA.HighScore;

				int increasedScore = cachedOriginalScore + 1;
				boardA.HighScore = increasedScore;

				StandardScoreBoard boardB = new StandardScoreBoard();
				Assert.IsTrue(increasedScore == boardB.HighScore);
			}
			finally 
			{
				if (cachedOriginalScore >= 0)
				{
					StandardScoreBoard finalBoard = new StandardScoreBoard();
					finalBoard.HighScore = cachedOriginalScore;
				}
			}
		}



		// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
		// `yield return null;` to skip a frame.
		//[UnityTest]
		//public IEnumerator FibonacciTestsWithEnumeratorPasses()
		//{
		//    // Use the Assert class to test conditions.
		//    // Use yield to skip a frame.
		//    yield return null;
		//}
	}
}