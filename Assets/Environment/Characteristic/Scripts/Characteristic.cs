using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Characteristic.Scripts{
	public class Characteristic : MonoBehaviour{
		public Vector3 Position => transform.position;

		[HorizontalGroup("characteristic")] [ReadOnly] [SerializeField]
		private List<GameObject> characteristicObjects;

		[HorizontalGroup("characteristic")]
		[Button(ButtonSizes.Small)]
		private void Refresh(){
			var transforms = GetComponentsInChildren<Transform>();
			characteristicObjects = transforms.Select(x => x.gameObject).ToList();
		}

		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

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