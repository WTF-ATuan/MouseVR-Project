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
	public class EnvironmentCue : OdinEditorWindow
	{
		[MenuItem("Tools/Project/EnvironmentCue")]
		private static void OpenWindow()
		{
			var window = GetWindow<EnvironmentCue>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;

		private SerializedObject serializedObject;
		
		[SerializeField]
		private List<Material> currentMaterials = new List<Material>();

		private SerializedProperty currentMaterialSerializedObject;
		
		[SerializeField]
		private List<Material> switchtMaterials = new List<Material>();
		
		private SerializedProperty switchtMaterialsSerializedObject;

		protected override void OnEnable()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			serializedObject = new SerializedObject(this);

			currentMaterialSerializedObject = serializedObject.FindProperty("currentMaterials");
			switchtMaterialsSerializedObject = serializedObject.FindProperty("switchtMaterials");
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
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(currentMaterialSerializedObject, true);
			EditorGUILayout.PropertyField(switchtMaterialsSerializedObject, true);
			EditorGUILayout.EndHorizontal();
			
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("");
			if (GUILayout.Button("Switch"))
			{
				
			}
			EditorGUILayout.EndHorizontal();
		}

		private float worldbrightness;
		private float worldContrast;
		private float AllCueContrast;
		private float visibility;
		
		private void DashboardUpPos()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("World brightness");
			worldbrightness = EditorGUILayout.Slider(worldbrightness, 1, 100);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("World contrast");
			worldContrast = EditorGUILayout.Slider(worldContrast, 1, 100);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("All-cue contrast");
			AllCueContrast = EditorGUILayout.Slider(AllCueContrast, 1, 100);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Visibility");
			visibility = EditorGUILayout.Slider(visibility, 1, 100);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			
			

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
