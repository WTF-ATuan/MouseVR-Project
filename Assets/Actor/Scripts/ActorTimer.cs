using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Scripts{
	public class ActorTimer : MonoBehaviour{
		[SerializeField] private float timer = 0;
		[SerializeField] private float limitTime = 2.5f;

		public void OnActorMoveDetected(ActorMoveDetected obj){
			if(timer > limitTime){
				EventBus.Post(new ActorJudged(true));
				timer = 0;
			}
			else{
				if(Mathf.Abs(obj.InputSpeed) >= 0.3f){
					timer = 0;
				}
				else{
					timer += Time.deltaTime;
				}
			}
		}
	}
}