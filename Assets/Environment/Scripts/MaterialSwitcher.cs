using System;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Scripts{
	public class MaterialSwitcher : MonoBehaviour{
		[SerializeField] private GameObject targetObject;
		[SerializeField] private Material targetMaterial;
		[SerializeField] private int materialIndex;
		private Material _originMaterial;

		private void Start()
		{
			EventBus.Subscribe<SwitchMaterialDetected>(OnSwitchMaterialDetected);
		}

		private void OnSwitchMaterialDetected(SwitchMaterialDetected obj)
		{
			Switch();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				EventBus.Post(new SwitchMaterialDetected());
			}
		}

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