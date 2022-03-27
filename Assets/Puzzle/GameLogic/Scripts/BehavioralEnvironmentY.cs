using System;
using Environment.Characteristic.Scripts;
using Environment.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzle.GameLogic.Scripts{
	public class BehavioralEnvironmentY : MonoBehaviour{
		[BoxGroup("Point")] [SerializeField] private Transform rightPoint;
		[BoxGroup("Point")] [SerializeField] private Transform leftPoint;

		[BoxGroup("Characteristic")] [SerializeField]
		private Characteristic square;

		[BoxGroup("Characteristic")] [SerializeField]
		private Characteristic circle;

		[BoxGroup("Area")] [SerializeField] private AreaCollisionHandler areaL;
		[BoxGroup("Area")] [SerializeField] private AreaCollisionHandler areaR;

		[ReadOnly] [SerializeField] private int randomNumber;

		private Vector3 LeftPosition => leftPoint.position;
		private Vector3 RightPosition => rightPoint.position;

		private void Start(){
			areaL.onExperimentCompleted += OnExperimentCompleted;
			areaR.onExperimentCompleted += OnExperimentCompleted;
		}

		private void OnExperimentCompleted(AreaType obj){
			randomNumber = Random.Range(0, 1);
			if(randomNumber == 0){
				square.transform.position = LeftPosition;
				circle.transform.position = RightPosition;
				areaL.SetAreaType(AreaType.Award);
				areaR.SetAreaType(AreaType.Punish);
			}

			if(randomNumber == 1){
				square.transform.position = RightPosition;
				circle.transform.position = LeftPosition;
				areaL.SetAreaType(AreaType.Punish);
				areaR.SetAreaType(AreaType.Award);
			}
		}
	}
}