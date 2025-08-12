using CarTest.Gameplay.RaceLogic;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CarTest.Gameplay.UI
{
	internal sealed class LapsUI : MonoBehaviour
	{
		[SerializeField] private RaceHandler _raceHandler;
		[Space]
		[SerializeField] private TextMeshProUGUI _currentLapText;
		[SerializeField] private TextMeshProUGUI _maxLapsText;

		private void OnEnable()
		{
			StartCoroutine(SetTextsAfterRaceHandlerInitialized());

			_raceHandler.CurrentLapChanged += HandleCurrentLapChanged;
		}

		private void OnDisable()
		{
			_raceHandler.CurrentLapChanged -= HandleCurrentLapChanged;
		}

		private IEnumerator SetTextsAfterRaceHandlerInitialized()
		{
			while (_raceHandler.CurrentLap == null && _raceHandler.MaxLaps == null)
			{
				yield return null;
			}

			_currentLapText.text = _raceHandler.CurrentLap.ToString();
			_maxLapsText.text = _raceHandler.MaxLaps.ToString();
		}

		private void HandleCurrentLapChanged(int newValue)
		{
			_currentLapText.text = newValue.ToString();
		}
	}
}
