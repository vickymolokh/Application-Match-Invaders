namespace Match_Invaders.Logic
{
	public class StandardScoreBoard : IScoreBoard
	{
		public StandardScoreBoard()
		{
			LoadFromDisk();
		}

		private int _currentScore;
		public int CurrentScore
		{
			get => _currentScore;
			set
			{
				_currentScore = value;
				if (value > HighScore)
				{
					HighScore = _currentScore;	
				}
			}
		}

		public int _highScore;
		private static int killFormulaMultiplier;

		public int HighScore
		{
			get => _highScore;
			set // this is public to support score resetting
			{
				_highScore = value;
				//SaveToDisk();
			}
		}

		public void AdjustCurrentScoreForKills(int killsInOneGo) => CurrentScore += ScoreIncreaseForNKills(killsInOneGo);

		public static int ScoreIncreaseForNKills(int killsInOneGo)
		{
			int killFormulaMultiplier = 10;
			return killsInOneGo * Fibonacci(killsInOneGo + 1) * killFormulaMultiplier;
		}

		public static int Fibonacci(int count)
		{
			if (count < 0)
			{
				throw new System.ArgumentException("Fibonacci sequence negative count not supported");
			}
			// index
			// 0, 1, 2, 3, 4, 5, 6, 7,  8,  9
			// value
			// 0, 1, 1, 2, 3, 5, 8, 13, 21, 34
			int prePrevious = 0;
			int previous = 1;
			int currentSum = 0;
			for(int i = 1; i <= count; i++)
			{
				currentSum = prePrevious + previous;
				prePrevious = previous;
				previous = currentSum;
			}
			return prePrevious;
		}

		public void LoadFromDisk() => throw new System.NotImplementedException();
		public void SaveToDisk() => throw new System.NotImplementedException();
	}
}
