using UnityEngine;
using System.Collections.Generic;
using System;


public class TilesArray {

	private GameObject[,] tiles;
	private int columnsSize, rowsSize;
	private GameObject[] toDestroyTiles;

	public void InitArrayWithSize (int columns, int rows)
	{
		tiles = new GameObject[columns,rows];
		toDestroyTiles = new GameObject[columns * rows];

		columnsSize = columns;
		rowsSize = rows;
	}

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
		tiles[item.GetComponent<Selectable>().column, item.GetComponent<Selectable>().row] = null;
	}
	
	public GameObject[] GetAllTilesToMove()
	{

		List<GameObject> toMoveTiles = new List<GameObject>();

		for (int i = 0; i<columnsSize; i++) {

			AddTilesInColumnToAnimate(i, toMoveTiles);

		}

		return toMoveTiles.ToArray();
	}


	private void AddTilesInColumnToAnimate(int col, List<GameObject> array)
	{
		int rowToTake = -1;
		int countTiles = 0;
//		Debug.Log (rowsSize);
		for (int i = 0; i<rowsSize; i++) {
		
			GameObject tile = tiles[col, i];

			if(tile != null)
			{
				Selectable sc = tile.GetComponent<Selectable>();

				if(sc.needsToBeDestroyed == true)
				{
					if(rowToTake == -1)
						rowToTake = sc.row;
					
					//				Debug.Log("on c = " + col + " to destroy r = " + rowToTake);
					sc.needsToBeDestroyed = false;
					
					Remove(tile);
					sc.DestroyObject();
					countTiles++;
				}
				else
					if(rowToTake != -1 && sc.needsToBeMoved == false)
				{
					sc.needsToBeMoved = true;
					sc.row = rowToTake;
					rowToTake++;
					array.Add(tile);
				}
			}

		}

		if (countTiles > 0) {

			for (int i = 0; i<countTiles; i++) {
				
				GameObject newOne = new GameObject();
				Selectable sc = newOne.AddComponent<Selectable>();
				sc.row = rowsSize - i - 1;
				sc.column = col;
				sc.newCreated = true;

				array.Add(newOne);
			}
		}
	}


}
