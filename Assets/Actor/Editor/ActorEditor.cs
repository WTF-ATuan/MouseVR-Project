#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Actor.Editor{
	public class ActorEditor : OdinEditorWindow{
		[MenuItem("Tools/Project/ActorEditor")]
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
			EditorGUILayout.BeginVertical(LayoutSettings.mainBox.style);
			EditorGUILayout.LabelField("Actor", LayoutSettings.sectionLabel);
			EditorGUILayout.BeginHorizontal(LayoutSettings.editWidth);
			EditorGUILayout.ObjectField(actor, typeof(Scripts.Actor), true);
			if(GUILayout.Button("Refresh", LayoutSettings.buttonOp)){
				actor = FindObjectOfType<Scripts.Actor>();
			}

			EditorGUILayout.EndHorizontal();
			if(actor || Application.isEditor && Application.isPlaying){
				DrawMethodButton();
			}
		}

		private void DrawMethodButton(){
			ActorTeleport();
			ActorReceiveReward();
		}

		private void ActorReceiveReward(){
			EditorGUILayout.BeginVertical(LayoutSettings.editWidth);
			var reward = string.Empty;
			EditorGUILayout.TextField("Reward Message", reward);
			var isReceived = GUILayout.Button("Receive Reward", LayoutSettings.editWidth);
			if(isReceived){
				actor.ReceiveReward(reward);
			}

			EditorGUILayout.EndVertical();
		}

		private void ActorTeleport(){
			EditorGUILayout.BeginVertical(LayoutSettings.editWidth);
			var teleportPosition = Vector3.zero;
			EditorGUILayout.Vector3Field("Position", teleportPosition);
			var isTeleport = GUILayout.Button("Teleport", LayoutSettings.editWidth);
			if(isTeleport){
				actor.Teleport(teleportPosition);
			}

			EditorGUILayout.EndVertical();
		}
	}
}
#endif