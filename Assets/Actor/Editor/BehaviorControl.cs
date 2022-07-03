using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class BehaviorControl : OdinEditorWindow
{
    [MenuItem("Tools/Project/BehaviorControl")]
    private static void OpenWindow()
    {
        var window = GetWindow<BehaviorControl>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
        window.Show();
    }
}
