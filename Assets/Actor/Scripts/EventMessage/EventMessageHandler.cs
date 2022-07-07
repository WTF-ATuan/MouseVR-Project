using System;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class EventMessageHandler : MonoBehaviour{
		[SerializeField] [FolderPath] [Required]
		private string path;

		[SerializeField] [Required] private string dataName;

		[SerializeField] [Range(0.05f, 0.2f)] private float writeDuring = 0.1f;


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
			switch(dataInfo.eventType){
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
			dataInfo.CombineActorData(_actorData);
			dataInfo.CombineTrailData(_trailData);
			dataInfo.CombineEventData(_eventData);
			dataInfo.time = Time.time;
			_messageExporter.WriteMessage(dataInfo);
		}

		private void OnDisable(){
			_messageExporter.Timeout();
		}
	}
}