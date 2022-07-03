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

		private bool isLick;


		[TitleGroup("Other Setting")] [LabelText("Lick")] [ReadOnly] public string lickString;
		[TitleGroup("Other Setting")] [LabelText("Random reward at reward zone")] [ReadOnly] public string RandomRewardAtRewardZone;
		[TitleGroup("Other Setting")] [LabelText("Random reward at check zone")] [ReadOnly] public string RandomRewardAtCheckZone;
		[TitleGroup("Other Setting")] [LabelText("Reward probability")] [Range(0 , 100)] public float rewardProbability;
		
		
		[TitleGroup("Reward Setting")] [LabelText("Maze position range")] [ReadOnly] public string mazePositionRange;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Position")] [ReadOnly] public string rewardZonePosition;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Size")] [ReadOnly] public string rewardZoneSize;
		[TitleGroup("Reward Setting")] [LabelText("Reward check zone periodiocity")] [ReadOnly] public string rewardCheckZonePeriodiocity;
		[TitleGroup("Reward Setting")] [LabelText("Reward valve duration (ms)")] [ReadOnly] public string rewardValveDuration;

		
		

		protected override void OnEnable()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			lickString = "";
		}

		protected override void OnGUI()
		{
			
			if (actor || Application.isEditor && Application.isPlaying)
			{
				//DrawMethodButton();
			}
			
			base.OnGUI();
		}

		[Button]
		public void GetReward()
		{
			settingPanel.GetReward();
		}
		
		[Button]
		public void SwitchLickOption()
		{
			isLick = !isLick;
			
			if (isLick)
			{
				
			}
			else
			{
				
			}
				
			
		}

		private void DrawMethodButton()
		{
			DashboardUpPos();
			DashboardDownPos();
		}
		


		private void DashboardDownPos()
		{
			
		}

		private bool isLickOption , randomRewardAtRewardZone , randomRewardAtCheckZone;
		private string startRange, endRange , checkZonePeriodiocity , rewardPeriodiocity , rewardDuration;

		private void DashboardUpPos()
		{
			EditorGUILayout.BeginVertical();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Give Reward");
			
			if (GUILayout.Button("Deliver Reward"))
			{
				
			}
			
			EditorGUILayout.EndHorizontal();
			
			
			
			EditorGUILayout.LabelField("Lick Option");
			isLickOption = EditorGUILayout.Toggle("Lick Option", isLickOption);
			
			if (GUILayout.Button("Check"))
			{
				if (isLickOption)
				{
				
				
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Maze Position Range");
			startRange = EditorGUILayout.TextField("Start", startRange);
			endRange = EditorGUILayout.TextField("End", endRange);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Reward Zone Position");
			startRange = EditorGUILayout.TextField("Start", startRange);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Reward Zone Size");
			startRange = EditorGUILayout.TextField("Size", startRange);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			randomRewardAtRewardZone = EditorGUILayout.Toggle("Random Reward at Reward Zone", randomRewardAtRewardZone);
			randomRewardAtCheckZone = EditorGUILayout.Toggle("Random Reward at Check Zone", randomRewardAtCheckZone);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Check"))
			{
				if (randomRewardAtRewardZone)
				{
				
				
				}

				if (randomRewardAtCheckZone)
				{
					
				}
			}

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
