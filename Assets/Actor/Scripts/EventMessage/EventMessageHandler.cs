
using System;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler : MonoBehaviour{
		[SerializeField] [FolderPath] [Required]
		private string path;

		[SerializeField] [Required] private string dataName;


		private MessageExporter _messageExporter;

		private void Start(){
			EventBus.Subscribe<SavedDataMessage>(OnSavedDataMessage);
			_messageExporter = new MessageExporter(path, dataName);
		}

		[Button]
		private void TestPostEvent(){
			var actorPositionInfo = new BehaviorDataInfo();
			actorPositionInfo.Animal_Speed = 1;
			actorPositionInfo.Time = Time.time;
			EventBus.Post(new SavedDataMessage(actorPositionInfo, actorPositionInfo.GetType()));
		}

		private void OnSavedDataMessage(SavedDataMessage obj){
			var message = obj.Message;
			_messageExporter.WriteMessage(message);
		}

		private void OnDisable(){
			_messageExporter.Timeout();
		}
	}
}