using System;
using UnityEngine;

namespace CarTest.Gameplay.RaceLogic
{
	internal sealed class LapEntryTrigger : MonoBehaviour
	{
		public event Action<LapEntryTrigger> Entered;

		private void OnTriggerEnter(Collider other)
		{
			Entered?.Invoke(this);
		}
	}
}
