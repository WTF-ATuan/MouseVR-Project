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
			
			//TODO
			EventBus.Subscribe<ActorJudged>(OnActorJudged);
		}

		private async void OnActorJudged(ActorJudged obj)
		{
			var isPunish = obj.isPunish;
			
			if (isPunish)
			{
				await Task.Delay(1000);
				actor.ResetActor();
			}
			else
			{
				actor.ReceiveReward("Lick");
				actor.ResetActor();
			}
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

		private void DetectActorLick()
		{
			actor.Lick();
		}

		private void DetectDirectionAngle(){
			var left = Input.GetKey(KeyCode.A);
			var right = Input.GetKey(KeyCode.D);
			if(!right && !left) return;
			var isRight = false;
			if(!right && left) isRight = false;
			if(right && !left) isRight = true;
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