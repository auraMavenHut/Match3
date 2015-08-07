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
		}
	}

	private void modifyScoreText (){
		GetComponent<Text>().text = totalScore.ToString()+"/"+scoreTarget.ToString();
	}

	private void stopScoring(TimeEvent.OnTimePassed evendData){
		Debug.Log("Will Stop scoring");
		canScore = false;
		eventBus.Publish (new EndGameEvent.OnEndGame(totalScore >= scoreTarget));
	}

	void OnDestroy ()
	{
		eventBus.RemoveListener<TimeEvent.OnTimePassed> (stopScoring);
		eventBus.RemoveListener<ScoreEvent.OnScoring> (addToScore);
	}

}
