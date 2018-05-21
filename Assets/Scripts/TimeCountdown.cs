using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour {

	public Text timerLabel; 

	public float time;

	// Use this for initialization
	void Start () {
	}

	void Update() {
		time -= Time.deltaTime;

		string minutes = ((int) time / 60).ToString(); //Divide the guiTime by sixty to get the minutes.
		string seconds = (time % 60).ToString("f2");//Use the euclidean division for the seconds.
		// var fraction = (time * 100) % 100;

		// update the label value
		timerLabel.text = "0" + minutes + ":" + seconds;
		// timerLabel.text = string.Format ("{00:00} : {01:00}", minutes, seconds);
	}

	public float getTime() {
		return time;
	}
}
