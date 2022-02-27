using System;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	[Serializable]
	public abstract class MessageInfo{ }

	public class ActorPositionInfo : MessageInfo{
		public Vector3 Position{ get; }
		public float BaseVelocity{ get; }
		public float GameVelocity{ get; }
		public float Time{ get; }

		public ActorPositionInfo(Vector3 position, float baseVelocity, float gameVelocity, float time){
			Position = position;
			BaseVelocity = baseVelocity;
			GameVelocity = gameVelocity;
			Time = time;
		}
	}

	public class ActorTeleportedInfo : MessageInfo{
		public Vector3 OriginPosition{ get; }
		public Vector3 TeleportedPosition{ get; }
		public float Time{ get; }

		public ActorTeleportedInfo(Vector3 originPosition, Vector3 teleportedPosition, float time){
			OriginPosition = originPosition;
			TeleportedPosition = teleportedPosition;
			Time = time;
		}
	}

	public class ActorRewardTimeInfo : MessageInfo{
		public string RewardType{ get; }
		public float Time{ get; }

		public ActorRewardTimeInfo(string rewardType, float time){
			RewardType = rewardType;
			Time = time;
		}
	}

	public class ActorSelectedInfo : MessageInfo{
		public bool IsRight{ get; }
		public float Time{ get; }

		public ActorSelectedInfo(bool isRight, float time){
			IsRight = isRight;
			Time = time;
		}
	}
}