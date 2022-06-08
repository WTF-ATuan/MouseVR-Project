using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Project;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Editor
{
	public class BehavioraYEditor : OdinEditorWindow
	{
		[MenuItem("Tools/Project/BehavioraYEditor")]
		private static void OpenWindow()
		{
			var window = GetWindow<BehavioraYEditor>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 300);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;

		protected override void OnEnable()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		protected override void OnGUI()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.ObjectField(actor, typeof(Scripts.Actor), true);
			
			if (GUILayout.Button("Refresh"))
			{
				actor = FindObjectOfType<Scripts.Actor>();
			}
			
			EditorGUILayout.ObjectField(settingPanel, typeof(SettingPanel), true);

			if (GUILayout.Button("Refresh"))
			{
				settingPanel = FindObjectOfType<SettingPanel>();
			}
			

			EditorGUILayout.EndHorizontal();
			if (actor || Application.isEditor && Application.isPlaying)
			{
				DrawMethodButton();
			}
		}

		private void DrawMethodButton()
		{
			SettingRewardValue();
			SettingArduinoPort();
		}

		private string com;

		private void SettingArduinoPort()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Set COM");
			com = EditorGUILayout.TextField("COM Port" , com);
			var isTeleport = GUILayout.Button("Set Value");
			
			if(isTeleport)
			{
				settingPanel.SettingReward(sliderValue);
			}

			EditorGUILayout.EndVertical();
		}

		private float sliderValue;
		
		private void SettingRewardValue()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("RewardValue");
			sliderValue = EditorGUILayout.Slider(sliderValue , 0 , 100);
			var isTeleport = GUILayout.Button("Set Value");
			
			if(isTeleport)
			{
				settingPanel.SettingReward(sliderValue);
			}

			EditorGUILayout.EndVertical();
		}
	}
}
