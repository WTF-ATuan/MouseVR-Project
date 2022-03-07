using UnityEngine;

namespace Environment.Editor{
	public class PathHandler : MonoBehaviour{
		public Vector3 StartPoint{ get; private set; }
		public Vector3 EndPoint{ get; private set; }

		public void Initialized(Vector3 startPoint, Vector3 endPoint){
			StartPoint = startPoint;
			EndPoint = endPoint;
		}
	}
}