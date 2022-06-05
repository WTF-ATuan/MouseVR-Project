using System;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	[Serializable]
	public abstract class MessageInfo{ }

	public class ActorPositionInfo : MessageInfo{
		public float Animal_Speed;
		public float Animal_Distance;
		public float Actor_Speed;
		public float Actor_PositionX;
		public float Actor_PositionZ;
		public float Time;

		public ActorPositionInfo(){ }
	}
}