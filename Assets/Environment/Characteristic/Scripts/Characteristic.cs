using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Characteristic.Scripts{
	public class Characteristic : MonoBehaviour{
		[BoxGroup("Offset")] [SerializeField] private Transform sidePoint;

		[HorizontalGroup("characteristic")] [ReadOnly] [SerializeField]
		private List<GameObject> characteristicObjects;

		private List<Vector3> characteristicLocalPositionList;
		private List<Vector3> characteristicLocalRotationList;

		[HorizontalGroup("characteristic")]
		[Button(ButtonSizes.Small)]
		private void Refresh(){
			var transforms = GetComponentsInChildren<Transform>();
			characteristicObjects = transforms.Select(x => x.gameObject).ToList();
			characteristicObjects.Remove(gameObject);
			InitLocalPosition();
		}

		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

		private void InitLocalPosition(){
			characteristicLocalPositionList = new List<Vector3>();
			characteristicLocalRotationList = new List<Vector3>();
			foreach(var characteristic in characteristicObjects){
				characteristicLocalPositionList.Add(characteristic.transform.localPosition);
				characteristicLocalRotationList.Add(characteristic.transform.localEulerAngles);
			}
		}
		[Button]
		public void SetSide(bool isOrigin){
			if(isOrigin){
				foreach(var characteristicObject in characteristicObjects){
					characteristicObject.transform.parent = transform;
				}
			}
			else{
				foreach(var characteristicObject in characteristicObjects){
					characteristicObject.transform.parent = sidePoint;
				}
			}

			for(var index = 0; index < characteristicObjects.Count; index++){
				var localPosition = characteristicLocalPositionList[index];
				var localRotation = characteristicLocalRotationList[index];
				var characteristicObject = characteristicObjects[index];
				characteristicObject.transform.localPosition = localPosition;
				characteristicObject.transform.localEulerAngles = localRotation;
			}
		}

		public void ModifyAllBrightness(float value){
			if(characteristicObjects.Count < 1 || characteristicObjects == null){
				Refresh();
			}

			foreach(var characteristicObject in characteristicObjects){
				var meshRenderer = characteristicObject.GetComponent<MeshRenderer>();
				var color = Color.Lerp(Color.black, Color.white, value);
				var material = meshRenderer.sharedMaterial;
				material.SetColor(EmissionColor, color);
			}
		}

		public void ModifySelectedBrightness(GameObject selected, float value){
			if(Application.isPlaying) throw new Exception("should be runtime");
			if(characteristicObjects.Count < 1 || characteristicObjects == null){
				Refresh();
			}

			if(!characteristicObjects.Contains(selected)){
				throw new Exception("is not in Characteristic List");
			}

			var meshRenderer = selected.GetComponent<MeshRenderer>();
			var color = Color.Lerp(Color.black, Color.white, value);
			var material = meshRenderer.material;
			material.SetColor(EmissionColor, color);
		}
	}
}