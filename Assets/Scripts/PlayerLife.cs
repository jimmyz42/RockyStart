using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

	public AudioSource playerAudio;
	public AudioClip gameOver;
	public float Volume;

	void Start () {
		playerAudio	= GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		playerAudio.PlayOneShot (gameOver, Volume);
		if (transform.position.y <= - Camera.main.orthographicSize) { // Or 0, will check with team
			Die ();
			BackgroundScript.Alive = false;
			Time.timeScale = 0;
		}
	}

	// Die is called if player is out of screen
	void Die(){
		// yield WaitForSeconds(0.25);
		Destroy (gameObject);
	}

	//temporary

	float maxSpeed;
	float objectBoundaryRadius = 0.5f * 0.5f;

	void Move(float x, float y) {

		Vector3 pos = transform.position;
		pos.y += y * maxSpeed * Time.deltaTime;
		pos.x += x * maxSpeed * Time.deltaTime;


		if (pos.y  + objectBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - objectBoundaryRadius;
		}
//		if (pos.y - objectBoundaryRadius < -Camera.main.orthographicSize) {
//			pos.y = -Camera.main.orthographicSize + objectBoundaryRadius;
//		}

		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		if (pos.x + objectBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - objectBoundaryRadius;
		}
		if (pos.x - objectBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + objectBoundaryRadius;
		}

		transform.position = pos;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		Move (x, y);
	}
}
