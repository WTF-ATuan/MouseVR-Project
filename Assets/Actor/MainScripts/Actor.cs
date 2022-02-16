using System;
using UnityEngine;

namespace Actor.MainScripts{
	public class Actor : MonoBehaviour{
		[SerializeField] private float speed = 1;

		public Vector3 StartPosition{ get; private set; }

		private CharacterController characterController;

		private void Start(){
			StartPosition = transform.position;
			characterController = GetComponent<CharacterController>();
		}

		public void Move(float inputValue){
			var forwardDirection = transform.forward;
			var moveDirection = inputValue * forwardDirection * speed;
			characterController.SimpleMove(moveDirection);
		}

		public void Teleport(Vector3 targetPosition){
			transform.position = targetPosition;
		}
	}
}