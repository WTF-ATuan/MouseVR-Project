using System;
using Actor.Scripts;
using UnityEngine;

namespace Puzzle.GameLogic.Scripts{
	public class RotateController : MonoBehaviour, IActorCollisionHandler{
		private Actor.Scripts.Actor enterActor;

		public void ActorCollision(Actor.Scripts.Actor actor){
			enterActor = actor;
			actor.canRotate = true;
		}

		private void OnTriggerExit(Collider other){
			enterActor.canRotate = false;
		}
	}
}