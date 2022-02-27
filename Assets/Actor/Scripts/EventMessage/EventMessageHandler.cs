namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler{
		private MessageExporter messageExporter;
		private EventMessageStore messageStore;

		public void SandMessage<T>(T messageInfo) where T : MessageInfo{ }
	}
}