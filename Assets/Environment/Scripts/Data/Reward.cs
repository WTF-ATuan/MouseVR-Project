using UnityEngine;

namespace Environment.Scripts{
	[System.Serializable]
	public class Reward{
		public Path rewardPath;
		public string rewardMessage;

		public Reward(Path rewardPath, string rewardMessage){
			this.rewardPath = rewardPath;
			this.rewardMessage = rewardMessage;
		}
	}
}