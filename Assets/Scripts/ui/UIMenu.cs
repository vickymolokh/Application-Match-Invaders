using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match_Invaders.UI
{
	public class UIMenu : MonoBehaviour, IUIMenu
	{
		[SerializeField]
		private UnityEngine.UI.Text _highScoreText;

		[SerializeField]
		private CanvasGroup _mainMenuCanvasGroup;

		[SerializeField]
		private CanvasGroup _pausedMenuCanvasGroup;

		[SerializeField]
		private CanvasGroup _defeatMenuCanvasGroup;

		[SerializeField]
		private CanvasGroup _victoryMenuCanvasGroup;

		private IEnumerable<CanvasGroup> Groups // just extend the membership if we implement more menus
		{
			get
			{
				yield return _mainMenuCanvasGroup;
				yield return _pausedMenuCanvasGroup;
				yield return _defeatMenuCanvasGroup;
				yield return _victoryMenuCanvasGroup;
			}
		}
		private void ShowAtMostOneGroup(CanvasGroup group)
		{
			foreach (CanvasGroup existingGroup in Groups)
			{
				SetVisibility(existingGroup, existingGroup == group);
			}
		}

		private void SetVisibility(CanvasGroup group, bool visibility)
		{
			group.alpha = visibility?1f:0f;
			group.interactable = visibility;
			group.blocksRaycasts = visibility;
		}

		public void Hide() => ShowAtMostOneGroup(null);

		public void ShowMainMenu() => ShowAtMostOneGroup(_mainMenuCanvasGroup);
		public void ShowPausedInGameMenu() => ShowAtMostOneGroup(_pausedMenuCanvasGroup);
		public void ShowDefeatMenu() => ShowAtMostOneGroup(_defeatMenuCanvasGroup);
		public void ShowVictoryMenu() => ShowAtMostOneGroup(_victoryMenuCanvasGroup);
		public void SetHighScore(int score)
		{
			_highScoreText.text = "High Score: " + score.ToString();
		}
	}
}