using Environment.Scripts;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Environment.Editor{
	public class EditEnvironmentData{
		private bool IsDataNull => data == null;

		[HorizontalGroup("Data")] [ShowInInspector] [InlineEditor(Expanded = false)]
		public EnvironmentData data;

		[HorizontalGroup("Data")]
		[Button(ButtonSizes.Small)]
		private void OpenPrefab(){
			if(IsDataNull) return;
			AssetDatabase.OpenAsset(data.mapPrefab);
		}

		[HorizontalGroup("path")] [ShowInInspector] [InlineEditor(Expanded = false)]
		public PathCreator pathCreator;

		[HorizontalGroup("path")]
		[Button(ButtonSizes.Small)]
		private void AutoFind(){
			var stageHandle = StageUtility.GetCurrentStageHandle();
			var creator = stageHandle.FindComponentOfType<PathCreator>();
			if(creator != null) pathCreator = creator;
		}

		[HorizontalGroup("path")]
		[Button(ButtonSizes.Small)]
		private void Create(){
			var gameObject = new GameObject("PathCreator");
			var creator = gameObject.AddComponent<PathCreator>();
			StageUtility.PlaceGameObjectInCurrentStage(gameObject);
			pathCreator = creator;

		}
	}
}