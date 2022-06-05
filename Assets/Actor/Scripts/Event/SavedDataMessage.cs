using System;

namespace Actor.Scripts.Event{
	public class SavedDataMessage<T>{
		public T Data{ get; }
		public Type Type{ get; }

		public SavedDataMessage(T data, Type type){
			Data = data;
			Type = type;
		}
	}
}