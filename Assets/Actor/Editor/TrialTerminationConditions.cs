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
		
	}
}
