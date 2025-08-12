using CarTest.Gameplay.Management;
using CarTest.Gameplay.RaceLogic;
using UnityEngine;

namespace CarTest.Gameplay.UI
{
	internal sealed class UIController : MonoBehaviour
	{
		[SerializeField] private GameplayStartUI _gameplayStartUI;
		[SerializeField] private LapsUI _lapsUI;
		[SerializeField] private RaceCompleteUI _raceCompleteUI;
		[Space]
		[SerializeField] private GameplayStarter _gameplayStarter;
		[SerializeField] private RaceHandler _raceHandler;

		private void OnEnable()
		{
			_gameplayStartUI.gameObject.SetActive(true);
			_lapsUI.gameObject.SetActive(false);
			_raceCompleteUI.gameObject.SetActive(false);

			_gameplayStarter.CountdownComplete += HandleCountdownComplete;
			_raceHandler.RaceComplete += HandleRaceComplete;
		}

		private void OnDisable()
		{
			_gameplayStarter.CountdownComplete -= HandleCountdownComplete;
			_raceHandler.RaceComplete -= HandleRaceComplete;
		}

		private void HandleCountdownComplete()
		{
			_gameplayStartUI.gameObject.SetActive(false);
			_lapsUI.gameObject.SetActive(true);
		}

		private void HandleRaceComplete()
		{
			_lapsUI.gameObject.SetActive(false);
			_raceCompleteUI.gameObject.SetActive(true);
		}
	}
}
