using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragManager : MHBaseClass, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	LinkedList<GameObject> selectedObjects = new LinkedList<GameObject> ();

	void Start ()
	{
		eventBus.AddListener<PointerEvent.OnSelected> (OnSelected);
	}

	void OnSelected (PointerEvent.OnSelected eventData)
	{
		selectedObjects.AddLast (eventData.targetObject);
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<PointerEvent.OnSelected> (OnSelected);
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			eventBus.Publish (new PointerEvent.OnSelectionChanged (hit.transform.gameObject, true));
		}
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		
	}

	public void OnDrag (PointerEventData eventData)
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			eventBus.Publish (new PointerEvent.OnSelectionChanged (hit.transform.gameObject, true));
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		foreach (GameObject selectedObject in selectedObjects) {
			eventBus.Publish (new PointerEvent.OnSelectionChanged (selectedObject, false));
		}

		selectedObjects.Clear ();
	}
}
