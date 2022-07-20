using System;
using Environment.Characteristic.Scripts;
using Environment.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Characteristic = Environment.Characteristic.Scripts.Characteristic;
using Random = UnityEngine.Random;

namespace Puzzle.GameLogic.Scripts{
	public class BehavioralEnvironmentY : MonoBehaviour{
		[BoxGroup("Characteristic")] [SerializeField]
		private Characteristic square;

		[BoxGroup("Characteristic")] [SerializeField]
		private Characteristic circle;

		[BoxGroup("Area")] [SerializeField] private AreaCollisionHandler areaL;
		[BoxGroup("Area")] [SerializeField] private AreaCollisionHandler areaR;

		[ReadOnly] [SerializeField] private int randomNumber;
		private int _previousNumber;
		public TrialStructureType StructureType{ get; set; }

		private void Start(){
			areaL.onExperimentCompleted += OnExperimentCompleted;
			areaR.onExperimentCompleted += OnExperimentCompleted;
		}

		private void OnExperimentCompleted(AreaType obj){
			switch(StructureType){
				case TrialStructureType.Random:
					randomNumber = Random.Range(0, 2);
					SetLevel(randomNumber);
					break;
				case TrialStructureType.Alternating:
					SetLevel(_previousNumber == 0 ? 1 : 0);
					break;
				case TrialStructureType.FixedTrial:
					SetLevel(_previousNumber == 0 ? 0 : 1);
					break;
				case TrialStructureType.TrialAOnly:
					SetLevel(0);
					break;
				case TrialStructureType.TrialBOnly:
					SetLevel(1);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		[Button]
		private void SetLevel(int number){
			switch(number){
				case 0:
					square.SetSide(true);
					circle.SetSide(true);
					areaL.SetAreaType(AreaType.Award);
					areaR.SetAreaType(AreaType.Punish);
					break;
				case 1:
					square.SetSide(false);
					circle.SetSide(false);
					areaL.SetAreaType(AreaType.Punish);
					areaR.SetAreaType(AreaType.Award);
					break;
			}

			_previousNumber = number;
		}

		public void SetRightSide(){
			SetLevel(0);
		}

		public void SetLeftSide(){
			SetLevel(1);
		}

		public Vector3 GetAwardVector(){
			if(areaL.GetAreaType() == AreaType.Award){
				return areaL.transform.position;
			}
			else{
				return areaR.transform.position;
			}
		}
	}

	public enum TrialStructureType{
		Random,
		Alternating,
		FixedTrial,
		TrialAOnly,
		TrialBOnly,
	}
}