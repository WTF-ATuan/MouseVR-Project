using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Actor.Editor{
	public class VisualAvailabilityEditor : OdinEditorWindow{
		[MenuItem("Tools/Project/Visual-Availability")]
		private static void OpenWindow(){
			var window = GetWindow<VisualAvailabilityEditor>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.Show();
		}

		[Required] public List<MeshRenderer> backGroundMesh;
		[Required] public List<MeshRenderer> allCueMesh;
		[BoxGroup] public VisualAvailability visualAvailability = new VisualAvailability();

		protected override void OnEnable(){
			Refresh();
		}

		[Button]
		private void Refresh(){
			var foundCamera = FindObjectsOfType<Camera>().ToList();
			var foundLight = FindObjectsOfType<Light>().ToList();
			visualAvailability.Setup(foundCamera, foundLight, backGroundMesh, allCueMesh);
		}
	}

	[Serializable]
	public class VisualAvailability{
		[Range(0, 1)] [SerializeField] [OnValueChanged("LightValueChanged")]
		private float lightSourceBrightness;

		[Range(0, 1)] [SerializeField] [OnValueChanged("SkyBrightValueChanged")]
		private float skyBrightness;

		[Range(0, 1)] [SerializeField] [OnValueChanged("MazeBackGroundValueChanged")]
		private float mazeBackgroundBrightness;

		[Range(0, 1)] [SerializeField] [OnValueChanged("AllCueBrightValueChanged")]
		private float allCueBrightness;

		[SerializeField] [OnValueChanged("VisibilityValueChanged")]
		private float visibility;

		[FoldoutGroup("Reference Object")] public List<Camera> allCamera;
		[FoldoutGroup("Reference Object")] public List<Light> allLight;
		[FoldoutGroup("Reference Object")] public List<MeshRenderer> backGroundMesh;
		[FoldoutGroup("Reference Object")] public List<MeshRenderer> allCueMesh;

		public void Setup(List<Camera> cameras, List<Light> lights, List<MeshRenderer> groundMesh,
			List<MeshRenderer> cueMesh){
			allCamera = cameras;
			allLight = lights;
			backGroundMesh = groundMesh;
		}

		private void LightValueChanged(){
			foreach(var light in allLight){
				var nextColor = new Color(lightSourceBrightness, lightSourceBrightness, lightSourceBrightness);
				light.color = nextColor;
			}
		}

		private void SkyBrightValueChanged(){
			foreach(var camera in allCamera){
				var nextColor = new Color(skyBrightness, skyBrightness, skyBrightness);
				camera.backgroundColor = nextColor;
			}
		}

		private void MazeBackGroundValueChanged(){
			foreach(var mesh in backGroundMesh){
				var nextColor = new Color(mazeBackgroundBrightness, mazeBackgroundBrightness, mazeBackgroundBrightness);
				mesh.materials.ForEach(x => x.color = nextColor);
			}
		}

		private void AllCueBrightValueChanged(){
			foreach(var mesh in allCueMesh){
				var nextColor = new Color(allCueBrightness, allCueBrightness, allCueBrightness);
				mesh.materials.ForEach(x => x.color = nextColor);
			}
		}

		private void VisibilityValueChanged(){
			foreach(var camera in allCamera){
				camera.farClipPlane = visibility;
			}
		}
	}
}