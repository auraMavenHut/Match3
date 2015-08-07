using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MHBaseClass {
	private int totalScore = 0;
	public int scoreTarget;
	public int baseTileScore;
	private bool canScore = true;

	// Use this for initialization
	void Start () {
		eventBus.AddListener <TimeEvent.OnTimePassed> (stopScoring);
		eventBus.AddListener <ScoreEvent.OnScoring> (addToScore);
		modifyScoreText();
	}

	private void addToScore(ScoreEvent.OnScoring evendData){
		if (canScore){
			totalScore += evendData.destroyedTiles*baseTileScore;
			modifyScoreText();
			if(totalScore >= scoreTarget){
//				eventBus.Publish (new EndGameEvent.OnEndGame(true));
				//this might cause problems. use commented version above if so.
				stopScoring(null);
			}
		}
	}

	private void modifyScoreText (){
		GetComponent<Text>().text = totalScore.ToString()+"/"+scoreTarget.ToString();
	}

	private void stopScoring(TimeEvent.OnTimePassed evendData){
		canScore = false;
		eventBus.Publish (new EndGameEvent.OnEndGame(totalScore >= scoreTarget));
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<TimeEvent.OnTimePassed> (stopScoring);
		eventBus.RemoveListener<ScoreEvent.OnScoring> (addToScore);
	}

}
