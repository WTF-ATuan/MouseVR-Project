using UnityEngine;

namespace Actor.Scripts.Event{
	public class ActorTeleportDetected{
		public Vector3 TargetPosition{ get; }

		public ActorTeleportDetected(Vector3 targetPosition){
			TargetPosition = targetPosition;
		}
	}
}