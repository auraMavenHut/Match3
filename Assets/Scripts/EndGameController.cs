using UnityEngine;
using System.Collections;

public class EndGameController : MHBaseClass {

	void Start () {
		eventBus.AddListener <EndGameEvent.OnEndGame> (endGame);
	}

	private void endGame (EndGameEvent.OnEndGame eventData){
		PlayerPrefs.SetInt(@"gameStatus", eventData.gameStatus ? 1 : 0);
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<EndGameEvent.OnEndGame> (endGame);
	}
}
