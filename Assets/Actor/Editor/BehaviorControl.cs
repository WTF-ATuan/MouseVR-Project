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

namespace Actor.Editor{
    public class BehaviorControl : OdinEditorWindow
    {
        [MenuItem("Tools/Project/BehaviorControl")]
        private static void OpenWindow()
        {
            var window = GetWindow<BehaviorControl>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
            window.Show();
        }

        private Scripts.Actor actor;
        private SettingPanel settingPanel;
        private ArduinoBasic arduinoBasic;
        private ArduinoDataReader arduinoDataReader;
        private ScreenEffect screenEffect;

        [LabelText("Cue display")] [TitleGroup("Behavior Control")] [OnValueChanged("OnCueDisplayChanged")] 
        public bool isCueDisplay = true;

        [LabelText("Blanking Duration")] [TitleGroup("Behavior Control")] [OnValueChanged("OnBlankingDurationChanged")]
        public float blankingDuration;

        [LabelText("Penalty Blanking Duration")] [TitleGroup("Behavior Control")] [OnValueChanged("OnPenaltyBlankingDurationChanged")]
        public float penaltyBlankingDuration;
        
        [LabelText("Trial bias offset factor")] [TitleGroup("Behavior Control")] [OnValueChanged("OnTrialBiasOffsetFactorChanged")][ReadOnly]
        public float trialBiasOffsetFactor ;
        
        [LabelText("Trigger pulse duration")] [TitleGroup("Behavior Control")] [OnValueChanged("OnTriggerPulseDurationChanged")][ReadOnly]
        public float triggerPulseDuration;
        
        [LabelText("Trigger position")] [TitleGroup("Behavior Control")] [OnValueChanged("OnTriggerPositionChanged")][ReadOnly]
        public float triggerPosition ;
        
        [LabelText("Random trial-type structure")] [TitleGroup("Behavior Control")] [OnValueChanged("OnRandomTrialTypeStructureChanged")][ReadOnly]
        public bool randomTrialTypeStructure;
        
        [LabelText("Non-random trial-type structure")] [TitleGroup("Behavior Control")] [OnValueChanged("OnNonRandomTrialTypeStructureChanged")][ReadOnly]
        public bool nonRandomTrialTypeStructure;
        
        


        protected override void OnEnable()
        {
            actor = FindObjectOfType<Scripts.Actor>();
            settingPanel = FindObjectOfType<SettingPanel>();
            arduinoBasic = FindObjectOfType<ArduinoBasic>();
            screenEffect = FindObjectOfType<ScreenEffect>();
        }

        protected override void OnGUI()
        {
            actor = FindObjectOfType<Scripts.Actor>();
            settingPanel = FindObjectOfType<SettingPanel>();
            arduinoBasic = FindObjectOfType<ArduinoBasic>();
            screenEffect = FindObjectOfType<ScreenEffect>();

            if (!arduinoBasic || !screenEffect)
            {
                base.OnGUI();
                return;
            }

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
        
        [Button]
        public void Refresh()
        {
            actor = FindObjectOfType<Scripts.Actor>();
            settingPanel = FindObjectOfType<SettingPanel>();
            arduinoBasic = FindObjectOfType<ArduinoBasic>();
        }

        public void OnCueDisplayChanged()
        {
            screenEffect.SetBlankActive(isCueDisplay);
        }

        public void OnBlankingDurationChanged()
        {
            actor.SetGetRewardTime(blankingDuration);
        }

        public void OnPenaltyBlankingDurationChanged()
        {
            actor.SetPunishTime(penaltyBlankingDuration);
        }

        public void OnRandomTrialTypeStructureChanged()
        {
            
        }

        public void OnTrialBiasOffsetFactorChanged()
        {
            
        }

        public void OnTriggerPulseDurationChanged()
        {
            
        }

        public void OnTriggerPositionChanged()
        {
            
        }

        public void OnNonRandomTrialTypeStructureChanged()
        {
            
        }
        
    }
    

}
