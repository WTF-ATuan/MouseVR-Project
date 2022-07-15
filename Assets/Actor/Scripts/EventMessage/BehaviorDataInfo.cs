using System;
using UnityEngine.Serialization;

namespace Actor.Scripts.EventMessage{
	[Serializable]
	public abstract class MessageInfo{ }

	[Serializable]
	public class BehaviorDataInfo{
		//Actor
		public float Animal_Speed = default;
		public float Animal_Distance = default;
		public float Actor_Speed = default;
		public float Actor_PositionX = default;
		public float Actor_PositionZ = default;

		//Trial
		public int Licking = default;
		public int LeverPress = default;
		public int Trail_Number = default;
		public int Trail_Success = default;

		//Event
		public string Reward_Event = "NaN";
		public string Sync_Event = "NaN";
		public string Other_Event = "NaN";
		public float time;

		public BehaviorDataInfo(){ }

		public void CombineActorData(BehaviorDataInfo dataInfo){
			if(Animal_Speed == default){
				Animal_Speed = dataInfo.Animal_Speed;
			}

			if(Animal_Distance == default){
				Animal_Distance = dataInfo.Animal_Distance;
			}

			if(Actor_Speed == default){
				Actor_Speed = dataInfo.Actor_Speed;
			}

			if(Actor_PositionX == default){
				Actor_PositionX = dataInfo.Actor_PositionX;
			}

			if(Actor_PositionZ == default){
				Actor_PositionZ = dataInfo.Actor_PositionZ;
			}
		}

		public void CombineTrailData(BehaviorDataInfo dataInfo){
			if(Licking == default){
				Licking = dataInfo.Licking;
			}

			if(LeverPress == default){
				LeverPress = dataInfo.LeverPress;
			}

			if(Trail_Number == default){
				Trail_Number = dataInfo.Trail_Number;
			}

			if(Trail_Success == default){
				Trail_Success = dataInfo.Trail_Success;
			}
		}

		public void CombineEventData(BehaviorDataInfo dataInfo){
			if(Reward_Event.Equals("NaN")){
				Reward_Event = dataInfo.Reward_Event;
			}

			if(Sync_Event.Equals("NaN")){
				Sync_Event = dataInfo.Sync_Event;
			}

			if(Other_Event.Equals("NaN")){
				Other_Event = dataInfo.Other_Event;
			}
		}
	}

	public enum BehaviorEventType{
		Actor,
		Trial,
		Event,
	}
}