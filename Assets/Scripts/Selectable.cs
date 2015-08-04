using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class Selectable : MHBaseClass
{
	public bool isSelected = false;
	bool wasSelected = false;

	GameObject selectionObject = null;

	void Start ()
	{
		selectionObject = gameObject.transform.FindChild ("Selection").gameObject;

		eventBus.AddListener<ClickEvent.OnPointerClick> (OnSelectionChanged);
	}

	void OnSelectionChanged (ClickEvent.OnPointerClick eventData)
	{
		if (eventData.clickedObject == gameObject) {
			isSelected = !wasSelected;
			wasSelected = isSelected;

			selectionObject.SetActive (isSelected);
		}
	}

	void CheckSelectionChanged ()
	{
		if (wasSelected != isSelected) {
			OnSelectionChanged (new ClickEvent.OnPointerClick (gameObject));
		}
	}

	void Update ()
	{
		CheckSelectionChanged ();
	}
}
