using UnityEngine;
using System.Collections;

public class EndGameController : MHBaseClass {

	void Start () {
		eventBus.AddListener <EndGameEvent.OnEndGame> (endGame);
	}

	private void endGame (EndGameEvent.OnEndGame eventData){
		Debug.Log("Ending the game");
		PlayerPrefs.SetInt("gameStatus", eventData.gameStatus ? 1 : 0);
		Application.LoadLevel("EndGameScene");
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<EndGameEvent.OnEndGame> (endGame);
	}
}
