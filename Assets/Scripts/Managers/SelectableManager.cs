using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectableManager : MHBaseClass
{
	LinkedList<GameObject> selectedObjects = new LinkedList<GameObject> ();
	int targetType = -1;

	void Start ()
	{
		eventBus.AddListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<PointerEvent.OnSelectionChanged> (OnSelectionChanged);
	}

	void OnSelectionChanged (PointerEvent.OnSelectionChanged eventData)
	{
		// find selected color
		if (eventData.isSelected) {
			if (targetType == -1) {
				targetType = eventData.targetObject.GetComponent<Selectable> ().type;
			}
		} else {
			targetType = -1;
		}

		// check if tile has valid selection (wasn't already selected, has the same type as the others selected before, and is adjacent to the last selected)
		if (eventData.targetObject) {
			Selectable selectable = eventData.targetObject.GetComponent<Selectable> ();
			if (eventData.isSelected) {
				if (IsSelectionValid (eventData.targetObject)) {
					selectable.isSelected = eventData.isSelected;
					GameObject highlight = eventData.targetObject.transform.FindChild ("Selection").gameObject;
					highlight.SetActive (selectable.isSelected);

					selectedObjects.AddLast (eventData.targetObject);
				}
			}
		} else {
			// deselect all tiles and check which need to be destroyed
			foreach (GameObject selectedObject in selectedObjects) {
				Selectable selectable = selectedObject.GetComponent<Selectable> ();
				if (selectable.isSelected) {
					selectable.isSelected = eventData.isSelected;
					GameObject highlight = selectedObject.transform.FindChild ("Selection").gameObject;
					highlight.SetActive (selectable.isSelected);

					if (CanDestroy ()) {
						selectable.MarkTileToDestroy ();
					}
				}
			}
			selectedObjects.Clear ();
		}
	}


	bool AreTilesAdjacent(Selectable tile1, Selectable tile2)
	{
		return (tile1.row == tile2.row || tile1.row == tile2.row - 1 || tile1.row == tile2.row + 1) && (tile1.column == tile2.column || tile1.column == tile2.column - 1 || tile1.column == tile2.column + 1);
	}

	bool IsSelectionValid (GameObject gameObject)
	{
		Selectable currSelectable = gameObject.GetComponent<Selectable> ();
					
		if (!currSelectable.isSelected && currSelectable.type == targetType) {
			bool belongsToGroup = false;
			if (selectedObjects.Count < 1) {
				belongsToGroup = true;
			} else {
				GameObject previousSelectedObject = selectedObjects.Last.Value;
				Selectable selectable = previousSelectedObject.GetComponent<Selectable> ();
				if (AreTilesAdjacent (currSelectable, selectable)) {
					belongsToGroup = true;
				}
			}

			return belongsToGroup;
		}

		return false;
	}

	bool CanDestroy ()
	{
		return selectedObjects.Count > 2;
	}
}
