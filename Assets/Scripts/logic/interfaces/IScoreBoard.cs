namespace Match_Invaders.Logic
{
	public interface IScoreBoard
	{

		int CurrentScore { get; set; }
		int HighScore { get; set; }
		void AdjustCurrentScoreForKills(int killsInOneGo);
	}
}
