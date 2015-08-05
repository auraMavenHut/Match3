using UnityEngine;
using System.Collections;

public class PointerEvent : GameEvent {
	public GameObject targetObject;
	public bool selected;

	public class OnSelectionChanged : PointerEvent
	{
		public OnSelectionChanged(GameObject clickedObject, bool selected)
		{
			this.targetObject = clickedObject;
			this.selected = selected;
		}
	}
}