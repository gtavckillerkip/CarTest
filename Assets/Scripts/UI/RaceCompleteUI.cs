using CarTest.Gameplay.Management;
using UnityEngine;
using UnityEngine.UI;

namespace CarTest.Gameplay.UI
{
	internal sealed class RaceCompleteUI : MonoBehaviour
	{
		[SerializeField] private GameplayStarter _gameplayStarter;
		[Space]
		[SerializeField] private Button _restartButton;

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(HandleRestartButtonClicked);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveAllListeners();
		}

		private void HandleRestartButtonClicked()
		{
			_gameplayStarter.Restart();
		}
	}
}
