using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum TimerType
{
	text = 0,
	graphic
}
;

public class TimerController : MHBaseClass
{

	public TimerType timerTag;
	public float maxTime;
	private float maxHeight;
	private float startTop;
	private float startTime;
	private Image timeStrip;
	private bool timerEnabled = true;

	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
		if (timerTag == TimerType.graphic) {
			timeStrip = GetComponent<Image> ();
			maxHeight = timeStrip.rectTransform.rect.height;
			startTop = timeStrip.rectTransform.offsetMax.y;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(timerEnabled){
			setTimerUIComponent ();
		}
	}

	private int calculateTimeLeft ()
	{
		float currentTime = Time.time;

		int timeLeft = (int)(maxTime - (currentTime - startTime));

		return timeLeft;
	}

	private void setTimerUIComponent ()
	{
		int timeLeft = calculateTimeLeft ();
		if (timeLeft >= 0) {
			switch (timerTag) {
			case TimerType.text:
				{
					int minutes = timeLeft / 60;
					int seconds = timeLeft - minutes * 60;
					GetComponent<Text> ().text = minutes.ToString () + ":" + System.String.Format("{0:00}", seconds);
				}
				break;

			case TimerType.graphic:
				{
					float newTimeStripScale = timeLeft / maxTime;
					//float newTop = (1 - newTimeStripScale) * maxHeight + startTop;
					//			timeStrip.rectTransform.offsetMax.Set(timeStrip.rectTransform.offsetMax.x, newTop);
					//found a way to move it down, by moving the y pivot to 0 and scaling
					transform.localScale = new Vector2 (newTimeStripScale, 1.0f);
				}
				break;
			}
			if (timeLeft == 0) {
				timerEnabled = false;
				eventBus.Publish (new TimeEvent.OnTimePassed());
				Debug.Log("time finished");
			}
		}
	}
}
