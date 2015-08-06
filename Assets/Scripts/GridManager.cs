using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public int columns, rows;
	public GameObject item;
	public float itemSize;

	public int[,] Grid;
	
	private TilesArray items;

	private bool shouldAnimate;

	// Use this for initialization
	void Start () {
	
//		items = new TilesArray();
		shouldAnimate = false;

//		GenerateItems();

		CreateGrid ();
	}

	void CreateGrid()
	{
		Grid = new int[columns, rows];

		for (int x = 0; x < columns; x++) {

			for(int y = 0; y < rows; y++)
			{
				GameObject elem = Instantiate (item, Vector3.zero, Quaternion.identity) as GameObject;
				Grid[x,y] = elem.GetComponent<Selectable>().type;
				elem.transform.SetParent(gameObject.transform);
				elem.GetComponent<Selectable>().column = x;
				elem.GetComponent<Selectable>().row = y;
				elem.transform.localScale = new Vector3(1,1,1);
				elem.transform.localPosition = new Vector3 (x*itemSize, y*itemSize, 0f);

//				items[x,y] = elem;
			}
		}
	}
	
	private void GenerateItems()
	{
		for (int i = 0; i< rows; i++) {
			for (int j = 0; j< columns; j++) {

				GameObject elem = CreateRandomTile();
//				elem.transform.SetParent(grid.transform);
				elem.transform.localScale = new Vector3(1,1,1);
				elem.transform.localPosition = new Vector3 (i*itemSize, -j*itemSize, 0f);

//				items.Add(elem);
			}
		}
	}

	private GameObject CreateRandomTile()
	{
		return Instantiate (item, Vector3.zero, Quaternion.identity) as GameObject;
	}
	

	// Update is called once per frame
	void Update () {

//		if()
	}

}
