using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Actor.Editor{
	public class CueSwapEditor : OdinEditorWindow{
		[MenuItem("Tools/Project/CueSwap")]
		private static void OpenWindow(){
			var window = GetWindow<CueSwapEditor>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.Show();
		}

		[Required] [OnValueChanged("TargetMeshChanged")]
		public MeshRenderer targetMesh;

		[HorizontalGroup("material")] public Material[] currentMaterials;
		[HorizontalGroup("material")] public Material[] targetMaterials;

		private void TargetMeshChanged(){
			if(!targetMesh) return;
			var targetMeshMaterials = (Material[])targetMesh.materials.Clone();
			currentMaterials = targetMeshMaterials;
		}

		[Button]
		[InfoBox("$GetMessage", InfoMessageType.Warning)]
		[HorizontalGroup("Swap")]
		private void Swap(){
			if(!Application.isPlaying){
				return;
			}

			var cloneTarget = (Material[])targetMaterials.Clone();
			targetMesh.materials = cloneTarget;
			targetMaterials = currentMaterials;
			currentMaterials = cloneTarget;
		}


		private string GetMessage(){
			if(!Application.isPlaying)
				return "Can,t Swap in Editor Mode Please Turn To Play Mode" + " ( Hot Key S )";
			else
				return "is Swapping" + " ( Hot Key S )";
		}
		
		[Button]
		private void ClearAll(){
			targetMesh = null;
			currentMaterials = Array.Empty<Material>();
			targetMaterials = Array.Empty<Material>();
		}

		protected override void OnGUI(){
			var current = Event.current;
			var currentType = current.type;
			if(currentType == EventType.KeyDown){
				if(current.keyCode == KeyCode.S){
					Swap();
				}
			}

			base.OnGUI();
		}
	}
}