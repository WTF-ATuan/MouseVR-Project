using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts{
	public class DirectRotate : MonoBehaviour, IRotate{
		[SerializeField] private float rotateAngle;
		[ProgressBar(30, 360)] [SerializeField] private float anglePerSecond = 60;

		private new Rigidbody rigidbody;
		private float currentAngle;
		private Actor actor;

		private void Start(){
			rigidbody = GetComponent<Rigidbody>();
			actor = GetComponent<Actor>();
			currentAngle = transform.eulerAngles.y;
		}

		public void Rotate(bool isRight){
			currentAngle = transform.eulerAngles.y;
			var angle = isRight ? rotateAngle : -rotateAngle;
			if(currentAngle != 0) return;
			currentAngle += angle;
			var targetAngle = Vector3.up * currentAngle;
			rigidbody.DORotate(targetAngle, rotateAngle / anglePerSecond);
			actor.canMoveForward = true;
		}
	}
}