using UnityEngine;
using Match_Invaders.UI;

namespace Match_Invaders.Logic
{
	class GameMain : MonoBehaviour, ICombinedBattlefieldReportReceiver
	{
		private States _gameState = States.Init;
		private Battlefield _battlefieldBuilder;
		private BattleConfiguration _config;
		private int _currentLevel;
		[SerializeField]
		public UIHUD _HUD;
		[SerializeField]
		public UIMenu _menu;
		public IHUDUpdater HUDUpdater => _HUD;
		public IUIMenu UIMenu => _menu;
		public IScoreBoard ScoreBoard; // = new StandardScoreBoard();

		private enum States
		{
			Init = 0,
			MainMenu = 1,
			LevelPlaying = 2,
			PausedInGameMenu = 3,
			LevelClearedMenu = 4,
			LevelFailedMenu = 5,
		}

		public bool PausedTimeScale // timescale operation only; not accounting for menus &c.
		{
			get => Time.timeScale <= Mathf.Epsilon; // avoid using == with floats
			set 
			{
				if (value)
				{
					Time.timeScale = 0f;
				}
				else
				{
					Time.timeScale = 1f;
				}
			}
		}

		private void LoadConfig()
		{
			const string DefaultConfigName = "DefaultBattleConfig";
			_config = Resources.Load<BattleConfiguration>(DefaultConfigName);
		}

		public void StartGame()
		{
			LoadConfig();
			UIMenu.Hide();
			_currentLevel = 1;
			StartLevel(_currentLevel);
		}

		public void StartLevel(int level)
		{
			// hypothetically one could load a different config here, creating different battlefields for different levels. But this is not part of the requirements.
			PausedTimeScale = false;
			CleanBattlefieldIfNeeded();
			UIMenu.Hide();
			_gameState = States.LevelPlaying;
			_battlefieldBuilder = new Battlefield(_config, this);
			HUDUpdater.CurrentLevel = level;
			HUDUpdater.PlayerHP = _config.PlayerHP;
		}

		private void CleanBattlefieldIfNeeded()
		{
			if (null != _battlefieldBuilder)
			{
				_battlefieldBuilder.DestroyBattlefield();
			}
		}

		public void PauseAndEscapeToGameMenu()
		{
			PausedTimeScale = true;
			UIMenu.ShowPausedInGameMenu();
			_gameState = States.PausedInGameMenu;
		}

		public void ResumePlaying()
		{
			PausedTimeScale = false;
			UIMenu.Hide();
			_gameState = States.LevelPlaying;
		}

		public void GoToMainMenu()
		{
			PausedTimeScale = false; // avoid keeping TS0.
			CleanBattlefieldIfNeeded();
			_gameState = States.MainMenu;
			UIMenu.ShowMainMenu();
			ScoreBoard.CurrentScore = 0;
		}

		public void GoToDefeatMenu()
		{
			_gameState = States.LevelFailedMenu;
			PausedTimeScale = true; // freeze and show last 'frame'
		}

		public void GoToVictoryMenu()
		{
			_gameState = States.LevelClearedMenu;
			PausedTimeScale = true; // freeze and show last 'frame'
		}

		public void GoToNextLevel()
		{
			_currentLevel++;
			StartLevel(_currentLevel);
		}

		public void QuitImmediately() => Application.Quit();

		public void BattlefieldClearedCallbackReceiver() => GoToVictoryMenu();
		public void KillsOccurredCallbackReceiver(int killsInOneGo) => ScoreBoard.AdjustCurrentScoreForKills(killsInOneGo);

		public void PlayerDamagedCallbackReceiver(PlayerShipBehaviour sender)
		{
			HUDUpdater.PlayerHP = sender.HP;
			if (sender.HP<=0)
			{
				GoToDefeatMenu();
			}
		}

		public void Update() => EnumStateMachine();
		private void EnumStateMachine()
		{
			switch (_gameState)
			{
				case States.Init:
					GoToMainMenu();
					break;
				case States.MainMenu: // leaving this for clarity only
					if (ConfirmKeyDown)
					{
						StartGame();
					}
					// note that escape does *not* try to quit the application to avoid accidental presses.
					break;
				case States.LevelPlaying:
					if (EscapeKeyDown)
					{
						PauseAndEscapeToGameMenu();
					}
					break;
				case States.PausedInGameMenu:
					if (EscapeKeyDown)
					{
						ResumePlaying();
					}
					break;
				case States.LevelClearedMenu:
					if (ConfirmKeyDown)
					{
						GoToNextLevel();
					}
					break;
				case States.LevelFailedMenu:
					if (ConfirmKeyDown)
					{
						StartGame(); // retry
					}
					if (EscapeKeyDown)
					{
						GoToMainMenu();
					}
					break;
			}
		}

		private bool ConfirmKeyDown => Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);
		private bool EscapeKeyDown => Input.GetKeyDown(KeyCode.Escape);
	}
}
