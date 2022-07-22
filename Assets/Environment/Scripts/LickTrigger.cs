using System;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.Scripts{
	public class LickTrigger : MonoBehaviour{
		[SerializeField] [ReadOnly] private int correctLickCount, wrongLickCount;

		[SerializeField] private int correctLickCountLimit, wrongLickCountLimit;

		[SerializeField] private bool isInfinite;

		[SerializeField] private bool showGizmos;

		[SerializeField] private bool skipJudge;

		[SerializeField] private float incentiveRate;


		private new Collider collider;

		private void Start(){
			collider = GetComponent<Collider>();
			
			EventBus.Subscribe<ChangeIncentiveRateDetected>(OnChangeIncentiveRateDetected);
		}

		private void OnChangeIncentiveRateDetected(ChangeIncentiveRateDetected obj)
		{
			incentiveRate = obj.incentiveRate;
		}

		private void OnTriggerExit(Collider other){
			//TODO
			if(other.GetComponent<Actor.Scripts.Actor>()){
				if(isInfinite)
				{
					EventBus.Post(new InfiniteLevelIns());
				}
				else
				{
					if (skipJudge)
					{
						EventBus.Post(new ActorJudged(false));
					}
					else
					{
						EventBus.Post(new ActorJudged(true));
					}
				}
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
			if(correctLickCount >= correctLickCountLimit)
			{
				EventBus.Post(new ActorJudged(false).onlyReward = true);
				correctLickCount = 0;
				wrongLickCount = 0;
			}

			if(wrongLickCount >= wrongLickCountLimit){
				if(!isInfinite) EventBus.Post(new ActorJudged(true));
				correctLickCount = 0;
				wrongLickCount = 0;
			}
		}

		private void OnDrawGizmos(){
			if(!showGizmos) return;
			collider = GetComponent<Collider>();
			var bounds = collider.bounds;
			var boundsCenter = bounds.center;
			var boundsSize = bounds.size;
			Gizmos.color = Color.red;
			Gizmos.DrawCube(boundsCenter, boundsSize);
		}

		public void SetCorrectLickCountLimit(int count)
		{
			correctLickCountLimit = count;
		}
		
		public void SetWrongLickCountLimit(int count)
		{
			wrongLickCountLimit = count;
		}

		public void SetGizmos(bool isActive)
		{
			showGizmos = isActive;
		}

		public void SetSkipJudge(bool isJudge)
		{
			skipJudge = isJudge;
		}

		public int GetCurrentLickCountLimit()
		{
			return correctLickCountLimit;
		}

		public int GetWrongLickCountLimit()
		{
			return wrongLickCountLimit;
		}
	}
}