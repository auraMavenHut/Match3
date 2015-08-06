using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//public enum TileType {
//	Red = 0xff0a0a, 
//	Green = 0x0ecd17, 
//	Blue = 0x007eff, 
//	White = 0xffffff, 
//	Pink = 0xf481d5, 
//	Purple = 0x7a08a5};

public class Selectable : MHBaseClass
{
	public int type;
	public int column, row;
	bool isSelected = false;

	string[] tileColors = new string[]{"bc5879", "32a994", "e8bfa5", "80baf0", "b18dd3"};

	GameObject selectionObject = null;

	void Awake()
	{
		type = Random.Range(0, tileColors.Length);
	}
	void Start ()
	{
		selectionObject = gameObject.transform.FindChild ("Selection").gameObject;
		Image bg = gameObject.GetComponent<Image> ();
		bg.color = HexToColor(tileColors[type]);//tileColors[type];
		eventBus.AddListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}

	void OnSelectionChanged (PointerEvent.OnSelectionChanged eventData)
	{
		if (eventData.targetObject == gameObject) {
			isSelected = eventData.selected;
			selectionObject.SetActive (isSelected);

			if(isSelected)
			{
				Debug.Log("c = " + column + "r = " + row);
			}

		}
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}


	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color(r/255.0f,g/255.0f,b/255.0f, 255/255.0f);
	}
}
