using UnityEngine;
using System.Collections;
using System;

public class TilesArray {

	private GameObject[,] tiles;

	public GameObject this[int row, int column]
	{
		get
		{
			try
			{
				return tiles[row, column];
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		set
		{
			tiles[row, column] = value;
		}
	}


	public void Remove(GameObject item)
	{
		tiles[item.GetComponent<Selectable>().row, item.GetComponent<Selectable>().column] = null;
	}


	IEnumerator MoveTilesInColumn(GameObject item, Vector3 newPosition)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime;
			item.transform.position = Vector3.Lerp(item.transform.position, newPosition, timeSinceStarted);
			
			// If the object has arrived, stop the coroutine
			if (item.transform.position == newPosition)
			{
				yield break;
			}
			
			// Otherwise, continue next frame
			yield return null;
		}
	}
}
