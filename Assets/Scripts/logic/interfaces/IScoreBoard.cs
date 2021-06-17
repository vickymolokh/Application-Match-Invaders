namespace Match_Invaders.Logic
{
	public delegate void HighScoreChangedDelegate(int score);
	public interface IScoreBoard
	{
		event HighScoreChangedDelegate OnHighScoreChanged;
		int CurrentScore { get; set; }
		int HighScore { get; set; }
		void AdjustCurrentScoreForKills(int killsInOneGo);
	}
}
