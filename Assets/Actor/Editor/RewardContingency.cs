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
			
		}

		private bool isLickOption , randomRewardAtRewardZone , randomRewardAtCheckZone;
		private string startRange, endRange , checkZonePeriodiocity , rewardPeriodiocity , rewardDuration;
		private float rewardProbability;

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
			EditorGUILayout.EndHorizontal();
			
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Reward Check Zone Periodiocity");
			startRange = EditorGUILayout.TextField("Size", startRange);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Reward Probability (%)");
			rewardProbability = EditorGUILayout.Slider(rewardProbability, 1f, 100f);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Reward Value Duration(ms)");
			startRange = EditorGUILayout.TextField("Time", startRange);
			EditorGUILayout.EndHorizontal();
			
			
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
