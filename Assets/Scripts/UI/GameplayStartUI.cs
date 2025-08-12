using CarTest.Gameplay.Management;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CarTest.Gameplay.UI
{
	internal sealed class GameplayStartUI : MonoBehaviour
	{
		[SerializeField] private GameplayStarter _gameplayStarter;
		[Space]
		[SerializeField] private TextMeshProUGUI _countdownText;

		private void OnEnable()
		{
			StartCoroutine(SetTextAfterGameplayStarterInitialized());

			_gameplayStarter.CountdownSecondPassed += HandleCountdownSecondPassed;
		}

		private void OnDisable()
		{
			_gameplayStarter.CountdownSecondPassed -= HandleCountdownSecondPassed;
		}

		private IEnumerator SetTextAfterGameplayStarterInitialized()
		{
			while (_gameplayStarter.CountdownLeft == null)
			{
				yield return null;
			}

			_countdownText.text = _gameplayStarter.CountdownLeft.ToString();
		}

		private void HandleCountdownSecondPassed()
		{
			_countdownText.text = _gameplayStarter.CountdownLeft.ToString();
		}
	}
}
