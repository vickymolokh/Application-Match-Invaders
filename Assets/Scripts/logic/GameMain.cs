using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Invaders.Logic
{
	class GameMain : MonoBehaviour, IPlayerShipDamageReportReceiver, IBattlefieldClearedReceiver
	{



		enum States
		{
			Init = 0,
			MainMenu = 1,
			PlayingLevel = 2,
			PausedInGameMenu = 3,
			LevelClearedMenu = 4,
			LevelFailedMenu = 5,
		}

		public void BattlefieldClearedCallbackReceiver() => throw new NotImplementedException();
		public void PlayerDamagedCallbackReceiver(PlayerShipBehaviour sender) => throw new NotImplementedException();
	}
}
