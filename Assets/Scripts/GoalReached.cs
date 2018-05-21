using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour {

	public AudioSource goalAudio;
	public AudioClip gameWon;
	public float Volume;

	void Start () {
		goalAudio = GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Creator" || coll.gameObject.tag == "Destroyer") {
			//Debug.Log("Goal reached");
			goalAudio.PlayOneShot (gameWon, Volume);
			BackgroundScript.Alive = false;
		}
	}
}
