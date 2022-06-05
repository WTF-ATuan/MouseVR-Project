using System;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	[Serializable]
	public abstract class MessageInfo{ }

	public class ActorBehaviorInfo : MessageInfo{
		public float Animal_Speed;
		public float Animal_Distance;
		public float Actor_Speed;
		public float Actor_PositionX;
		public float Actor_PositionZ;
		public float Time;

		public ActorBehaviorInfo(float animalSpeed, float actorSpeed, float animalDistance, float actorPositionX,
			float actorPositionZ){
			Animal_Speed = animalSpeed;
			Actor_Speed = actorSpeed;
			Animal_Distance = animalDistance;
			Actor_PositionX = actorPositionX;
			Actor_PositionZ = actorPositionZ;
			Time = UnityEngine.Time.time;
		}

		public ActorBehaviorInfo(){
			Time = UnityEngine.Time.time;
		}
	}
}