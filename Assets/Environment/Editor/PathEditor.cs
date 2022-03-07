using System;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Environment.Editor{
	[Serializable]
	public class PathEditor{
		[HorizontalGroup("path")] [ShowInInspector] [InlineEditor(Expanded = false)]
		public PathCreator pathCreator;

		[HorizontalGroup("path")]
		[Button(ButtonSizes.Small)]
		private void AutoFind(){
			var creator = FindCurrentStage();
			if(creator != null) pathCreator = creator;
			InitCreator();
		}

		[HorizontalGroup("path")]
		[Button(ButtonSizes.Small)]
		private void Create(){
			if(pathCreator) return;
			var currentCreator = FindCurrentStage();
			if(currentCreator){
				pathCreator = currentCreator;
				return;
			}

			var gameObject = new GameObject("PathCreator");
			var creator = gameObject.AddComponent<PathCreator>();
			StageUtility.PlaceGameObjectInCurrentStage(gameObject);
			pathCreator = creator;
			InitCreator();
		}

		private PathCreator FindCurrentStage(){
			var stageHandle = StageUtility.GetCurrentStageHandle();
			var creator = stageHandle.FindComponentOfType<PathCreator>();
			return creator;
		}

		private void InitCreator(){
			var bezierPath = pathCreator.bezierPath;
			bezierPath.Space = PathSpace.xz;
			bezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
			bezierPath.AutoControlLength = 0.01f;
		}
	}
}