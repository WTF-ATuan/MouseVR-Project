using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Environment.Scripts;
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
	public class RewardContingency : OdinEditorWindow
	{
		[MenuItem("Tools/Project/RewardContingency")]
		private static void OpenWindow()
		{
			var window = GetWindow<RewardContingency>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;

		private SerializedObject serializedObject;

		private LickTrigger[] lickTrigger;

		private bool isLick;


		[TitleGroup("Reward Area Setting")] [LabelText("Random reward at reward zone")] [ReadOnly] public bool randomRewardAtRewardZone = true;
		[TitleGroup("Reward Area Setting")] [LabelText("Random reward at check zone")] [ReadOnly] public bool randomRewardAtCheckZone = false;
		[TitleGroup("Reward Area Setting")] [LabelText("Reward probability")] [Range(0 , 100)] [OnValueChanged("OnRewardProbabilityChanged")] public float rewardProbability;
		
		
		[TitleGroup("Reward Setting")] [LabelText("Maze position range")] [ReadOnly] public string mazePositionRange;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Position")] [ReadOnly] public string rewardZonePosition;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Size")] [ReadOnly] public string rewardZoneSize;
		[TitleGroup("Reward Setting")] [LabelText("Reward check zone periodiocity")] [ReadOnly] public string rewardCheckZonePeriodiocity;
		[TitleGroup("Reward Setting")] [LabelText("Reward valve duration (ms)")] [OnValueChanged("OnRewardValveDurationChanged")] public float rewardValveDuration;
		
		[TitleGroup("Only In Line Maze")] [LabelText("Lick")] [OnValueChanged("OnLickChange")] public bool lick;
		[TitleGroup("Only In Line Maze")] [LabelText("Show Gizmos")] [OnValueChanged("OnShowGizmosChange")] public bool showGizmos;
		[TitleGroup("Only In Line Maze")] [LabelText("Current Lick Count Limit")] [OnValueChanged("OnCurrentLickCountLimit")] public int currentLickCountLimit;
		[TitleGroup("Only In Line Maze")] [LabelText("Wrong Lick Count Limit")] [OnValueChanged("OnWrongLickCountLimit")] public int wrongLickCountLimit;
		[TitleGroup("Reward Setting")] [LabelText("Reward Count Limit")] [OnValueChanged("OnRewardCountLimit")] public int rewardLimit;



		protected override void OnEnable()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		protected override void OnGUI()
		{
			
			if (actor || Application.isEditor && Application.isPlaying)
			{
				//DrawMethodButton();
			}
			
			base.OnGUI();
		}

		[Button][TitleGroup("Reward Setting")]
		public void GetReward()
		{
			settingPanel.GetReward();
		}

		[Button] [TitleGroup("Reward Area Setting")]
		public void SwitchAreaSetting()
		{
			randomRewardAtCheckZone = !randomRewardAtCheckZone;
			randomRewardAtRewardZone = !randomRewardAtRewardZone;
			
			var rewardZone = FindObjectsOfType<RewardArea>();

			foreach (var VARIABLE in rewardZone)
			{
				VARIABLE.GetComponent<BoxCollider>().enabled = randomRewardAtRewardZone;
			}
			
			foreach (var kLickTrigger in lickTrigger)
			{
				kLickTrigger.GetComponent<BoxCollider>().enabled = randomRewardAtCheckZone;
			}
		}
		
		[Button]
		public void Refresh()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		private void OnCurrentLickCountLimit()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetCorrectLickCountLimit(currentLickCountLimit);
			}
		}

		private void OnLickChange()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetSkipJudge(!this.lick);
			}
		}

		private void OnRewardValveDurationChanged()
		{
			arduinoBasic.SetLimitTime(rewardValveDuration);
		}
		

		private void OnWrongLickCountLimit()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetCorrectLickCountLimit(wrongLickCountLimit);
			}
		}

		private void OnShowGizmosChange()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetGizmos(showGizmos);
			}
		}

		private void OnRandomRewardAtRewardZoneChange()
		{
			
		}

		private void OnRandomRewardAtCheckZoneChange()
		{
			/*
			randomRewardAtCheckZone = !randomRewardAtCheckZone;
			randomRewardAtRewardZone = !randomRewardAtRewardZone;
			
			
			var rewardZone = FindObjectsOfType<RewardArea>();
			
			foreach (var VARIABLE in rewardZone)
			{
				VARIABLE.GetComponent<BoxCollider>().enabled = randomRewardAtRewardZone;
			}
			
			foreach (var kLickTrigger in lickTrigger)
			{
				kLickTrigger.GetComponent<BoxCollider>().enabled = randomRewardAtCheckZone;
			}
			*/
		}

		private void OnRewardCountLimit()
		{
			settingPanel.SetReward(rewardLimit);
		}

		private void Update()
		{
			lickTrigger = FindObjectsOfType<LickTrigger>();
		}

		private void OnRewardProbabilityChanged()
		{
			settingPanel.SettingReward(rewardProbability);
			Debug.Log("Change");
		}

		private void DrawMethodButton()
		{
			DashboardUpPos();
			DashboardDownPos();
		}
		


		private void DashboardDownPos()
		{
			
		}

		private void DashboardUpPos()
		{
			

		}

		private void DashLine()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------");
			EditorGUILayout.EndHorizontal();
		}
		
	}
}
