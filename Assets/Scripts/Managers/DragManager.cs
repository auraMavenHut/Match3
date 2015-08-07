using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragManager : MHBaseClass, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	void NotifySelectHitObject(PointerEventData eventData)
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			GameObject selectedObject = hit.transform.gameObject;

			eventBus.Publish (new PointerEvent.OnSelectionChanged (selectedObject, true));
		}
	}

	void NotifyDeselectHitObjects()
	{
		eventBus.Publish (new PointerEvent.OnSelectionChanged (null, false));
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		NotifySelectHitObject (eventData);
	}

	public void OnDrag (PointerEventData eventData)
	{
		NotifySelectHitObject (eventData);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		NotifyDeselectHitObjects ();
	}
}
