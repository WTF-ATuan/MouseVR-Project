using UnityEditor;
using UnityEngine;

public class Utility{
	public static void SelectMenu<T>(MenuSettings<T> settings) where T : UnityEngine.Object{
		// Object cant be found (possible serialization on run)
		if(settings.selectedObj == null){
			T obj = null;
			// Check if instance ID is valid
			if(settings.selectedInstanceId != 0){
				try{
					obj = (T)EditorUtility.InstanceIDToObject(settings.selectedInstanceId);
				}
				catch(System.InvalidCastException){
					obj = null;
				} // catches changed instanceID on restart.
			}

			// Otherwise find first on list.
			if(obj == null){
				obj = Object.FindObjectOfType<T>();
			}

			if(obj != null){
				settings.selectedObj = obj;
			}
		}

		settings.selectedObj = (T)EditorGUILayout.ObjectField(settings.selectedObj, typeof(T), true);
	}
}