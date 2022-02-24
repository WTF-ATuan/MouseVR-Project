#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Actor.Editor{
	public class ActorEditor : OdinEditorWindow{
		[MenuItem("Tools/Project/ActorEditor")]
		private static void OpenWindow(){
			var window = GetWindow<ActorEditor>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.Show();
		}

	}
}
#endif