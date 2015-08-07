using UnityEngine;
using System.Collections;

public class PointerEvent : GameEvent {
	public GameObject targetObject;
	public bool isSelected;

	public class OnSelectionChanged : PointerEvent
	{
		public OnSelectionChanged(GameObject targetObject, bool isSelected)
		{
			this.targetObject = targetObject;
			this.isSelected = isSelected;
		}
	}


	public class OnPointerFinished : PointerEvent
	{
	}

}
