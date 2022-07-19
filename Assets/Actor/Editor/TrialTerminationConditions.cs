using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Actor.Scripts;
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
    public class TrialTerminationConditions : OdinEditorWindow
    {
        [MenuItem("Tools/Project/TrialTerminationConditions")]
        private static void OpenWindow()
        {
            var window = GetWindow<TrialTerminationConditions>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 500);
            window.Show();
        }

        private Scripts.Actor actor;
        private SettingPanel settingPanel;
        private ArduinoBasic arduinoBasic;
        private ArduinoDataReader arduinoDataReader;
        
        private ActorTimer actorTimer;

        private SerializedObject serializedObject;

        private LickTrigger[] lickTrigger;

        [TitleGroup("Only In Line Maze")]
        [LabelText("Pre-RZ licking cutoff ('lick limit')")]
        [OnValueChanged("OnWrongLickCountLimit")]
        public int wrongLickCountLimit;

        [TitleGroup("Only In Line Maze")]
        [LabelText("Definition of immobility: < X cm/s (treadmill)")]
        [OnValueChanged("OnDefinitionOImmobility")]
        public float definitionOImmobility;

        [TitleGroup("Only In Line Maze")]
        [LabelText("Terminate the trial when immobile > Y sec")]
        [OnValueChanged("OnTerminateTheTrialWhenImmobile")]
        public float terminateTheTrialWhenImmobile;


        protected override void OnEnable()
        {
            Refresh();
            
        }

        private void Refresh()
        {
            actor = FindObjectOfType<Scripts.Actor>();
            settingPanel = FindObjectOfType<SettingPanel>();
            arduinoBasic = FindObjectOfType<ArduinoBasic>();
            actorTimer = FindObjectOfType<ActorTimer>();

            lickTrigger = FindObjectsOfType<LickTrigger>();
            
             wrongLickCountLimit = lickTrigger[1].GetWrongLickCountLimit();
             definitionOImmobility = actorTimer.GetLimitSpeed();
             terminateTheTrialWhenImmobile = actorTimer.GetLimitTime();
        }

        private int lickLimit, speed, time;


        private void OnWrongLickCountLimit()
        {
            foreach (var lick in lickTrigger)
            {
                lick.SetWrongLickCountLimit(wrongLickCountLimit);
            }
        }

        public void OnDefinitionOImmobility()
        {
            var actorTimer = FindObjectOfType<ActorTimer>();

            actorTimer.SetLimitSpeed(definitionOImmobility);
        }

        public void OnTerminateTheTrialWhenImmobile()
        {
            var actorTimer = FindObjectOfType<ActorTimer>();

            actorTimer.SetLimitTime(terminateTheTrialWhenImmobile);
        }
    }
}