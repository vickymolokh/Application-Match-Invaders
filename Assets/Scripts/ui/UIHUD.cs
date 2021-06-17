using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Match_Invaders.UI
{
	public class UIHUD : MonoBehaviour, IHUDUpdater
	{
		[SerializeField] private CanvasGroup _HUDCanvasGroup;
		[SerializeField] private Text _scoreText;
		
		[SerializeField] private Text _playerHPText;
		[SerializeField] private Text _levelText;

		public int PlayerHP { set => _playerHPText.text = "HP: " + value.ToString(); }
		public int CurrentLevel { set => _levelText.text = "Level: " + value.ToString(); }

		private int _currentScore;
		private int _highScore;
		
		public int CurrentScore
		{
			set
			{
				_currentScore = value;
				AdjustScoreDisplay();
			}
		}
		public int HighScore
		{
			set
			{
				_highScore = value;
				AdjustScoreDisplay();
			}
		}

		public void SetVisibility(CanvasGroup group, bool visibility) => group.alpha = visibility ? 1f : 0f;

		private void AdjustScoreDisplay()
		{
			bool currentScoreIsHighest = _highScore <= _currentScore;
			float closeEnoughMultiplier = 0.9f;
			bool approachingHighScore = _currentScore > (float)_highScore * closeEnoughMultiplier;

			StringBuilder builder = new StringBuilder();
			builder.Append($"Your Score: {_currentScore}\n");
			if (currentScoreIsHighest)
			{
				builder.Append("New high score!");
			}
			else
			{
				builder.Append($"High Score: {_highScore}\n");
				if (approachingHighScore)
				{
					builder.Append("You can do it!");
				}
			}
			_scoreText.text = builder.ToString();
		}

		public void HideHUD() => SetVisibility(_HUDCanvasGroup, false);
		public void ShowHUD() => SetVisibility(_HUDCanvasGroup, true);
	}
}
