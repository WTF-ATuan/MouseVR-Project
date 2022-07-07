using System;
using Actor.Scripts.EventMessage;

namespace Actor.Scripts.Event{
	public class SavedDataMessage{
		public BehaviorDataInfo Message{ get; }
		public Type Type{ get; }

		public BehaviorEventType EventType;

		public SavedDataMessage(BehaviorDataInfo message, Type type){
			Message = message;
			Type = type;
		}

		public SavedDataMessage(BehaviorDataInfo message, Type type, BehaviorEventType eventType){
			Message = message;
			Type = type;
			EventType = eventType;
		}
	}
}