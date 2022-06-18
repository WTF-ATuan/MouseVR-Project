﻿using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using PhilippeFile.Script;
using Project;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum TeleportPoint{
	CUBE = 0,
	SPHERE = 1,
	PLANE = 2
}

namespace Actor.Editor{
	public class Intialization : OdinEditorWindow{
		[MenuItem("Tools/Project/Intialization")]
		private static void OpenWindow(){
			var window = GetWindow<Intialization>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;


		protected override void OnEnable(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		protected override void OnGUI(){
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.EndHorizontal();
			if(actor || Application.isEditor && Application.isPlaying){
				DrawMethodButton();
			}
		}

		private void DrawMethodButton(){
			DashboardUpPos();
			DashboardDownPos();
		}

		private void DashboardDownPos(){ }

		public TeleportPoint tp;
		public SceneObject scene;

		private void DashboardUpPos(){
			EditorGUILayout.BeginVertical();
			var serializedObject = new SerializedObject(this);
			var serializedProperty = serializedObject.FindProperty("scene");
			EditorGUILayout.PropertyField(serializedProperty, GUILayout.Width(400));
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginHorizontal();
			tp = (TeleportPoint)EditorGUILayout.EnumPopup("Teleport", tp);

			if(GUILayout.Button("Teleport")){ }

			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Arduino Connect : " + arduinoBasic.connectAction); //Blank display

			if(GUILayout.Button("Change")){ }

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Blank display : " + arduinoBasic.connectAction); //Blank display

			if(GUILayout.Button("Change")){ }

			EditorGUILayout.EndHorizontal();
		}

		private void DashLine(){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------");
			EditorGUILayout.EndHorizontal();
		}
	}
}