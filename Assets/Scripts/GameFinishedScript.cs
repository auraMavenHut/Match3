using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameFinishedScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		bool gameStatus = PlayerPrefs.GetInt("gameStatus") == 1;

		if (gameStatus){
			Debug.Log(@"won");
			GetComponent<Text>().text = "You Won :D";
		} else{
			GetComponent<Text>().text = "You lost :(";
		}
	}
	
	public void restartGame(){
		Application.LoadLevel("MainScene");
	}
}
