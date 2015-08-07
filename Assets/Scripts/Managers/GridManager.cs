using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MHBaseClass
{

	public int columns, rows;
	public GameObject item;
	public float itemSize;

	public int[,] Grid;
	
	private TilesArray items = new TilesArray ();

	private bool needsToAnimate;

	private int tileIndex = 0;

	private GameObject boardObject = null;

	private Vector2 offset = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
	
		items.InitArrayWithSize (columns, rows);

		needsToAnimate = false;
	
		boardObject = gameObject;

		float boardWidth = boardObject.GetComponent<RectTransform> ().sizeDelta.x;
		offset = new Vector2 (-boardWidth / 2 + (boardWidth - columns * itemSize + itemSize) / 2, itemSize / 2);

		CreateGrid ();


		eventBus.AddListener<PointerEvent.OnPointerFinished> (OnPointerFinished);

	}

	void CreateGrid ()
	{
		Grid = new int[columns, rows];

		for (int x = 0; x < columns; x++) {

			for (int y = 0; y < rows; y++) {
				GameObject elem = CreateRandomTileAtPosition (x, y);
				Grid [x, y] = elem.GetComponent<Selectable> ().type;

				items [x, y] = elem;
			}
		}
	}

	private GameObject CreateRandomTileAtPosition (int x, int y)
	{
		GameObject elem = Instantiate (item, Vector3.zero, Quaternion.identity) as GameObject;
		elem.name = "Tile" + tileIndex;
		tileIndex++;
		elem.transform.SetParent (gameObject.transform);

		elem.GetComponent<Selectable> ().column = x;
		elem.GetComponent<Selectable> ().row = y;
		elem.transform.localScale = new Vector3 (1, 1, 1);
		elem.transform.localPosition = new Vector3 (offset.x + x * itemSize, offset.y + y * itemSize, 0f);

		return elem;
	}
	

	// Update is called once per frame
	void Update ()
	{
		if(needsToAnimate)
		{
			CheckForTilesToMove ();

			needsToAnimate = false;
		}
	}


	void OnPointerFinished(PointerEvent.OnPointerFinished data)
	{
		needsToAnimate = true;
	}


	void CheckForTilesToMove ()
	{
		GameObject[] tilesToMove = items.GetAllTilesToMove ();

		if (tilesToMove.Length > 0) {
			ReplacePlaceholderTilesIn (tilesToMove);
		}

		foreach (GameObject tile in tilesToMove) {
		
			if (tile != null) {
				Selectable sc = tile.GetComponent<Selectable> ();

				StartCoroutine (MoveTilesInColumn (tile, new Vector3 (offset.x + sc.column * itemSize, offset.y + sc.row * itemSize, 0f)));
			}
		}
	}



	void ReplacePlaceholderTilesIn (GameObject[] tiles)
	{
		int[] tilesToReplace = new int[tiles.Length];
		int counting = 0;

		for (int i = 0; i < tiles.Length; i++) {

			GameObject tile = tiles [i];

			if (tile != null) {
				
				Selectable sc = tile.GetComponent<Selectable> ();
				
				if (sc.newCreated == true) {

					tilesToReplace [counting] = i;
					counting++;
				}
			}
		}

		if (counting > 0) {

			for (int i = 0; i < counting; i++) {
				GameObject phTile = tiles [tilesToReplace [i]];
				Selectable sc = phTile.GetComponent<Selectable> ();
				GameObject newOne = CreateRandomTileAtPosition (sc.column, sc.row);
				tiles [tilesToReplace [i]] = newOne;

				newOne.transform.localPosition = new Vector3 (offset.x + sc.column * itemSize, offset.y + (rows + GetNumberOfNewTilesFromArrayOnColumn(tiles,sc.column)) * itemSize, 0f);

				Destroy (phTile);
			}
		}
	}



	int GetNumberOfNewTilesFromArrayOnColumn(GameObject[] tiles, int col)
	{
		int counting = 0;

		for (int i = 0; i < tiles.Length; i++) {
			
			GameObject tile = tiles [i];
			
			if (tile != null) {
				
				Selectable sc = tile.GetComponent<Selectable> ();
				
				if (sc.newCreated == true && sc.column == col) {
					
					counting++;
				}
			}
		}

		return counting;
	}



	IEnumerator MoveTilesInColumn (GameObject item, Vector3 newPosition)
	{
		float timeSinceStarted = 0f;
		while (true) {
			timeSinceStarted += Time.deltaTime;
			item.transform.localPosition = Vector3.Lerp (item.transform.localPosition, newPosition, timeSinceStarted);
			
			if (item.transform.localPosition == newPosition) {
				Selectable sc = item.GetComponent<Selectable> ();

				items [sc.column, sc.row] = null;
				items [sc.column, sc.row] = item;

				sc.UnmarkMovingTile ();

				// tile finished animation
				yield break;
			}
			
			yield return null;
		}
	}

	void OnDestroy()
	{
		eventBus.RemoveListener<PointerEvent.OnPointerFinished> (OnPointerFinished);
	}

}
