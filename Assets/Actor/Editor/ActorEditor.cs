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
			ActorSelectDirection();
			ActorReceiveReward();
		}

		private int directionIndex;
		private void ActorSelectDirection(){
			EditorGUILayout.BeginVertical(LayoutSettings.editWidth);
			directionIndex = EditorGUILayout.IntField("0 is Left 1 is Right", directionIndex);
			var isSelect = GUILayout.Button("Select Direction", LayoutSettings.editWidth);
			if(isSelect){
				var isRight = directionIndex == 1;
				actor.SelectDirection(isRight);
			}

			EditorGUILayout.EndVertical();
		}

		private Vector3 teleportPosition;
		private void ActorTeleport(){
			EditorGUILayout.BeginVertical(LayoutSettings.editWidth);
			teleportPosition = EditorGUILayout.Vector3Field("Position", teleportPosition);
			var isTeleport = GUILayout.Button("Teleport", LayoutSettings.editWidth);
			if(isTeleport){
				actor.Teleport(teleportPosition);
			}

			EditorGUILayout.EndVertical();
		}

		private string reward;
		private void ActorReceiveReward(){
			EditorGUILayout.BeginVertical(LayoutSettings.editWidth);
			reward = EditorGUILayout.TextField("Reward Message", reward);
			var isReceived = GUILayout.Button("Receive Reward", LayoutSettings.editWidth);
			if(isReceived){
				actor.ReceiveReward(reward);
			}

			EditorGUILayout.EndVertical();
		}
	}
}
#endif