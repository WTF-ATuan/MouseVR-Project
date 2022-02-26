using Environment.Scripts;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Environment.Editor{
	public class EnvironmentEditor : OdinMenuEditorWindow{
		[MenuItem("Tools/Project/EnvironmentEditor")]
		private static void OpenWindow(){
			GetWindow<EnvironmentEditor>().Show();
		}

		protected override OdinMenuTree BuildMenuTree(){
			var tree = new OdinMenuTree();
			tree.Add("Create New", new CreateNewEnvironmentData());
			tree.Add("Edit" , new EditEnvironmentData());
			tree.AddAllAssetsAtPath("EnemyData", "Assets/Environment/Data", typeof(EnvironmentData));
			return tree;
		}
	}
}