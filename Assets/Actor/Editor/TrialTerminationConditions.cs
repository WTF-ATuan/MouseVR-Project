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
	public class TrialTerminationConditions : OdinEditorWindow
	{
		[MenuItem("Tools/Project/TrialTerminationConditions")]
		private static void OpenWindow()
		{
			var window = GetWindow<TrialTerminationConditions>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;

		private SerializedObject serializedObject;
		
		private LickTrigger[] lickTrigger;
		
		[TitleGroup("Only In Line Maze")] [LabelText("Current Lick Count Limit")] [OnValueChanged("OnCurrentLickCountLimit")] public int currentLickCountLimit;
		[TitleGroup("Only In Line Maze")] [LabelText("Wrong Lick Count Limit")] [OnValueChanged("OnWrongLickCountLimit")] public int wrongLickCountLimit;
		[TitleGroup("Only In Line Maze")] [LabelText("Lick Count Limit")] [OnValueChanged("OnWrongLickCountLimit")] public int lickCountLimit;
		[TitleGroup("Only In Line Maze")] [LabelText("Definition of immobility")] [OnValueChanged("OnWrongLickCountLimit")] public int definitionOImmobility;
		[TitleGroup("Only In Line Maze")] [LabelText("Terminate the trial when immobile")] [OnValueChanged("OnWrongLickCountLimit")] public int terminateTheTrialWhenImmobile;

		

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
				
			}
			
			base.OnGUI();
		}

		private void DrawMethodButton()
		{
			DashboardUpPos();
			DashboardDownPos();
		}
		


		private void DashboardDownPos()
		{
			
		}

		private int lickLimit, speed, time;

		private void DashboardUpPos()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Lick Limit");
			lickLimit = EditorGUILayout.IntField("Limit", lickLimit);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Definition of immobility: < X cm/s");
			speed = EditorGUILayout.IntField("Speed", speed);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Terminate the trial when immobile");
			time = EditorGUILayout.IntField("Time", time);
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
		
		
		private void OnCurrentLickCountLimit()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetCorrectLickCountLimit(currentLickCountLimit);
			}
		}
		
		private void OnWrongLickCountLimit()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetCorrectLickCountLimit(wrongLickCountLimit);
			}
		}
		
	}
}
