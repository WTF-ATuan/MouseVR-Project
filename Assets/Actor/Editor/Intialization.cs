using PhilippeFile.Script;
using Puzzle.GameLogic.Scripts;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

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


		protected override void OnEnable(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			screenEffect = FindObjectOfType<ScreenEffect>();
			behavioraYEditor = FindObjectOfType<BehavioralEnvironmentY>();
		}

		protected override void OnGUI(){
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.EndHorizontal();
			if(actor || Application.isEditor && Application.isPlaying){
				DrawMethodButton();
			}
		}

		private void DrawMethodButton(){
			DashboardUpPos();
			DashboardDownPos();
		}

		private void DashboardDownPos(){ }

		public TeleportPoint tp;
		public SceneObject scene;
		public BehavioralEnvironmentY behavioraYEditor;

		public string screenState = "Disconnect";

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
			tp = (TeleportPoint)EditorGUILayout.EnumPopup("Teleport", tp);

			if (GUILayout.Button("Teleport"))
			{
				if (tp == TeleportPoint.Left)
				{
					behavioraYEditor.SetRightSide();
					actor.ResetActor();
				}
				else
				{
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