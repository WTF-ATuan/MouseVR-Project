using System.Collections;
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

		[ReadOnly] [HorizontalGroup("Trail")] [LabelText("Trail Number :")] public int trialNum;
		[ReadOnly] [HorizontalGroup("Trail")] [LabelText("Reward position : ")] public float rewardPosition;
		
		[ReadOnly] [HorizontalGroup("Success")] [LabelText("Success : ")] public int success;
		[ReadOnly] [HorizontalGroup("Success")] [LabelText("Reward Size : ")] public string rewardSize;
		
		[ReadOnly] [HorizontalGroup("Stop")] [LabelText("Stop : ")] public int stop;
		
		[ReadOnly] [HorizontalGroup("Miss")] [LabelText("Miss : ")] public int miss;
		[ReadOnly] [HorizontalGroup("Miss")] [LabelText("Time of Recording : ")] public string timeOfRecording;
		
		[ReadOnly] [HorizontalGroup("Stop")] [LabelText("Manual Reward : ")] public int manualReward;
		[ReadOnly] [HorizontalGroup("Distance")] [LabelText("Distance : ")] public string distance;
		[ReadOnly] [HorizontalGroup("Distance")] [LabelText("Speed : ")] public string speed;
		
		[ReadOnly] [HorizontalGroup("Lick")] [LabelText("Lick : ")] public int lick;
		[ReadOnly] [HorizontalGroup("Lick")] [LabelText("Press : ")] public int press;
		
		[ReadOnly] [HorizontalGroup("Choose")] [LabelText("ChooseL : ")] public int chooseL;
		[ReadOnly] [HorizontalGroup("Choose")] [LabelText("ChooseR : ")] public int chooseR;

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
			
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			distance = actor.GetDistance().ToString("0.00");
			speed = actor.GetSpeed().ToString("0");
			
			lick = settingPanel.GetLickCount();
			press = settingPanel.GetSuccessCount();

			chooseL = settingPanel.GetChooseLeft();
			chooseR = settingPanel.GetChooseRight();

			trialNum = (settingPanel.GetFallCount() + settingPanel.GetSuccessCount());
			rewardPosition = settingPanel.GetRewardDistance();

			success = settingPanel.GetRewardCount();
			rewardSize = settingPanel.GetRewardSize();

			stop = settingPanel.GetFallCount();
			miss = settingPanel.GetFallCount();

			timeOfRecording = GetPlayTime();
			manualReward = settingPanel.GetManualReward();
			
			
			if(GUILayout.Button("Refresh"))
			{
				actor = FindObjectOfType<Scripts.Actor>();
				settingPanel = FindObjectOfType<SettingPanel>();
				arduinoBasic = FindObjectOfType<ArduinoBasic>();
			}
			/*
			EditorGUILayout.EndHorizontal();
			if (actor || Application.isEditor && Application.isPlaying)
			{
				
			}
			
			DrawMethodButton();
			*/
			base.OnGUI();
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
