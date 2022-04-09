using Actor.Scripts;
using Actor.Scripts.Event;
using Environment.Scripts;
using Project;
using UnityEngine;

namespace Puzzle.GameLogic.Scripts{
	public class BehaviorEnvironmentLine : MonoBehaviour{
		private ActorTimer timer;
		private LickTrigger trigger;

		private void Start(){
			timer = GetComponent<ActorTimer>();
			trigger = FindObjectOfType<LickTrigger>();
			EventBus.Subscribe<ActorMoveDetected>(timer.OnActorMoveDetected);
			EventBus.Subscribe<ActorLickDetected>(trigger.OnActorLickDetected);
		}
	}
}