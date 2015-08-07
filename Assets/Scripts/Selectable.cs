using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//public enum TileType {
//	Red = 0xff0a0a,
//	Green = 0x0ecd17,
//	Blue = 0x007eff,
//	White = 0xffffff,
//	Pink = 0xf481d5,
//	Purple = 0x7a08a5};

public class Selectable : MHBaseClass
{
	public int type;
	public int column, row;
	public bool needsToBeMoved = false;
	public bool needsToBeDestroyed = false;
	public bool newCreated = false;
	public bool isSelected = false;

	string[] tileColors = new string[]{ "bc5879", "32a994", "e8bfa5", "80baf0", "b18dd3" };

	GameObject selectionObject = null;

	void Awake ()
	{
		type = Random.Range (0, tileColors.Length);
	}

	void Start ()
	{
		if (gameObject.transform.FindChild ("Selection") == null) {
			return;
		}

		selectionObject = gameObject.transform.FindChild ("Selection").gameObject;

		Image bg = gameObject.GetComponent<Image> ();
		bg.color = HexToColor (tileColors [type]);
	}

	public void MarkTileToDestroy ()
	{
		needsToBeDestroyed = true;
	}

	public void MarkTileToMove ()
	{
		needsToBeMoved = true;
		
	}

	public void UnmarkMovingTile ()
	{
		needsToBeMoved = false;
		needsToBeDestroyed = false;
		newCreated = false;
		isSelected = false;
		selectionObject.SetActive (false);
	}

	public void DestroyObject ()
	{
		Destroy (this.gameObject);
	}

	Color HexToColor (string hex)
	{
		byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color (r / 255.0f, g / 255.0f, b / 255.0f, 255 / 255.0f);
	}
}
