using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTest.Gameplay.RaceLogic
{
	internal sealed class RouteRecorder : MonoBehaviour
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private RaceHandler _raceHandler;
		[Space]
		[SerializeField] private RouteRecorder _routeRecorder;

		private readonly float _timeStep = 1f;

		private List<(Vector3 Position, Quaternion Direction)> _route = new();

		public IEnumerable<(Vector3 Position, Quaternion Direction)> Route => _route;

		private void OnEnable()
		{
			_raceHandler.CurrentLapChanged += HandleCurrentLapChanged;
		}

		private void OnDisable()
		{
			_raceHandler.CurrentLapChanged -= HandleCurrentLapChanged;
		}

		private void HandleCurrentLapChanged(int newValue)
		{
			switch (newValue)
			{
				case 1:
					StartCoroutine(RecordPoints());
					break;

				case 2:
					enabled = false;
					break;
			}
		}

		private IEnumerator RecordPoints()
		{
			float t = _timeStep;

			do
			{
				if (t >= _timeStep)
				{
					t = 0;
					_route.Add((_playerTransform.position, _playerTransform.rotation));
				}

				t += Time.deltaTime;

				yield return null;
			} while (true);
		}
	}
}
