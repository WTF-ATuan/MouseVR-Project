using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Scripts{
	[CreateAssetMenu(fileName = "EnvironmentData", menuName = "Project/ EnvironmentData")]
	public class EnvironmentData : ScriptableObject{
		[BoxGroup("Basic Info")] [LabelWidth(100)]
		public string environmentName;

		[BoxGroup("Basic Info")] [LabelWidth(100)] [TextArea]
		public string description;

		[HorizontalGroup("Map Data")] [PreviewField(100)] [HideLabel]
		public GameObject mapModel;
	}
}