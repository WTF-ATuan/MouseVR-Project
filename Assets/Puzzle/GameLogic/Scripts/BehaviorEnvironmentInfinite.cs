using Actor.Scripts.Event;
using Environment.Scripts;
using Project;
using UnityEngine;

namespace Puzzle.GameLogic.Scripts{
	public class BehaviorEnvironmentInfinite : MonoBehaviour{
		private LickTrigger trigger;

		private void Start(){
			trigger = FindObjectOfType<LickTrigger>();
			EventBus.Subscribe<ActorLickDetected>(trigger.OnActorLickDetected);
		}
	}
}