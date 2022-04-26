using System.Threading.Tasks;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using UnityEngine;

namespace Actor.Scripts{
	public class ActorController : MonoBehaviour{
		private Actor actor;

		private void Start(){
			actor = GetComponent<Actor>();
			EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
			EventBus.Subscribe<ActorTeleportDetected>(OnActorTeleportDetected);
			EventBus.Subscribe<ActorJudged>(OnActorJudged);
		}

		private void OnActorJudged(ActorJudged obj){
			var isPunish = obj.isPunish;
			actor.ReceiveJudged(isPunish);
		}

		private void OnActorTeleportDetected(ActorTeleportDetected obj){
			var targetPosition = obj.TargetPosition;
			actor.Teleport(targetPosition);
		}

		private void OnActorMoveDetected(ActorMoveDetected obj){
			var inputSpeed = obj.InputSpeed;
			actor.Move(inputSpeed);
		}

		private void Update(){
			DetectMoveValue();
			DetectDirectionAngle();
			
			
			if(Input.GetKeyDown(KeyCode.Z)){
				DetectTeleportValue();
			}

			if(Input.GetKeyDown(KeyCode.X)){
				DetectActorLick();
			}
		}

		private void DetectActorLick(){
			actor.Lick();
		}

		private void DetectDirectionAngle(){
			var left = Input.GetKeyDown(KeyCode.A);
			var right = Input.GetKeyDown(KeyCode.D);
			if(!right && !left) return;
			var isRight = right && !left;
			actor.SelectDirection(isRight);
		}

		private void DetectMoveValue(){
			var scrollDeltaOffsetY = Input.GetAxisRaw("Vertical");
			EventBus.Post(new ActorMoveDetected(scrollDeltaOffsetY));
		}

		private void DetectTeleportValue(){
			var actorStartPosition = actor.StartPosition;
			EventBus.Post(new ActorTeleportDetected(actorStartPosition));
		}
	}
}