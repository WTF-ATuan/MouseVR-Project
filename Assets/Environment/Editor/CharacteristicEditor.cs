using Sirenix.OdinInspector;
using UnityEngine;

namespace Environment.Editor{
	public class CharacteristicEditor{
		[ShowInInspector] public GameObject viewObject;

		[BoxGroup("All")] [ShowIf("viewObject")] [ProgressBar(0, 1, ColorGetter = "GetBrightNessColor")]
		public float brightNess = 1;

		[BoxGroup("All")] [ShowIf("viewObject")] [ProgressBar(0, 1)]
		public float contrast;


		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

		private Color GetBrightNessColor(float value){
			var color = Color.Lerp(Color.black, Color.white, value);
			var meshRenderer = viewObject.GetComponentInChildren<MeshRenderer>();
			var material = meshRenderer.sharedMaterial;
			material.SetColor(EmissionColor, color);
			return color;
		}
	}
}