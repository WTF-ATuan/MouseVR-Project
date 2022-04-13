using Actor.Scripts;
using UnityEngine;

namespace Puzzle.GameLogic.Scripts{
	public class RotateController : MonoBehaviour, IActorCollisionHandler{
		public void ActorCollision(Actor.Scripts.Actor actor){
			actor.canRotate = true;
		}
	}
}