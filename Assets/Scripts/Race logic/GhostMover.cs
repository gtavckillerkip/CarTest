using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarTest.Gameplay.RaceLogic
{
	internal sealed class GhostMover : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _rotationSpeed;

		public void MoveUponRoute(IEnumerable<(Vector3 Position, Quaternion Rotation)> route, bool objectInFirstPosition = false)
		{
			if (objectInFirstPosition == false)
			{
				var (position0, rotation0) = route.ElementAt(0);

				transform.SetPositionAndRotation(position0, rotation0);
			}

			StartCoroutine(Move(route));
		}

		private IEnumerator Move(IEnumerable<(Vector3 Position, Quaternion Rotation)> route)
		{
			var previousElement = route.ElementAt(0);

			int index = 1;

			while (true)
			{
				if (route.Count() <= index)
				{
					yield return null;
					continue;
				}

				var element = route.ElementAt(index);

				Quaternion rotation = Quaternion.identity;
				var lookRotationVector = element.Position - previousElement.Position;
				if (lookRotationVector != Vector3.zero)
				{
					rotation = Quaternion.LookRotation(element.Position - previousElement.Position);
				}

				while ((transform.position - element.Position).magnitude > Mathf.Epsilon)
				{
					transform.position = Vector3.MoveTowards(transform.position, element.Position, _speed * Time.deltaTime);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

					yield return null;
				}

				previousElement = element;

				index++;
			}
		}
	}
}
