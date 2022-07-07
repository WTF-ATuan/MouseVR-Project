using System;
using System.Threading.Tasks;
using Actor.Scripts;
using Actor.Scripts.Event;
using Actor.Scripts.EventMessage;
using Environment.Scripts.Events;
using Project;
using UnityEngine;

namespace Environment.Scripts{
	[RequireComponent(typeof(BoxCollider))]
	public class AreaCollisionHandler : MonoBehaviour, IActorCollisionHandler{
		[SerializeField] private AreaType type = AreaType.Award;

		private BoxCollider boxCollider;

		private void Start(){
			boxCollider = GetComponent<BoxCollider>();
			boxCollider.isTrigger = true;
		}

		public void SetAreaType(AreaType areaType){
			type = areaType;
		}

		public void ActorCollision(Actor.Scripts.Actor actor){
			var behaviorDataInfo = new BehaviorDataInfo();
			switch(type){
				case AreaType.Award:
					EventBus.Post(new ActorJudged(false));
					behaviorDataInfo.Trail_Success = 1;
					onExperimentCompleted?.Invoke(type);
					break;
				case AreaType.Punish:
					EventBus.Post(new ActorJudged(true));
					behaviorDataInfo.Trial_Failed = 1;
					onExperimentCompleted?.Invoke(type);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			EventBus.Post(new SavedDataMessage(behaviorDataInfo, behaviorDataInfo.GetType(), BehaviorEventType.Trial));
		}

		public Action<AreaType> onExperimentCompleted;
	}
}