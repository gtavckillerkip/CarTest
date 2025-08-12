using System;
using System.Linq;
using UnityEngine;

namespace CarTest.Gameplay.RaceLogic
{
	internal sealed class RaceHandler : MonoBehaviour
	{
		[SerializeField] private LapEntryTrigger _trigger;
		[Space]
		[SerializeField] private GameObject _ghostPrefab;
		[SerializeField] private RouteRecorder _routeRecorder;
		[Space]
		[SerializeField] private RaceHandler _raceHandler;

		private int? _currentLap = null;
		private int? _maxLaps = null;

		public event Action<int> CurrentLapChanged;
		public event Action RaceComplete;

		public int? CurrentLap => _currentLap;

		public int? MaxLaps => _maxLaps;

		private void Awake()
		{
			_currentLap = 0;
			_maxLaps = 2;
		}

		private void OnEnable()
		{
			_trigger.Entered += HandleLapEntryTriggerEntered;
		}

		private void OnDisable()
		{
			_trigger.Entered -= HandleLapEntryTriggerEntered;
		}

		private void HandleLapEntryTriggerEntered(LapEntryTrigger t)
		{
			_currentLap++;
			CurrentLapChanged?.Invoke(_currentLap.Value);

			if (_currentLap == 2)
			{
				var (position0, rotation0) = _routeRecorder.Route.ElementAt(0);

				var ghost = Instantiate(_ghostPrefab, position0, rotation0);

				var ghostMover = ghost.GetComponent<GhostMover>();

				ghostMover.MoveUponRoute(_routeRecorder.Route, true);
			}

			if (_currentLap > _maxLaps)
			{
				RaceComplete?.Invoke();
			}
		}
	}
}
