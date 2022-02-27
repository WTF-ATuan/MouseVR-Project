using System;
using Actor.Scripts.EventMessage;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Actor.ActorTests{
	public class MessageExporterTests{
		private readonly MessageExporter messageExporter = new MessageExporter(Application.dataPath);


		[Test]
		public void SetFilePath(){
			var dataPath = Application.dataPath;
			messageExporter.SetFilePath(dataPath);
			Assert.AreEqual(dataPath, messageExporter.FilePath);
		}

		[Test]
		public void TranslateMessage(){
			var actorPositionInfo = new ActorPositionInfo(Vector3.zero, 1, 2);
			var message = messageExporter.TranslateMessage(actorPositionInfo);
			Debug.Log($"message = {message}");
			Assert.NotNull(message);
		}

		[Test]
		public void SaveToJsonFile(){
			//Set Path
			var dataPath = Application.dataPath + "/Actor/MessageData";
			messageExporter.SetFilePath(dataPath);
			//Prepare Json File
			var actorPositionInfo = new ActorPositionInfo(Vector3.zero, 1, 2);
			var jsonMessage = messageExporter.TranslateMessage(actorPositionInfo);
			messageExporter.SaveToJsonFile(jsonMessage);
		}

		[Test]
		public void SaveMultipleFile(){
			var dataPath = Application.dataPath + "/Actor/MessageData";
			messageExporter.SetFilePath(dataPath);
			var messages = string.Empty;
			for(var i = 0; i < 10; i++){
				var actorPositionInfo = new ActorPositionInfo(Vector3.one, i, i + 1);
				var message = messageExporter.TranslateMessage(actorPositionInfo);
				messages += message;
			}

			messageExporter.SaveToJsonFile(messages);
		}
	}
}