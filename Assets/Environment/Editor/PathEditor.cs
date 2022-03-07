using System;
using System.Collections.Generic;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

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
			Selection.activeGameObject = pathCreator.gameObject;
			var bezierPath = pathCreator.bezierPath;
			bezierPath.Space = PathSpace.xz;
			bezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
			bezierPath.AutoControlLength = 0.01f;
		}


		[HorizontalGroup("Export", 100)]
		[Button(ButtonSizes.Small, ButtonStyle.FoldoutButton)]
		private void ClearPath(){
			foreach(var path in pathHandlers){
				Object.DestroyImmediate(path);
			}

			pathHandlers.Clear();
		}

		[HorizontalGroup("Export", 300)]
		[Button(ButtonSizes.Small, ButtonStyle.FoldoutButton)]
		private void ExportPathToGameObject(){
			var bezierPath = pathCreator.bezierPath;
			for(var i = 1; i < bezierPath.NumAnchorPoints; i++){
				var j = i - 1;
				var startPoint = bezierPath[i * 3];
				var endPoint = bezierPath[j * 3];
				CreatePathPoint(startPoint, endPoint);
			}
		}


		[HorizontalGroup("Export")] [ReadOnly] [SerializeField]
		private List<PathHandler> pathHandlers = new List<PathHandler>();

		private void CreatePathPoint(Vector3 startPoint, Vector3 endPoint){
			var centerPoint = Vector3.Lerp(startPoint, endPoint, 0.5f);
			var gameObject = new GameObject($"Path + {centerPoint}");
			var pathHandler = gameObject.AddComponent<PathHandler>();
			pathHandler.Initialized(startPoint, endPoint);
			StageUtility.PlaceGameObjectInCurrentStage(gameObject);
			pathHandler.transform.position = centerPoint;
			pathHandler.transform.SetParent(pathCreator.transform);
			pathHandlers.Add(pathHandler);
		}
	}
}