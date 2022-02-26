using Environment.Scripts;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Environment.Editor{
	public class EditEnvironmentData{
		[ShowInInspector] [InlineEditor(Expanded = true)]
		public EnvironmentData data;

		private bool IsDataNull => data == null;

		[Button(ButtonSizes.Small)]
		private void OpenDataPrefab(){
			if(IsDataNull) return;
			AssetDatabase.OpenAsset(data.mapModel);
		}
	}
}