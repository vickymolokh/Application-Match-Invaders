using UnityEngine;
using UnityEngine.UI;
namespace Match_Invaders.UI
{
	public class UIHUD : MonoBehaviour, IHUDUpdater
	{
		[SerializeField] private CanvasGroup _HUDCanvasGroup;
		[SerializeField] private Text _yourScoreText;
		[SerializeField] private Text _highScoreText;
		[SerializeField] private Text _playerHPText;
		[SerializeField] private Text _levelText;
		[SerializeField] private CanvasGroup _markOfTheContender;
		[SerializeField] private CanvasGroup _markOfTheChampion;

		public int PlayerHP { set => _playerHPText.text = "HP: " + value.ToString(); }
		public int CurrentLevel { set => _levelText.text = "Level: " + value.ToString(); }
		public int CurrentScore { set => _yourScoreText.text = "Your Score: " + value.ToString(); }

		public int HighScore { set => _highScoreText.text = "High Score: " + value.ToString(); }
		public bool MarkOfTheChampion { set => SetVisibility(_markOfTheChampion, value); }
		public bool MarkOfTheContender { set => SetVisibility(_markOfTheContender, value); }

		public void SetVisibility(CanvasGroup group, bool visibility) => group.alpha = visibility ? 1f : 0f;

		public void HideHUD() => SetVisibility(_HUDCanvasGroup, false);
		public void ShowHUD() => SetVisibility(_HUDCanvasGroup, true);
	}
}
