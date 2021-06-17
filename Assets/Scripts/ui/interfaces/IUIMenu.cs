namespace Match_Invaders.UI
{
	public interface IUIMenu
	{
		void Hide();
		void ShowPausedInGameMenu();
		void ShowMainMenu();
		void ShowVictoryMenu();
		void ShowDefeatMenu();
		void SetHighScore(int score);
	}
}