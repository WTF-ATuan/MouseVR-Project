using UnityEngine;

namespace Actor.Scripts{
	public class ActorInput : MonoBehaviour{
		private Actor actor;

		private void Start(){
			actor = GetComponent<Actor>();
		}

		private void Update(){
			MoveValueDetected();
			if(Input.GetKeyDown(KeyCode.Space)){
				TeleportValueDetected();
			}
		}

		private void MoveValueDetected(){
			var scrollDeltaOffsetY = Input.mouseScrollDelta.y;
			actor.Move(scrollDeltaOffsetY);
		}

		private void TeleportValueDetected(){
			var actorStartPosition = actor.StartPosition;
			actor.Teleport(actorStartPosition);
		}
	}
}