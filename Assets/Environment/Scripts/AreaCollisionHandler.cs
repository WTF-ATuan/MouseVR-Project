using System;
using System.Threading.Tasks;
using Actor.Scripts;
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
			switch(type){
				case AreaType.Award:
					EventBus.Post(new ActorJudged(false));
					onExperimentCompleted?.Invoke(type);
					break;
				case AreaType.Punish:
					EventBus.Post(new ActorJudged(true));
					onExperimentCompleted?.Invoke(type);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public Action<AreaType> onExperimentCompleted;
	}
}