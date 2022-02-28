using System;
using System.Collections.Generic;
using System.Linq;

namespace Actor.Scripts.EventMessage{
	public class EventMessageStore{
		private readonly Dictionary<Type, List<MessageInfo>> storeMessageInfos =
				new Dictionary<Type, List<MessageInfo>>();

		public void Store<T>(T message) where T : MessageInfo{
			var type = typeof(T);
			var containsKey = storeMessageInfos.ContainsKey(type);
			if(containsKey){
				storeMessageInfos[type].Add(message);
			}
			else{
				var messageList = new List<MessageInfo>{ message };
				storeMessageInfos.Add(type, messageList);
			}
		}

		public List<T> Get<T>() where T : MessageInfo{
			var type = typeof(T);
			if(storeMessageInfos.ContainsKey(type)){
				var messageInfos = storeMessageInfos[type];
				return messageInfos.Cast<T>().ToList();
			}
			else{
				return new List<T>();
			}
		}

		public List<List<MessageInfo>> GetAll(){
			var allMessage = storeMessageInfos.Values;
			return allMessage.ToList();
		}

		public void Clear(){
			storeMessageInfos.Clear();
		}
	}
}