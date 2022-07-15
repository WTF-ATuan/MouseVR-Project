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

		private int _trailNumber;
		private int _trailSuccess;

		private void Start(){
			boxCollider = GetComponent<BoxCollider>();
			boxCollider.isTrigger = true;
		}

		public void SetAreaType(AreaType areaType){
			type = areaType;
		}

		public AreaType GetAreaType(){
			return type;
		}

		public void ActorCollision(Actor.Scripts.Actor actor){
			var behaviorDataInfo = new BehaviorDataInfo();
			switch(type){
				case AreaType.Award:
					EventBus.Post(new ActorJudged(false));
					_trailSuccess++;
					behaviorDataInfo.Trail_Success = _trailSuccess;
					onExperimentCompleted?.Invoke(type);
					break;
				case AreaType.Punish:
					EventBus.Post(new ActorJudged(true));
					onExperimentCompleted?.Invoke(type);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			_trailNumber++;
			behaviorDataInfo.Trail_Number = _trailNumber;
			EventBus.Post(new SavedDataMessage(behaviorDataInfo, behaviorDataInfo.GetType(), BehaviorEventType.Trial));
		}

		public Action<AreaType> onExperimentCompleted;
	}
}