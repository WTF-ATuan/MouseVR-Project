using System;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler : MonoBehaviour{
		[SerializeField] [FolderPath] [Required]
		public string path;

		[SerializeField] [Required] public string dataName;

		[SerializeField] [Range(0.05f, 0.2f)] public float writeDuring = 0.1f;


		private MessageExporter _messageExporter;

		private BehaviorDataInfo _actorData, _trailData, _eventData;

		private float _invokeTime;

		private void Start(){
			EventBus.Subscribe<SavedDataMessage>(OnSavedDataMessage);
			_messageExporter = new MessageExporter(path, dataName);
		}

		private void Update(){
			var time = Time.time;
			if(!(time > _invokeTime)) return;
			WriteMessage();
			_invokeTime = time + writeDuring;
		}

		[Button]
		private void TestPostEvent(){
			var actorPositionInfo = new BehaviorDataInfo();
			actorPositionInfo.Animal_Speed = 1;
			actorPositionInfo.time = Time.time;
			EventBus.Post(new SavedDataMessage(actorPositionInfo, actorPositionInfo.GetType()));
		}

		private void OnSavedDataMessage(SavedDataMessage obj){
			var dataInfo = obj.Message;
			var eventType = obj.EventType;
			switch(eventType){
				case BehaviorEventType.Actor:
					_actorData = dataInfo;
					break;
				case BehaviorEventType.Trial:
					_trailData = dataInfo;
					break;
				case BehaviorEventType.Event:
					_eventData = dataInfo;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void WriteMessage(){
			var dataInfo = new BehaviorDataInfo();
			if(_actorData != null){
				dataInfo.CombineActorData(_actorData);
				_actorData = null;
			}

			if(_trailData != null){
				dataInfo.CombineTrailData(_trailData);
				_trailData = null;
			}

			if(_eventData != null){
				dataInfo.CombineEventData(_eventData);
				_eventData = null;
			}

			dataInfo.time = Time.time;
			_messageExporter.WriteMessage(dataInfo);
		}

		private void OnDisable(){
			_messageExporter.Timeout();
		}
	}
}