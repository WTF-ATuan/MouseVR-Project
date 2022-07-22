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

		[ReadOnly] [InfoBox("Connect VR: Hot Key 'C'")] [VerticalGroup("Connect")]
		public string arduinoConnect = "Disconnect";

		[ReadOnly] [InfoBox("Blank display: Hot Key 'B'")] [VerticalGroup("Connect")]
		public string screenConnect = "Disconnect";
		
		[HorizontalGroup("Position")] [LabelText("Teleport target X")] public float x;
		[HorizontalGroup("Position")] [LabelText("Teleport target Z")] public float z;



		private BehavioralEnvironmentY behavioraYEditor;

		private string screenState = "Disable";



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
			base.OnGUI();
		}

		public TeleportPoint teleportMazePoint;

		[Button]
		public void Teleport(){
			if(teleportMazePoint == TeleportPoint.Left){
				behavioraYEditor?.SetLeftSide();
				actor.ResetActor();
			}
			else{
				behavioraYEditor?.SetRightSide();
				actor.ResetActor();
			}
		}
		
		[Button]
		public void TeleportOnTargetPoint(){
			actor.Teleport(new Vector3(x, actor.transform.position.y, z));
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


		
	}                          
}