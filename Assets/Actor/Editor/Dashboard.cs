using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using PhilippeFile.Script;
using Project;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Editor
{
	public class Dashboard : OdinEditorWindow
	{
		[MenuItem("Tools/Project/Dashboard")]
		private static void OpenWindow()
		{
			var window = GetWindow<Dashboard>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader = new ArduinoDataReader();

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
			
			EditorGUILayout.LabelField("Task Info");
			
			if(GUILayout.Button("Refresh"))
			{
				actor = FindObjectOfType<Scripts.Actor>();
				settingPanel = FindObjectOfType<SettingPanel>();
				arduinoBasic = FindObjectOfType<ArduinoBasic>();
			}

			EditorGUILayout.EndHorizontal();
			if (actor || Application.isEditor && Application.isPlaying)
			{
				DrawMethodButton();
			}
		}

		private void DrawMethodButton()
		{
			DashboardUpPos();
			DashboardDownPos();
		}

		private void DashboardDownPos()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Position Info");
			EditorGUILayout.EndHorizontal();
			
			DashLine();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Distance : " + actor.GetDistance().ToString("0.00"));
			EditorGUILayout.LabelField("Speed : " + actor.GetSpeed());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Lick : " + settingPanel.GetLickCount());
			EditorGUILayout.LabelField("Press : " + settingPanel.GetSuccessCount());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Choose L : " + settingPanel.GetChooseLeft());
			EditorGUILayout.LabelField("Choose R : " + settingPanel.GetChooseRight());
			EditorGUILayout.EndHorizontal();
		}

		private void DashboardUpPos()
		{
			DashLine();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Trial num : " + (settingPanel.GetFallCount() + settingPanel.GetSuccessCount()));
			EditorGUILayout.LabelField("Reward position : " + settingPanel.GetRewardDistance());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("");
			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Success : " + settingPanel.GetRewardCount());
			EditorGUILayout.LabelField("Reward Size : " + settingPanel.GetRewardSize());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("");
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Stop : " + settingPanel.GetFallCount());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Miss : " + settingPanel.GetFallCount());
			EditorGUILayout.LabelField("Time of Recording : " + GetPlayTime());
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Manual Reward : " + settingPanel.GetManualReward());
			EditorGUILayout.EndHorizontal();
			
			

			DashLine();
		}

		private void DashLine()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------");
			EditorGUILayout.EndHorizontal();
		}

		private string GetPlayTime()
		{
			if (Application.isPlaying)
			{
				return Time.realtimeSinceStartup.ToString("0.0");
			}
			else
			{
				return "0";
			}
		}
		
		
	}
}
