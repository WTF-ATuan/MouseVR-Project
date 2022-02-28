using System.Collections.Generic;
using System.Linq;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler{
		private readonly MessageExporter messageExporter;
		private readonly EventMessageStore messageStore;

		public EventMessageHandler(MessageExporter messageExporter, EventMessageStore messageStore){
			this.messageExporter = messageExporter;
			this.messageStore = messageStore;
		}

		public void SaveMessage<T>(T messageInfo) where T : MessageInfo{
			messageStore.Store(messageInfo);
		}

		public void ExportMessage(string path){
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