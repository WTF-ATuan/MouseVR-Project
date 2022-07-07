using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Actor.Scripts.EventMessage{
	public class MessageExporter{
		public string FilePath{ get; }
		public string FileName{ get; }

		private readonly FileStream _file;

		private string _fileFullName;

		private StreamWriter _streamWriter;

		public MessageExporter(string filePath, string fileName){
			_fileFullName = filePath + "/" + $"{fileName}" + ".json";
			_file = new FileStream(_fileFullName, FileMode.CreateNew);
			_streamWriter = new StreamWriter(_file);
			FilePath = filePath;
			FileName = fileName;
			AssetDatabase.Refresh();
		}


		public void WriteMessage(BehaviorDataInfo message){
			var jsonString = JsonUtility.ToJson(message);
			_streamWriter.Write(jsonString + System.Environment.NewLine);
			_streamWriter.Flush();
		}

		public void Timeout(){
			_streamWriter.Flush();
			_streamWriter.Close();
			_streamWriter.Dispose();
			AssetDatabase.Refresh();
		}
	}
}