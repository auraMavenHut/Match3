using UnityEngine;
using System.Collections;

public class ClickEvent : GameEvent {
	public GameObject clickedObject;

	public class OnPointerClick : ClickEvent
	{
		public OnPointerClick(GameObject clickedObject)
		{
			this.clickedObject = clickedObject;
		}
	}
}