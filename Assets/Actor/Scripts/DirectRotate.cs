using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts{
	public class DirectRotate : MonoBehaviour, IRotate{
		[SerializeField] private float rotateAngle;
		[ProgressBar(30, 90)] [SerializeField] private float anglePerSecond = 40;

		private new Rigidbody rigidbody;
		private float currentAngle;

		private void Start(){
			rigidbody = GetComponent<Rigidbody>();
			currentAngle = transform.eulerAngles.y;
		}

		public void Rotate(bool isRight){
			currentAngle = transform.eulerAngles.y;
			var angle = isRight ? rotateAngle : -rotateAngle;
			currentAngle += angle;
			var targetAngle = Vector3.up * currentAngle;
			rigidbody.DORotate(targetAngle, rotateAngle / anglePerSecond);
		}
	}
}