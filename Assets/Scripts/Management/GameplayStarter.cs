using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarTest.Gameplay.Management
{
	internal sealed class GameplayStarter : MonoBehaviour
	{
		[SerializeField] private int _startCountdownTime = 3;

		private int? _countdownLeft = null;

		public event Action CountdownSecondPassed;
		public event Action CountdownComplete;

		public int? CountdownLeft => _countdownLeft;

		private void Awake()
		{
			_countdownLeft = _startCountdownTime;
		}

		private IEnumerator Start()
		{
			float t = 0;

			while (_countdownLeft > 0)
			{
				t += Time.deltaTime;

				if (t >= 1)
				{
					t = 0;
					_countdownLeft--;
					CountdownSecondPassed?.Invoke();
				}

				yield return null;
			}

			CountdownComplete?.Invoke();
		}

		public void Restart()
		{
			SceneManager.LoadScene(gameObject.scene.name);
		}
	}
}
