using System;
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
	public class RewardContingency : OdinEditorWindow
	{
		[MenuItem("Tools/Project/RewardContingency")]
		private static void OpenWindow()
		{
			var window = GetWindow<RewardContingency>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
			window.Show();
		}

		private Scripts.Actor actor;
		private SettingPanel settingPanel;
		private ArduinoBasic arduinoBasic;
		private ArduinoDataReader arduinoDataReader;

		private DistanceCount distanceCount;

		private SerializedObject serializedObject;

		private LickTrigger[] lickTrigger;

		private bool isLick;


		[TitleGroup("Reward Area Setting")] [LabelText("Random reward at reward zone")] [ReadOnly] public bool randomRewardAtRewardZone = true;
		[TitleGroup("Reward Area Setting")] [LabelText("Random reward at check zone")] [ReadOnly] public bool randomRewardAtCheckZone = false;
		[TitleGroup("Reward Area Setting")] [LabelText("Reward probability")] [Range(0 , 100)] [OnValueChanged("OnRewardProbabilityChanged")] public float rewardProbability;
		
		
		[TitleGroup("Reward Setting")] [LabelText("Maze position range")] [ReadOnly] public float mazePositionRange;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Position")] [OnValueChanged("OnRewardZoneCenterPositionChanged")] [ReadOnly] public float rewardZoneCenterPosition;
		[TitleGroup("Reward Setting")] [LabelText("Reward Zone Size")] [OnValueChanged("OnRewardZoneSizeChanged")] public float rewardZoneSize;
		[TitleGroup("Reward Setting")] [LabelText("Reward valve duration (ms)")] [OnValueChanged("OnRewardValveDurationChanged")] public float rewardValveDuration;
		
		[TitleGroup("Reward Area Setting")] [LabelText("Lick to")] [OnValueChanged("OnLickChange")] public bool lick;
		[TitleGroup("Reward Setting")] [LabelText("Reward Count Limit")] [OnValueChanged("OnRewardCountLimit")] public int rewardLimit;
		[TitleGroup("Reward Setting")] [LabelText("Current Lick Count Limit")] [OnValueChanged("OnCurrentLickCountLimit")] public int currentLickCountLimit;


		private bool isOpenGizmos;


		protected override void OnEnable()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}

		protected override void OnGUI()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();

			try
			{
				distanceCount = FindObjectOfType<DistanceCount>();
				mazePositionRange = distanceCount.GetDistance();
			}
			catch (Exception e)
			{
				mazePositionRange = 0;
			}

			


			Repaint();
			
			
			if (actor || Application.isEditor && Application.isPlaying)
			{
				//DrawMethodButton();
			}
			
			base.OnGUI();
		}
		
		
		

		[Button][TitleGroup("Reward Setting")][LabelText("Deliver Reward")]
		public void DeliverReward()
		{
			settingPanel.GetReward();
		}

		[Button] [TitleGroup("Reward Area Setting")][LabelText("Switch Reward Policy")]
		public void SwitchAreaSetting()
		{
			randomRewardAtCheckZone = !randomRewardAtCheckZone;
			randomRewardAtRewardZone = !randomRewardAtRewardZone;
			
			var rewardZone = FindObjectsOfType<RewardArea>();

			foreach (var VARIABLE in rewardZone)
			{
				VARIABLE.GetComponent<BoxCollider>().enabled = randomRewardAtRewardZone;
			}
			
			foreach (var kLickTrigger in lickTrigger)
			{
				kLickTrigger.GetComponent<BoxCollider>().enabled = randomRewardAtCheckZone;
			}
		}
		
		[Button]
		public void Refresh()
		{
			actor = FindObjectOfType<Scripts.Actor>();
			settingPanel = FindObjectOfType<SettingPanel>();
			arduinoBasic = FindObjectOfType<ArduinoBasic>();
		}
		
		[Button]
		public void SwitchGizmos()
		{
			isOpenGizmos = !isOpenGizmos;
		}

		private void OnLickChange()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetSkipJudge(!this.lick);
			}
		}

		private void OnRewardValveDurationChanged()
		{
			arduinoBasic.SetLimitTime(rewardValveDuration);
		}

		private void OnRandomRewardAtRewardZoneChange()
		{
			
		}

		private void OnRandomRewardAtCheckZoneChange()
		{
			/*
			randomRewardAtCheckZone = !randomRewardAtCheckZone;
			randomRewardAtRewardZone = !randomRewardAtRewardZone;
			
			
			var rewardZone = FindObjectsOfType<RewardArea>();
			
			foreach (var VARIABLE in rewardZone)
			{
				VARIABLE.GetComponent<BoxCollider>().enabled = randomRewardAtRewardZone;
			}
			
			foreach (var kLickTrigger in lickTrigger)
			{
				kLickTrigger.GetComponent<BoxCollider>().enabled = randomRewardAtCheckZone;
			}
			*/
		}

		public void OnRewardZoneCenterPositionChanged()
		{
			
		}

		public void OnRewardZoneSizeChanged()
		{
			var allCollider = FindObjectsOfType<RewardArea>();

			foreach (var g in allCollider)
			{
				var collider = g.GetComponent<BoxCollider>();
				collider.size = new Vector3(1,1,rewardZoneSize);
			}
		}
		
		

		private void OnRewardCountLimit()
		{
			settingPanel.SetReward(rewardLimit);
		}

		private void Update()
		{
			
			if(!Application.isPlaying) return;

			lickTrigger = FindObjectsOfType<LickTrigger>();
			
			var rewardArea = FindObjectsOfType<RewardArea>(); 
			RewardArea minRewardArea = null;
			var minDis = Vector3.Distance(actor.transform.position, rewardArea[0].transform.position);

			foreach (var _rewardArea in rewardArea)
			{
				if (minDis > Vector3.Distance(actor.transform.position, _rewardArea.transform.position))
				{
					minDis = Vector3.Distance(actor.transform.position, _rewardArea.transform.position);
					minRewardArea = _rewardArea;
				}
			}

			rewardZoneCenterPosition = minRewardArea.transform.position.z;
			rewardZoneSize = minRewardArea.GetComponent<BoxCollider>().size.z;


			if (isOpenGizmos)
			{
				foreach (var area in rewardArea)
				{
					area.SetGizmos(randomRewardAtRewardZone);
				}
				
				foreach (var lick in lickTrigger)
				{
					lick.SetGizmos(randomRewardAtCheckZone);
				}
			}
			else
			{
				foreach (var area in rewardArea)
				{
					area.SetGizmos(false);
				}
				
				foreach (var lick in lickTrigger)
				{
					lick.SetGizmos(false);
				}
			}
		}

		private void OnRewardProbabilityChanged()
		{
			settingPanel.SettingReward(rewardProbability);
			Debug.Log("Change");
		}
		
		private void OnCurrentLickCountLimit()
		{
			foreach (var lick in lickTrigger)
			{
				lick.SetCorrectLickCountLimit(currentLickCountLimit);
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

		private void DashboardUpPos()
		{
			

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
