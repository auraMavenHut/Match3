using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragManager : MHBaseClass, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

	void NotifySelectHitObject(PointerEventData eventData)
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			eventBus.Publish (new PointerEvent.OnSelectionChanged (hit.transform.gameObject, true));
		}
	}

	void NotifyDeselectHitObjects()
	{
		foreach (GameObject selectedObject in selectedObjects) {
			eventBus.Publish (new PointerEvent.OnSelectionChanged (selectedObject, false));

		}

		selectedObjects.Clear ();
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		NotifySelectHitObject (eventData);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		NotifyDeselectHitObjects ();
	}

	public void OnDrag (PointerEventData eventData)
	{
		NotifySelectHitObject (eventData);
	}
}
