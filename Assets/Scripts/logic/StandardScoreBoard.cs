namespace Match_Invaders.Logic
{
	public class StandardScoreBoard : IScoreBoard
	{
		public StandardScoreBoard() => TryLoadFromDisk();

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

		public event HighScoreChangedDelegate OnHighScoreChanged;

		public int HighScore
		{
			get => _highScore;
			set // this is public to support score resetting
			{
				_highScore = value;
				OnHighScoreChanged?.Invoke(_highScore);
				SaveToDisk();
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
			for (int i = 1; i <= count; i++)
			{
				int currentSum = prePrevious + previous;
				prePrevious = previous;
				previous = currentSum;
			}
			return prePrevious;
		}

		private string HighScorePathAndFile => UnityEngine.Application.persistentDataPath +"/HighScore.txt";
		public void TryLoadFromDisk()
		{
			if (System.IO.File.Exists(HighScorePathAndFile))
			{
				string text = System.IO.File.ReadAllText(HighScorePathAndFile);
				UnityEngine.Debug.Log("Successfully read score text: " + text);
				if (int.TryParse(text, out int result))
				{
					HighScore = result;
				}
			}
			else
			{
				UnityEngine.Debug.Log("No HighScore.txt file found; starting from 0");
			}
		}

		public void SaveToDisk()
		{
			string text = HighScore.ToString(); // for something more serious than one int, a JSON would be appropriate
			System.IO.File.WriteAllText(HighScorePathAndFile, text);
		}
	}
}
