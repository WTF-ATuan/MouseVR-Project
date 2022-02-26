using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Scripts{
	[CreateAssetMenu(fileName = "EnvironmentData", menuName = "Project/ EnvironmentData")]
	public class EnvironmentData : ScriptableObject{
		[BoxGroup("Basic Info")] [LabelWidth(100)]
		public new string name;

		[BoxGroup("Basic Info")] [LabelWidth(100)] [TextArea]
		public string description;

		[HorizontalGroup("Map Data", 100)] [PreviewField(100)] [HideLabel]
		public GameObject mapModel;

		[VerticalGroup("Map Data/MapSetting")] [LabelWidth(100)]
		public List<Characteristic> characteristics;

		[VerticalGroup("Map Data/MapSetting")] [LabelWidth(100)]
		public List<Path> paths;

		[VerticalGroup("Map Data/MapSetting")] [LabelWidth(100)]
		public List<Reward> rewards;

		[VerticalGroup("Map Data/MapSetting")] [LabelWidth(100)] [Range(0, 100)] [GUIColor(100, 100, 100)]
		public int brightNess;

		[VerticalGroup("Map Data/MapSetting")] [LabelWidth(100)] [Range(0, 100)] [GUIColor(100, 100, 100)]
		public int contrast;
	}
}