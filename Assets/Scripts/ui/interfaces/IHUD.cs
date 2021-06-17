namespace Match_Invaders.UI
{
	public interface IHUDUpdater
	{
		int PlayerHP { set; }
		int CurrentLevel { set; }
		int CurrentScore { set; }
	}
}
