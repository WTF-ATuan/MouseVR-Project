using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class ActorPositionInfo{
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

	public class ActorTeleportedInfo{
		public Vector3 OriginPosition{ get; }
		public Vector3 TeleportedPosition{ get; }
		public float Time{ get; }

		public ActorTeleportedInfo(Vector3 originPosition, Vector3 teleportedPosition, float time){
			OriginPosition = originPosition;
			TeleportedPosition = teleportedPosition;
			Time = time;
		}
	}

	public class ActorRewardTimeInfo{
		public string RewardType{ get; }
		public float Time{ get; }

		public ActorRewardTimeInfo(string rewardType, float time){
			RewardType = rewardType;
			Time = time;
		}
	}

	public class ActorSelectedInfo{
		public bool IsRight{ get; }
		public float Time{ get; }

		public ActorSelectedInfo(bool isRight, float time){
			IsRight = isRight;
			Time = time;
		}
	}
}