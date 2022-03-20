using System;
using UnityEngine;

namespace Actor.Scripts{
	[RequireComponent(typeof(Actor))]
	public class ActorCollider : MonoBehaviour{
		private Actor actor;

		private void Start(){
			actor = GetComponent<Actor>();
		}

		private void OnCollisionEnter(Collision other){
			var otherGameObject = other.gameObject;
			var actorCollision = otherGameObject.GetComponent<IActorCollision>();
			actorCollision?.ActorCollision(actor);
		}

		private void OnTriggerEnter(Collider other){
			var actorCollision = other.GetComponent<IActorCollision>();
			actorCollision?.ActorCollision(actor);
		}
	}
}