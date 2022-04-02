using UnityEngine;

namespace Actor.Scripts{
	public class DirectRotate : MonoBehaviour, IRotate{
		[SerializeField] private float rotateAngle;

		private new Rigidbody rigidbody;
		private float currentAngle;

		private void Start(){
			rigidbody = GetComponent<Rigidbody>();
			currentAngle = transform.eulerAngles.y;
		}

		public void Rotate(bool isRight){
			var angle = isRight ? rotateAngle : -rotateAngle;
			if(isRight){
				currentAngle += angle;
				currentAngle = Mathf.Clamp(currentAngle, 0, angle);
			}
			else{
				currentAngle += angle;
				currentAngle = Mathf.Clamp(currentAngle, angle, 0);
			}

			var targetAngle = Vector3.up * currentAngle;
			var deltaAngle = Quaternion.Euler(targetAngle);
			rigidbody.MoveRotation(deltaAngle);
		}
	}
}