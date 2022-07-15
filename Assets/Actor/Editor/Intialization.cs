using System;
using PhilippeFile.Script;
using Puzzle.GameLogic.Scripts;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TeleportPoint{
	Left,
	Right,
}

namespace Actor.Editor{
	public class Intialization : OdinEditorWindow{
		[MenuItem("Tools/Project/Intialization")]
		private static void OpenWindow(){
			var window = GetWindow<Intialization>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;
		private ScreenEffect screenEffect;

		[ReadOnly] public string sceneName;

		[FilePath] [OnValueChanged("ChangeScene")]
		public string sceneFilePath;

		[ReadOnly] [InfoBox("Hot Key 'C'")] [VerticalGroup("Connect")]
		public string arduinoConnect = "Disconnect";

		[ReadOnly] [InfoBox("Hot Key 'B'")] [VerticalGroup("Connect")]
		public string screenConnect = "Disconnect";
		
		[HorizontalGroup("Position")] public float x , z;

		[VerticalGroup("Connect")] [OnValueChanged("OnChangeColor")]
		public Color blankColor;

		private BehavioralEnvironmentY behavioraYEditor;

		private string screenState = "Disable";

		private void OnChangeColor(){
			screenEffect.SetColor(blankColor);
		}

		private void ChangeScene(){
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene(sceneFilePath);
			sceneName = SceneManager.GetActiveScene().name;
		}


		protected override void OnEnable(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			screenEffect = FindObjectOfType<ScreenEffect>();
			behavioraYEditor = FindObjectOfType<BehavioralEnvironmentY>();

			arduinoConnect = "Disconnect";
			screenConnect = "Disable";
			sceneName = SceneManager.GetActiveScene().name;
		}

		protected override void OnGUI(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			screenEffect = FindObjectOfType<ScreenEffect>();
			behavioraYEditor = FindObjectOfType<BehavioralEnvironmentY>();
			if(!arduinoBasic || !screenEffect){
				base.OnGUI();
				return;
			}

			arduinoConnect = arduinoBasic.connectAction;
			screenConnect = screenState;
			/*
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.EndHorizontal();
			if(actor || Application.isEditor && Application.isPlaying){
				DrawMethodButton();
			}
			*/

			base.OnGUI();
		}

		private void TargetColorChanged(){ }

		private void DrawMethodButton(){
			DashboardUpPos();
			DashboardDownPos();
		}

		private void DashboardDownPos(){ }

		public TeleportPoint teleportMazePoint;

		[Button]
		public void Teleport(){
			if(teleportMazePoint == TeleportPoint.Left){
				behavioraYEditor?.SetRightSide();
				actor.ResetActor();
			}
			else{
				behavioraYEditor?.SetRightSide();
				actor.ResetActor();
			}
		}

		[Button][LabelText("Blank Display")]
		public void ChangeBlankScreen(){
			screenEffect.ChangeScreenBlank();

			screenState = screenEffect.GetState();
		}

		[Button]
		public void GetReward(){
			settingPanel.GetReward();
		}

		[Button]
		public void TeleportOnTargetPoint(){
			actor.Teleport(new Vector3(x, actor.transform.position.y, z));
		}


		private void DashboardUpPos(){
			EditorGUILayout.BeginVertical();
			var serializedObject = new SerializedObject(this);
			var serializedProperty = serializedObject.FindProperty("scene");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedProperty, GUILayout.Width(400));
			serializedObject.ApplyModifiedProperties();
			if(GUILayout.Button("Load")){
				// EditorSceneManager.OpenScene(scene.ToString());
			}

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			teleportMazePoint = (TeleportPoint)EditorGUILayout.EnumPopup("Teleport", teleportMazePoint);

			if(GUILayout.Button("Teleport")){
				if(teleportMazePoint == TeleportPoint.Left){
					behavioraYEditor.SetRightSide();
					actor.ResetActor();
				}
				else{
					behavioraYEditor.SetRightSide();
					actor.ResetActor();
				}
			}

			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Arduino Connect : " + arduinoBasic.connectAction + "(hot key C)");

			if(GUILayout.Button("Change")){ }

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Blank display : " + screenState + "(hot key B)"); //Blank display

			if(GUILayout.Button("Change")){
				screenEffect.ChangeScreenBlank();

				screenState = screenEffect.GetState();
			}

			EditorGUILayout.EndHorizontal();
		}

		private void DashLine(){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------------------------------------");
			EditorGUILayout.LabelField("----------");
			EditorGUILayout.EndHorizontal();
		}
	}
}