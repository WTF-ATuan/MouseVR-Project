using System;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Scripts{
	public class ActorTimer : MonoBehaviour{
		[SerializeField] private float timer = 0;
		[SerializeField] private float limitTime = 2.5f;

		[SerializeField] private float limitSpeed;


		private void Start()
		{
			EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
		}

		public void OnActorMoveDetected(ActorMoveDetected obj){
			if(timer > limitTime){
				EventBus.Post(new ActorJudged(true));
				timer = 0;
			}
			else{
				if(Mathf.Abs(obj.InputSpeed) >= limitSpeed){
					timer = 0;
				}
				else{
					timer += Time.deltaTime;
				}
			}
		}

		public void SetLimitSpeed(float value)
		{
			limitSpeed = value;
		}

		public void SetLimitTime(float value)
		{
			limitTime = value;
		}
	}
}