using UnityEngine;
using System.Collections;

public class Selectable : MHBaseClass
{
	bool isSelected = false;

	GameObject selectionObject = null;

	void Start ()
	{
		selectionObject = gameObject.transform.FindChild ("Selection").gameObject;

		eventBus.AddListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}

	void OnSelectionChanged (PointerEvent.OnSelectionChanged eventData)
	{
		if (eventData.targetObject == gameObject) {
			isSelected = eventData.selected;
			selectionObject.SetActive (isSelected);
		}
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}
}
