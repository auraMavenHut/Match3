using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragManager : MHBaseClass, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public void OnPointerClick (PointerEventData eventData)
	{
		/*RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			Debug.Log ("Clicked " + hit.transform.name);

			eventBus.Publish(new ClickEvent.OnPointerClick(hit.transform.gameObject));
		}*/
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		
	}

	public void OnDrag (PointerEventData eventData)
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			if (!hit.transform.gameObject.GetComponent<Selectable> ().isSelected) {
				eventBus.Publish (new ClickEvent.OnPointerClick (hit.transform.gameObject));
			}
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		
	}
}
