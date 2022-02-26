using UnityEngine;

namespace Environment.Scripts{
	[System.Serializable]
	public class Path{
		public Vector3 startPosition;
		public Vector3 endPosition;

		public Path(Vector3 startPosition, Vector3 endPosition){
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}
	}
}