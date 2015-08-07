using UnityEngine;
using System.Collections;

public class EndGameEvent : GameEvent {
	public bool gameStatus;

	public class OnEndGame : EndGameEvent
	{
		public OnEndGame(bool gameStatus){
			this.gameStatus = gameStatus;
		}
	}
}
