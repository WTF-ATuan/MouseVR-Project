using UnityEngine;

public class MenuSettings<T> where T : UnityEngine.Object
{
	public string typeName;
	public bool[] show = { false, false, false, false, false };
	public string name = "";
	public int selectedInstanceId;
	public Rect editRect = new Rect(); // stores location editing window.
	private T _selectedObj;
	public T selectedObj
	{
		get {return _selectedObj;}
		set
		{
			if (!UnityEngine.Object.ReferenceEquals(value,_selectedObj))
			{
				_selectedObj = value;
				if (value!=null) { selectedInstanceId = value.GetInstanceID(); }
				else { selectedInstanceId = 0; }
			}
		}
	}
}