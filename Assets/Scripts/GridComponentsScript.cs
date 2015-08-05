using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridComponentsScript : MonoBehaviour {

	public int rows, columns;
	public GameObject item;
	public float itemSize;

	public GridLayoutGroup grid;

	private List<GameObject> items;

	// Use this for initialization
	void Start () {
	
		items = new List<GameObject>();

		GenerateItems();
	}


	private void GenerateItems()
	{
		for (int i = 0; i< rows; i++) {
			for (int j = 0; j< columns; j++) {

				GameObject elem = CreateRandomTile();
				elem.transform.SetParent(grid.transform);
				elem.transform.localScale = new Vector3(1,1,1);
				elem.transform.localPosition = new Vector3 (i*itemSize, -j*itemSize, 0f);

				items.Add(elem);
			}
		}
	}

	private GameObject CreateRandomTile()
	{
		return Instantiate (item, Vector3.zero, Quaternion.identity) as GameObject;;
	}
	

	// Update is called once per frame
	void Update () {

	}

}
