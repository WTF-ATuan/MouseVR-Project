using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class MessageExporter{
		
		public string TranslateMessage(MessageInfo info){
			var jsonFile = JsonUtility.ToJson(info);
			return jsonFile;
		}
		
	}
}