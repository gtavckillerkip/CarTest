using CarTest.Gameplay.RaceLogic;
using System;
using UnityEngine;

namespace CarTest.Gameplay.Management
{
	internal sealed class InputManager : MonoBehaviour
	{
		[SerializeField] private GameplayStarter _gameplayStarter;
		[SerializeField] private RaceHandler _raceHandler;

		public event Action<bool> InputActiveChanged;

		public bool InputActive { get; private set; } = false;

		private void OnEnable()
		{
			_gameplayStarter.CountdownComplete += HandleCountdownComplete;
			_raceHandler.RaceComplete += HandleRaceComplete;
		}

		private void OnDisable()
		{
			_gameplayStarter.CountdownComplete -= HandleCountdownComplete;
			_raceHandler.RaceComplete -= HandleRaceComplete;
		}

		private void HandleCountdownComplete() => InputActiveChanged?.Invoke(InputActive = true);

		private void HandleRaceComplete() => InputActiveChanged?.Invoke(InputActive = false);
	}
}
