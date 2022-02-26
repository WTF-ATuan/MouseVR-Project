using UnityEngine;

namespace Environment.Scripts{
	[System.Serializable]
	public class Characteristic{
		public Vector3 position;
		public GameObject graphicView;
		public Characteristic(Vector3 position, GameObject graphicView){
			this.position = position;
			this.graphicView = graphicView;
		}
	}
}