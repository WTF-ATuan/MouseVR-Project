using System.Collections;
using System.IO.Ports;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actor.Editor{
	public class ConfigurationOutput : OdinEditorWindow{
		[MenuItem("Tools/Project/Configuration and Output")]
		private static void OpenWindow(){
			var window = GetWindow<ConfigurationOutput>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.Show();
		}

		[InfoBox("Reference not found please check scene ", InfoMessageType.Error, "CheckReference")]
		[SerializeField]
		[ValueDropdown("GetAllPort")]
		[OnValueChanged("OnArduinoPortValueChanged")]
		private string arduinoPort;

		[FilePath]
		private string scenePath;

		[Title("Save Scene")] [HorizontalGroup("Save")] [SerializeField] [FolderPath] [HideLabel]
		private string path;

		[Title("Save Scene")] [HorizontalGroup("Save")] [SerializeField] [HideLabel]
		private string fileName;

		[Button(ButtonSizes.Medium)]
		private void SaveToNewScene(){
			var currentScene = SceneManager.GetActiveScene();
			var dataPath = path + "/" + fileName + ".unity";
			EditorSceneManager.SaveScene(currentScene, dataPath, true);
		}

		[SerializeField]
		[PropertyOrder(9)]
		[OnValueChanged("OnTrialsEndNumber")]
		[Space]
		[InfoBox("Reference not found please check scene ", InfoMessageType.Error, "CheckReference")]
		private int numberOfTrialsToEndTheSession;

		[FoldoutGroup("Reference Object")] [SerializeField] [PropertyOrder(10)]
		private ArduinoBasic arduinoBasic;

		[FoldoutGroup("Reference Object")] [SerializeField] [PropertyOrder(10)]
		private SettingPanel settingPanel;

		protected override void OnEnable(){
			Refresh();
		}

		[Button(ButtonStyle.Box)]
		[PropertyOrder(20)]
		private void Refresh(){
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			settingPanel = FindObjectOfType<SettingPanel>();
		}

		private bool CheckReference(){
			return !arduinoBasic || !settingPanel;
		}

		private void OnTrialsEndNumber(){
			settingPanel.SetRewardLimit(numberOfTrialsToEndTheSession);
		}

		private void OnArduinoPortValueChanged(){
			arduinoBasic.port = arduinoPort;
		}

		private IEnumerable GetAllPort(){
			var portNames = SerialPort.GetPortNames().ToList();
			portNames.Insert(0, "Not Connect");
			var valueDropdownItems = portNames.Select(x => new ValueDropdownItem(x, x));
			return valueDropdownItems;
		}

		private void OnScenePathChanged(){
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene(scenePath);
		}
	}
}