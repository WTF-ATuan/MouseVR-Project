using UnityEngine;

namespace Actor.Scripts{
	public class Actor : MonoBehaviour{
		[SerializeField] private float speed = 5;
		[SerializeField] private int rotateAngle;
		

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

		public void ReceiveReward(string reward){
			Debug.Log($"reward = {reward}");
		}

		public void SelectDirection(bool isRight){
			var angel = isRight ? rotateAngle : -rotateAngle;
			var direction = new Vector3(0, angel, 0);
			transform.eulerAngles += direction;
		}

		public void SetMoveSpeed(float moveSpeed){
			speed = moveSpeed;
		}
	}
}