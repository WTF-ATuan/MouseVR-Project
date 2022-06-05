using System;
using System.Collections.Generic;
using System.Linq;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler : MonoBehaviour{
		[SerializeField] [FolderPath] [Required]
		private string path;

		[SerializeField] [Required] private string dataName;


		private readonly MessageExporter messageExporter;
		private readonly EventMessageStore messageStore;

		private void Start(){
			EventBus.Subscribe<SavedDataMessage<MessageInfo>>(OnSavedDataMessage);
		}

		private void OnSavedDataMessage(SavedDataMessage<MessageInfo> obj){
			var type = obj.Type;
			var data = obj.Data;
			messageStore.Store(type, data);
		}

		public void ExportMessage(){
			messageExporter.SetFilePath(path);
			var allTypeMessage = messageStore.GetAll();
			var allMessage = TranslateAllMessage(allTypeMessage);
			messageExporter.SaveToJsonFile(allMessage);
		}

		private string TranslateAllMessage(IEnumerable<List<MessageInfo>> allTypeMessage){
			var allMessage =
					(from messages in allTypeMessage
						from message in messages
						select messageExporter.TranslateMessage(message))
					.Aggregate(string.Empty, (current, s) => current + s);
			return allMessage;
		}
	}
}