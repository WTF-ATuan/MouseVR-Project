using System;
using UnityEngine;

namespace Actor.Scripts{
	public class LinearRotate : MonoBehaviour , IRotate{
		[SerializeField] private float rotateSpeed;

		private new Rigidbody rigidbody;

		private void Start(){
			rigidbody = GetComponent<Rigidbody>();
		}

		public void Rotate(bool isRight){
			var eulerAngel = isRight ? Vector3.up : Vector3.down;
			var deltaRotation = Quaternion.Euler(eulerAngel * rotateSpeed * Time.fixedDeltaTime);
			rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
		}
	}
}