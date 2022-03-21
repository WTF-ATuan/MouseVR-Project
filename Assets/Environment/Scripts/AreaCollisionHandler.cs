using System;
using System.Threading.Tasks;
using Actor.Scripts;
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

		public async void ActorCollision(Actor.Scripts.Actor actor){
			switch(type){
				case AreaType.Award:
					await Task.Delay(3000);
					actor.ReceiveReward("Sugar Water");
					actor.Teleport(actor.StartPosition);
					break;
				case AreaType.Punish:
					await Task.Delay(6000);
					actor.Teleport(actor.StartPosition);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}