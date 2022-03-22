using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts{
	public class Actor : MonoBehaviour{
		[SerializeField] private float speed = 5;
		[SerializeField] private int rotateAngle;


		public Vector3 StartPosition{ get; private set; }

		private new Rigidbody rigidbody;

		private void Start(){
			StartPosition = transform.position;
			rigidbody = GetComponent<Rigidbody>();
		}

		public void Move(float inputValue){
			var forwardDirection = transform.forward;
			var moveDirection = inputValue * forwardDirection * speed;
			rigidbody.velocity = moveDirection;
		}

		public void Teleport(Vector3 targetPosition){
			transform.position = targetPosition;
		}
		[Button]
		public void ResetActor(){
			Teleport(StartPosition);
			transform.eulerAngles = Vector3.zero;
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