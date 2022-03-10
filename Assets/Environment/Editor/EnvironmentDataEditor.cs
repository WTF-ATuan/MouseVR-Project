using Environment.Scripts;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Environment.Editor{
	public class EnvironmentDataEditor{
		private bool IsDataNull => data == null;

		[HorizontalGroup("Data")] [ShowInInspector] [InlineEditor(Expanded = false)]
		public EnvironmentData data;

		[HorizontalGroup("Data")]
		[Button(ButtonSizes.Small)]
		private void OpenPrefab(){
			if(IsDataNull) return;
			AssetDatabase.OpenAsset(data.mapPrefab);
		}

		[Title("Path Creator", "Hold *Left-Shift* to Create Dot *Left-Ctrl* to Delete ",TitleAlignments.Centered)]
		[HideIf("IsDataNull")]
		[HideLabel]
		public PathEditor pathEditor = new PathEditor();
	}
}