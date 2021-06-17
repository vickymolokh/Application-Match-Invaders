namespace Match_Invaders.Logic
{
	public interface IKillReportReceiver
	{
		void KillsOccurredCallbackReceiver(int killsInOneGo);
	}
}
