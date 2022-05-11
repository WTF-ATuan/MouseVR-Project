using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Scripts{
	public class MaterialSwitcher : MonoBehaviour{
		[SerializeField] private GameObject targetObject;
		[SerializeField] private Material targetMaterial;
		[SerializeField] private int materialIndex;
		private Material _originMaterial;

		[Button]
		private void Switch(){
			if(targetObject == null){
				var meshRenderer = GetComponent<MeshRenderer>();
				var currentMaterials = meshRenderer.materials;
				currentMaterials[materialIndex] = targetMaterial;
				meshRenderer.materials = currentMaterials;
			}
			else{
				var meshRenderer = targetObject.GetComponent<MeshRenderer>();
				var currentMaterials = meshRenderer.materials;
				currentMaterials[materialIndex] = targetMaterial;
				meshRenderer.materials = currentMaterials;
			}
		}
	}
}