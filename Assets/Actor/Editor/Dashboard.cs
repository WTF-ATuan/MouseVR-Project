using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Actor.Scripts;
using Actor.Scripts.EventMessage;
using Environment.Scripts;
using PhilippeFile.Script;
using Project;
using Puzzle.GameLogic.Scripts;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Editor{
	public class Dashboard : OdinEditorWindow{
		[MenuItem("Tools/Project/Dashboard")]
		private static void OpenWindow(){
			var window = GetWindow<Dashboard>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		[ReadOnly] [FoldoutGroup("Task")] [LabelText("Trail Number :")]
		public int trialNum;

		[ReadOnly] [FoldoutGroup("Task")] [LabelText("Reward position : ")]
		public float rewardPosition;

		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("Success : ")]
		public int success;

		[ReadOnly] [FoldoutGroup("Task")] [LabelText("Reward size (valve open T): : ")]
		public float rewardSize;

		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("Failure : ")]
		public int stop;

		[ReadOnly] [FoldoutGroup("Trial")] public string timeOfRecording;

		[ReadOnly] [FoldoutGroup("Trial")] public int manualReward;

		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Distance : ")]
		public string distance;

		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("VR Speed : ")]
		public string vrSpeed;

		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Treadmill Speed : ")]
		public string treadmillSpeed;

		[ReadOnly] [FoldoutGroup("Behavior")] [LabelText("Lick : ")]
		public int lick;

		[ReadOnly] [FoldoutGroup("Trial")] public int press;

		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("ChooseL : ")]
		public int chooseL;

		[ReadOnly] [FoldoutGroup("Trial")] [LabelText("ChooseR : ")]
		public int chooseR;


		[FoldoutGroup("Reference Object")]
		[SerializeField]
		[InfoBox("Reference not found please check scene ", InfoMessageType.Error, "CheckReference")]
		private Scripts.Actor actor;

		[FoldoutGroup("Reference Object")] [SerializeField]
		private SettingPanel settingPanel;

		[FoldoutGroup("Reference Object")] [SerializeField]
		private ArduinoBasic arduinoBasic;

		[FoldoutGroup("Reference Object")] [SerializeField]
		private ScreenEffect screenEffect;

		[FoldoutGroup("Reference Object")] [SerializeField]
		private EventMessageHandler messageHandler;

		[FoldoutGroup("Reference Object")] [SerializeField]
		private RewardArea[] rewardArea;

		[FoldoutGroup("Line Maze")] [SerializeField]
		private ActorTimer actorTimer;

		[FoldoutGroup("Line Maze")] [SerializeField]
		private LickTrigger[] lickTrigger;

		[FoldoutGroup("Behavioral Y")] [SerializeField]
		private BehavioralEnvironmentY behavioralEnvironmentY;


		protected override void OnEnable(){
			Refresh();
		}

		protected override void OnGUI(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			distance = actor.GetDistance().ToString("0.00");
			vrSpeed = actor.GetSpeed().ToString("0");
			treadmillSpeed = arduinoBasic.GetSpeed().ToString("0.00");

			lick = settingPanel.GetLickCount();
			press = settingPanel.GetSuccessCount();

			chooseL = settingPanel.GetChooseLeft();
			chooseR = settingPanel.GetChooseRight();

			trialNum = (settingPanel.GetFallCount() + settingPanel.GetSuccessCount());


			if(rewardPosition < actor.GetDistance()){
				rewardPosition = actor.GetDistance();
			}

			success = settingPanel.GetRewardCount();
			rewardSize = arduinoBasic.GetRewardLimit();

			stop = settingPanel.GetFallCount();

			timeOfRecording = FormatTime(GetPlayTime());
			manualReward = settingPanel.GetManualReward();
			Repaint();

			base.OnGUI();
		}

		[Button]
		private void Refresh(){
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			screenEffect = FindObjectOfType<ScreenEffect>();
			actorTimer = FindObjectOfType<ActorTimer>();
			behavioralEnvironmentY = FindObjectOfType<BehavioralEnvironmentY>();


			lickTrigger = FindObjectsOfType<LickTrigger>();
			rewardArea = FindObjectsOfType<RewardArea>();
			messageHandler = FindObjectOfType<EventMessageHandler>();
		}

		private void Update(){
			if(Event.current != null){
				actor = FindObjectOfType<Scripts.Actor>();
				settingPanel = FindObjectOfType<SettingPanel>();
				arduinoBasic = FindObjectOfType<ArduinoBasic>();

				distance = actor.GetDistance().ToString("0.00");
				vrSpeed = actor.GetSpeed().ToString("0");
				treadmillSpeed = arduinoBasic.GetSpeed().ToString("0.00");

				lick = settingPanel.GetLickCount();
				press = settingPanel.GetSuccessCount();

				chooseL = settingPanel.GetChooseLeft();
				chooseR = settingPanel.GetChooseRight();

				trialNum = (settingPanel.GetFallCount() + settingPanel.GetSuccessCount());
				rewardPosition = settingPanel.GetRewardDistance();

				success = settingPanel.GetRewardCount();
				rewardSize = arduinoBasic.GetRewardLimit();

				stop = settingPanel.GetFallCount();

				timeOfRecording = FormatTime(GetPlayTime());
				manualReward = settingPanel.GetManualReward();
			}
		}
		

		private float GetPlayTime(){
			if(Application.isPlaying){
				return Time.realtimeSinceStartup;
			}
			else{
				return 0f;
			}
		}

		public string FormatTime(float time){
			System.TimeSpan calc = System.TimeSpan.FromSeconds(time);
			return string.Format("{0:00}:{1:00}:{2:00}", calc.Hours, calc.Minutes, calc.Seconds);
		}

		private bool CheckReference(){
			return !arduinoBasic || !settingPanel || !actor;
		}
	}
}