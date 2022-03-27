﻿using System;
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

		public void SetAreaType(AreaType areaType){
			type = areaType;
		}

		public async void ActorCollision(Actor.Scripts.Actor actor){
			switch(type){
				case AreaType.Award:
					Debug.Log($"Get Award waiting for 3s");
					await Task.Delay(3000);
					actor.ReceiveReward("Sugar Water");
					actor.ResetActor();
					onExperimentCompleted?.Invoke(type);
					break;
				case AreaType.Punish:
					Debug.Log($"Get Punish waiting for 6s");
					await Task.Delay(6000);
					actor.ResetActor();
					onExperimentCompleted?.Invoke(type);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public Action<AreaType> onExperimentCompleted;
	}
}