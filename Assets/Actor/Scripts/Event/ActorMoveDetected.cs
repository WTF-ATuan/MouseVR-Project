namespace Actor.Scripts.Event{
	public class ActorMoveDetected{
		public float InputSpeed{ get; }

		public ActorMoveDetected(float inputSpeed){
			InputSpeed = inputSpeed;
		}
	}
}