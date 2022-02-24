#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Actor.Editor{
	public class ActorEditor : EditorWindow{
		[MenuItem("Tools/Project/ActorEditor")]
		private static void ShowWindow(){
			var window = GetWindow<ActorEditor>();
			window.titleContent = new GUIContent("TITLE");
			window.Show();
		}

		private void OnGUI(){ }
	}
}
#endif