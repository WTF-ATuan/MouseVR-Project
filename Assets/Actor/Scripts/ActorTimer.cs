using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorTimer : MonoBehaviour
{
	[SerializeField] private float Timer = 0, LimitTime = 2.5f;


	void Start(){
		EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
		
	}

	private void OnActorMoveDetected(ActorMoveDetected obj){
		if(Timer > LimitTime)
		{
			EventBus.Post(new ActorJudged(true));
			Timer = 0;
		}
		else{
			if(Mathf.Abs(obj.InputSpeed) >= 0.3f){
				Timer = 0;
			}
			else{
				Timer += Time.deltaTime;
			}
		}
	}
}