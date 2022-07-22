using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Linq;
using Actor.Scripts.EventMessage;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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


		[Title("Save Scene")] [HorizontalGroup("Save")] [SerializeField] [FolderPath] [HideLabel]
		private string scenePath;

		[Title("Save Scene")] [HorizontalGroup("Save")] [SerializeField] [HideLabel]
		private string sceneName;

		[Title("Save BehaviorData")]
		[HorizontalGroup("Data")]
		[SerializeField]
		[FolderPath]
		[HideLabel]
		[PropertyOrder(2)]
		private string dataPath;

		[Title("Save BehaviorData")]
		[HorizontalGroup("Data")]
		[SerializeField]
		[HideLabel]
		[PropertyOrder(2)]
		[InfoBox("Path is already Exist", InfoMessageType.Error, "CheckDataPath")]
		private string dataName;

		[Title("Behavioral data write-in interval (s)")]
		[HorizontalGroup("Data")]
		[SerializeField]
		[PropertyOrder(2)]
		[HideLabel]
		//[ProgressBar(0.05f, 0.3f)]
		private float during = 0.016666666666666f;

		[Button(ButtonSizes.Medium)]
		[PropertyOrder(1)]
		private void SaveToNewScene(){
			var currentScene = SceneManager.GetActiveScene();
			var path = scenePath + "/" + sceneName + ".unity";
			EditorSceneManager.SaveScene(currentScene, path, true);
		}

		[Button(ButtonSizes.Medium)]
		[PropertyOrder(2)]
		private void SetDirectEventMessage(){
			if(CheckDataPath()) return;
			eventMessageHandler.path = dataPath;
			eventMessageHandler.dataName = dataName;
			eventMessageHandler.writeDuring = during;
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

		[FoldoutGroup("Reference Object")] [SerializeField] [PropertyOrder(10)]
		private EventMessageHandler eventMessageHandler;

		protected override void OnEnable(){
			Refresh();
		}

		[Button(ButtonStyle.Box)]
		[PropertyOrder(20)]
		private void Refresh(){
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			settingPanel = FindObjectOfType<SettingPanel>();
			eventMessageHandler = FindObjectOfType<EventMessageHandler>();
		}

		private bool CheckReference(){
			return !arduinoBasic || !settingPanel;
		}

		private void OnTrialsEndNumber(){
			settingPanel.SettingReward(numberOfTrialsToEndTheSession);
		}

		private void OnArduinoPortValueChanged(){
			if(arduinoPort.IsNullOrWhitespace()) return;
			arduinoBasic.SetPortCOM(arduinoPort);
		}

		private IEnumerable GetAllPort(){
			var portNames = SerialPort.GetPortNames().ToList();
			portNames.Insert(0, "Not Connect");
			var valueDropdownItems = portNames.Select(x => new ValueDropdownItem(x, x));
			return valueDropdownItems;
		}

		private bool CheckDataPath(){
			var path = dataPath + "/" + dataName + ".json";
			return !eventMessageHandler || File.Exists(path);
		}
	}
}