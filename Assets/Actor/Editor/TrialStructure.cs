using System;
using PhilippeFile.Script;
using Puzzle.GameLogic.Scripts;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Editor{
	public class TrialStructure : OdinEditorWindow{
		[MenuItem("Tools/Project/Trial Structure")]
		private static void OpenWindow(){
			var window = GetWindow<TrialStructure>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor _actor;
		private ArduinoDataReader _arduinoDataReader;
		private ScreenEffect _screenEffect;
		private BehavioralEnvironmentY _behavioralEnvironmentY;
		private ArduinoBasic arduinoBasic;

		[LabelText("Cue display")] [TitleGroup("Trail Structure")] [OnValueChanged("OnCueDisplayChanged")]
		public bool isCueDisplay = true;

		[LabelText("Blanking Duration")] [TitleGroup("Trail Structure")] [OnValueChanged("OnBlankingDurationChanged")]
		public float blankingDuration;

		[LabelText("Penalty Blanking Duration")]
		[TitleGroup("Trail Structure")]
		[OnValueChanged("OnPenaltyBlankingDurationChanged")]
		public float penaltyBlankingDuration;


		[LabelText("Trial Structure")]
		[EnumPaging]
		[TitleGroup("Trail Structure")]
		[OnValueChanged("OnStructureTypeChanged")]
		public TrialStructureType structureType;

		[LabelText("Trigger position")]
		[TitleGroup("Closed-Loop Manipulation")]
		[OnValueChanged("OnTriggerPositionChanged")]
		public float triggerPosition;

		[LabelText("Trigger pulse duration")]
		[TitleGroup("Closed-Loop Manipulation")]
		[OnValueChanged("OnTriggerPulseDurationChanged")]
		public float triggerPulseDuration;
		

		protected override void OnEnable(){
			Refresh();
		}
		

		[Button]
		public void Refresh(){
			_actor = FindObjectOfType<Scripts.Actor>();
			_screenEffect = FindObjectOfType<ScreenEffect>();
			_behavioralEnvironmentY = FindObjectOfType<BehavioralEnvironmentY>();

			arduinoBasic = FindObjectOfType<ArduinoBasic>();
			
			blankingDuration = _actor.GetPunishTime();
			penaltyBlankingDuration = _actor.GetRewardTime();
			structureType = _behavioralEnvironmentY.StructureType;
		}

		public void OnCueDisplayChanged(){
			_screenEffect.SetBlankActive(isCueDisplay);
		}

		public void OnBlankingDurationChanged(){
			_actor.SetGetRewardTime(blankingDuration);
		}

		public void OnPenaltyBlankingDurationChanged(){
			_actor.SetPunishTime(penaltyBlankingDuration);
		}

		public void OnStructureTypeChanged(){
			_behavioralEnvironmentY.StructureType = structureType;
		}

		public void OnTriggerPulseDurationChanged()
		{
			arduinoBasic.SetTriggerLimitTime(triggerPulseDuration);
		}

		public void OnTriggerPositionChanged()
		{
			_actor.SetTriggerDistance(triggerPosition);
		}
	}
}