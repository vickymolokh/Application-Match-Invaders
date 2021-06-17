namespace Match_Invaders.UI
{
	public interface IHUDUpdater
	{
		void HideHUD();
		void ShowHUD();
		int PlayerHP { set; }
		int CurrentLevel { set; }
		int CurrentScore { set; }
		int HighScore { set; }
		bool MarkOfTheChampion { set; }
		bool MarkOfTheContender { set; }
	}
}
