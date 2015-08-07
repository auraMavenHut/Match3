using UnityEngine;
using System.Collections;

public class ScoreEvent : GameEvent {
	public int destroyedTiles;

	public class OnScoring : ScoreEvent
	{
		public OnScoring(int destroyedTiles){
			this.destroyedTiles = destroyedTiles;
		}
	}
}
