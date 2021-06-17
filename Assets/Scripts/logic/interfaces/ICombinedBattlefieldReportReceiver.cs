namespace Match_Invaders.Logic
{
	public interface ICombinedBattlefieldReportReceiver : IBattlefieldClearedReceiver, IKillReportReceiver, IPlayerShipDamageReportReceiver
	{
	}
}
