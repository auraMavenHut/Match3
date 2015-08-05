using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum TileType {Red, Green, Blue, White, Pink, Purple, COUNT};

public class Selectable : MHBaseClass
{
	public TileType type;

	bool isSelected = false;

	GameObject selectionObject = null;

	void Start ()
	{
		selectionObject = gameObject.transform.FindChild ("Selection").gameObject;
		type = (TileType) Random.Range(0, (int)TileType.COUNT);

		eventBus.AddListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}

	void OnSelectionChanged (PointerEvent.OnSelectionChanged eventData)
	{
		if (eventData.targetObject == gameObject) {
			isSelected = eventData.selected;
			selectionObject.SetActive (isSelected);

			Debug.Log(type);
		}
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}
}
