using Actor.Scripts;
using UnityEngine;

namespace Environment.Scripts{
	[RequireComponent(typeof(BoxCollider))]
	public class AreaCollisionHandler : MonoBehaviour , IActorCollision{
		[SerializeField] private AreaType type = AreaType.Award;

		private BoxCollider boxCollider;

		private void Start(){
			boxCollider = GetComponent<BoxCollider>();
		}

		public void ActorCollision(Actor.Scripts.Actor actor){
			
		}
		
	}
}