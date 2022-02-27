using System;
using System.IO;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class MessageExporter{
		public string FilePath{ get; private set; }

		public MessageExporter(string filePath){
			if(!System.IO.Directory.Exists(filePath)){
				throw new Exception("File Path is Not found");
			}

			FilePath = filePath;
		}

		public void SetFilePath(string path){
			if(!System.IO.Directory.Exists(path)){
				throw new Exception("File Path is Not found");
			}

			FilePath = path;
		}

		public string TranslateMessage<T>(T info) where T : MessageInfo{
			var fileName = typeof(T).Name;
			var jsonFile = JsonUtility.ToJson(info);
			jsonFile += $"Time :{Time.time}" + "\r\n";
			return fileName + jsonFile;
		}

		public void SaveToJsonFile(string rawMessage){
			var subs = rawMessage.Split('{');
			var messageType = subs[0];
			var saveFilePath = FilePath + "/" + $"{messageType}" + ".json";
			var fileStream = new FileStream(saveFilePath, FileMode.CreateNew);
			using(var streamWriter = new StreamWriter(fileStream)){
				streamWriter.Write(rawMessage);
			}
		}
	}
}