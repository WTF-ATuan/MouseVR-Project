using System;
using System.Collections.Generic;
using System.Linq;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler : MonoBehaviour{
		[SerializeField] [FolderPath] [Required] [ReadOnly]
		private string path;

		[SerializeField] [Required] private string dataName;


		private MessageExporter _messageExporter;
		private EventMessageStore _messageStore;

		private void Start(){
			EventBus.Subscribe<SavedDataMessage>(OnSavedDataMessage);
			_messageStore = new EventMessageStore();
			_messageExporter = new MessageExporter(path, dataName);
		}

		[Button]
		private void TestPostEvent(){
			var actorPositionInfo = new ActorPositionInfo();
			actorPositionInfo.Animal_Speed = 1;
			actorPositionInfo.Time = Time.time;
			EventBus.Post(new SavedDataMessage(actorPositionInfo, actorPositionInfo.GetType()));
		}

		[Button]
		private void TestPostEvent1(){
			var actorPositionInfo = new ActorPositionInfo();
			actorPositionInfo.Animal_Speed = 3;
			actorPositionInfo.Time = Time.time;
			EventBus.Post(new SavedDataMessage(actorPositionInfo, actorPositionInfo.GetType()));
		}

		private void OnSavedDataMessage(SavedDataMessage obj){
			var type = obj.Type;
			var message = obj.Message;
			_messageStore.Store(type, message);
		}

		[Button]
		public void ExportMessage(){
			_messageExporter.SetFilePath(path);
			var allTypeMessage = _messageStore.GetAll();
			var allMessage = TranslateAllMessage(allTypeMessage);
			_messageExporter.SaveToJsonFile(allMessage);
		}

		private string TranslateAllMessage(IEnumerable<List<MessageInfo>> allTypeMessage){
			var allMessage =
					(from messages in allTypeMessage
						from message in messages
						select _messageExporter.TranslateMessage(message))
					.Aggregate(string.Empty, (current, s) => current + s);
			return allMessage;
		}
	}
}