using System;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Scripts{
	public class LickTrigger : MonoBehaviour{
		[SerializeField] [ReadOnly] private int correctLickCount, wrongLickCount;

		[SerializeField] private int correctLickCountLimit, wrongLickCountLimit;

		private new Collider collider;

		private void Start(){
			collider = GetComponent<Collider>();
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<Actor.Scripts.Actor>())
			{
				EventBus.Post(new ActorJudged(true));
			}
			
		}


		public void OnActorLickDetected(ActorLickDetected obj){
			var isContains = collider.bounds.Contains(obj.LickPosition);

			if(isContains){
				correctLickCount++;
			}
			else{
				wrongLickCount++;
			}

			CalculateLickCount();
		}

		private void CalculateLickCount(){
			if(correctLickCount >= correctLickCountLimit){
				EventBus.Post(new ActorJudged(false));
				correctLickCount = 0;
				wrongLickCount = 0;
			}

			if(wrongLickCount >= wrongLickCountLimit){
				EventBus.Post(new ActorJudged(true));
				correctLickCount = 0;
				wrongLickCount = 0;
			}
		}

		private void OnDrawGizmos(){
			if(!collider) return;
			var bounds = collider.bounds;
			var boundsCenter = bounds.center;
			var boundsSize = bounds.size;
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(boundsCenter, boundsSize);
		}
	}
}