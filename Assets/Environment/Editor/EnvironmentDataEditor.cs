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
		[HideIf("IsDataNull")] [HideLabel]
		public PathEditor pathEditor = new PathEditor();
		
	}
}