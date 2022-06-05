using System;
using Actor.Scripts.EventMessage;

namespace Actor.Scripts.Event{
	public class SavedDataMessage{
		public MessageInfo Message{ get; }
		public Type Type{ get; }

		public SavedDataMessage(MessageInfo message, Type type){
			Message = message;
			Type = type;
		}
	}
}