using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using PhilippeFile.Script;
using Project;
using Puzzle.GameLogic.Scripts;
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

		[ReadOnly] [FoldoutGroup("Task")]  [LabelText("Trail Number :")] public int trialNum;
		[ReadOnly] [FoldoutGroup("Task")] [LabelText("Reward position : ")] public float rewardPosition;
		
		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("Success : ")] public int success;
		[ReadOnly] [FoldoutGroup("Task")] [LabelText("Reward size (valve open T): : ")] public float rewardSize;
		
		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("Failure : ")] public int stop;
		
		[ReadOnly] [FoldoutGroup("Trial")] public string timeOfRecording;
		
		[ReadOnly] [FoldoutGroup("Trial")] public int manualReward;
		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Distance : ")] public string distance;
		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("VR Speed : ")] public string vrSpeed;
		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Treadmill Speed : ")] public string treadmillSpeed;
		
		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Lick : ")] public int lick;
		[ReadOnly] [FoldoutGroup("Trial")] public int press;
		
		[ReadOnly] [FoldoutGroup("Trial")]  [LabelText("ChooseL : ")] public int chooseL;
		[ReadOnly] [FoldoutGroup("Trial")]  [LabelText("ChooseR : ")] public int chooseR;

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
			vrSpeed = actor.GetSpeed().ToString("0");
			treadmillSpeed = arduinoBasic.GetSpeed().ToString("0.00");
			
			lick = settingPanel.GetLickCount();
			press = settingPanel.GetSuccessCount();

			chooseL = settingPanel.GetChooseLeft();
			chooseR = settingPanel.GetChooseRight();

			trialNum = (settingPanel.GetFallCount() + settingPanel.GetSuccessCount());


			if (rewardPosition < actor.GetDistance())
			{
				rewardPosition = actor.GetDistance();
			}
			
			success = settingPanel.GetRewardCount();
			rewardSize = arduinoBasic.GetRewardLimit();

			stop = settingPanel.GetFallCount();

			timeOfRecording = FormatTime(GetPlayTime());
			manualReward = settingPanel.GetManualReward();
			Repaint();
			
			
			
			/*
			EditorGUILayout.EndHorizontal();
			if (actor || Application.isEditor && Application.isPlaying)
			{
				
			}
			
			DrawMethodButton();
			*/
			base.OnGUI();
		}

		[Button]
		private void Refresh()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		private void Update()
		{
			if (Event.current != null)
			{
				actor = FindObjectOfType<Scripts.Actor>();
				settingPanel = FindObjectOfType<SettingPanel>();
				arduinoBasic = FindObjectOfType<ArduinoBasic>();

				distance = actor.GetDistance().ToString("0.00");
				vrSpeed = actor.GetSpeed().ToString("0");
				treadmillSpeed = arduinoBasic.GetSpeed().ToString("0.00");
			
				lick = settingPanel.GetLickCount();
				press = settingPanel.GetSuccessCount();

				chooseL = settingPanel.GetChooseLeft();
				chooseR = settingPanel.GetChooseRight();

				trialNum = (settingPanel.GetFallCount() + settingPanel.GetSuccessCount());
				rewardPosition = settingPanel.GetRewardDistance();

				success = settingPanel.GetRewardCount();
				rewardSize = arduinoBasic.GetRewardLimit();

				stop = settingPanel.GetFallCount();

				timeOfRecording = FormatTime(GetPlayTime());
				manualReward = settingPanel.GetManualReward();

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

		private float GetPlayTime()
		{
			if (Application.isPlaying)
			{
				return Time.realtimeSinceStartup;
			}
			else
			{
				return 0f;
			}
		}
		
		public string FormatTime( float time )
		{
			System.TimeSpan calc = System.TimeSpan.FromSeconds(time);
			return string.Format("{0:00}:{1:00}:{2:00}" , calc.Hours , calc.Minutes, calc.Seconds);
		}
		
		
	}
}
