#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Actor.Editor{
	public class ActorEditor : OdinEditorWindow{
		private static void OpenWindow(){
			var window = GetWindow<ActorEditor>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.Show();
		}

		private Scripts.Actor actor;

		protected override void OnEnable(){
			actor = FindObjectOfType<Scripts.Actor>();
		}

		protected override void OnGUI(){
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Actor");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.ObjectField(actor, typeof(Scripts.Actor), true);
			
			if(GUILayout.Button("Refresh")){
				actor = FindObjectOfType<Scripts.Actor>();
			}

			EditorGUILayout.EndHorizontal();
			if(actor || Application.isEditor && Application.isPlaying){
				DrawMethodButton();
			}
		}

		private void DrawMethodButton(){
			ActorTeleport();
			ActorSelectDirection();
			ActorReceiveReward();
			RewardArea();
		}

		private int directionIndex;

		private void ActorSelectDirection(){
			EditorGUILayout.BeginVertical();
			directionIndex = EditorGUILayout.IntField("0 is Left 1 is Right", directionIndex);
			var isSelect = GUILayout.Button("Select Direction");
			if(isSelect){
				var isRight = directionIndex == 1;
				actor.SelectDirection(isRight);
			}

			EditorGUILayout.EndVertical();
		}

		private Vector3 teleportPosition;

		private void ActorTeleport(){
			EditorGUILayout.BeginVertical();
			teleportPosition = EditorGUILayout.Vector3Field("Position", teleportPosition);
			var isTeleport = GUILayout.Button("Teleport");
			if(isTeleport){
				actor.Teleport(teleportPosition);
			}

			EditorGUILayout.EndVertical();
		}

		private string reward;

		private void ActorReceiveReward(){
			EditorGUILayout.BeginVertical();
			reward = EditorGUILayout.TextField("Reward Message", reward);
			var isReceived = GUILayout.Button("Receive Reward");
			EditorGUILayout.EndVertical();
		}
		
		static string selectedLayer;
		
		private void RewardArea()
		{
			EditorGUILayout.BeginVertical();
			selectedLayer = EditorGUILayout.TextField("Object Name: ", Selection.activeGameObject.name);
			var isOpen = GUILayout.Button("Open/Close Reward Area");
			EditorGUILayout.EndVertical();

		}
	}
}
#endif