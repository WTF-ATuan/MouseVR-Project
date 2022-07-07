using System;
using Actor.Scripts.EventMessage;

namespace Actor.Scripts.Event{
	public class SavedDataMessage{
		public BehaviorDataInfo Message{ get; }
		public Type Type{ get; }

		public SavedDataMessage(BehaviorDataInfo message, Type type){
			Message = message;
			Type = type;
		}
	}
}