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

		private void Start(){
			areaL.onExperimentCompleted += OnExperimentCompleted;
			areaR.onExperimentCompleted += OnExperimentCompleted;
		}

		private void OnExperimentCompleted(AreaType obj){
			randomNumber = Random.Range(0, 2);
			SetLevel(randomNumber);
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
		}

		public void SetRightSide()
		{
			SetLevel(0);
		}

		public void SetLeftSide()
		{
			SetLevel(1);
		}
	}
}