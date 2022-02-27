using System;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	[Serializable]
	public abstract class MessageInfo{ }

	public class ActorPositionInfo : MessageInfo{
		public Vector3 position;
		public float baseVelocity;
		public float gameVelocity;

		public ActorPositionInfo(){ }

		public ActorPositionInfo(Vector3 position, float baseVelocity, float gameVelocity){
			this.position = position;
			this.baseVelocity = baseVelocity;
			this.gameVelocity = gameVelocity;
		}
	}

	public class ActorTeleportedInfo : MessageInfo{
		public Vector3 originPosition;
		public Vector3 teleportedPosition;

		public ActorTeleportedInfo(Vector3 originPosition, Vector3 teleportedPosition){
			this.originPosition = originPosition;
			this.teleportedPosition = teleportedPosition;
		}
	}

	public class ActorRewardTimeInfo : MessageInfo{
		public string rewardType;

		public ActorRewardTimeInfo(string rewardType){
			this.rewardType = rewardType;
		}
	}

	public class ActorSelectedInfo : MessageInfo{
		public bool isRight;

		public ActorSelectedInfo(bool isRight){
			this.isRight = isRight;
		}
	}
}