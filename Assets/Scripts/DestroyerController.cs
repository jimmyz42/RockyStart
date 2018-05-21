using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour {

	public GameObject Destroyer;
	private Animator animator;
	public GameObject Explosion;

	public float cooldown = 1;
	float timer;
	public float movementSpeed = 5;

	public AudioSource destroyerAudio;
	public AudioClip rockBreak;
	public float Volume;

	// Use this for initialization
	void Start () {
		timer = cooldown;
		Destroyer = GameObject.Find("Destroyer1");

		destroyerAudio = GetComponent<AudioSource> ();
		animator = this.GetComponent<Animator>();
		animator.SetInteger("direction", 2);
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Destroy rock ahead
//		if (Input.GetKey (KeyCode.Space)) {
//			Debug.Log("Destroy rock");
//			DestroyAhead ();
//		}
		if (timer > 0) {
			timer -= Time.deltaTime;	
		}
		// Move
		if (Input.anyKey) {
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
				animator.SetInteger ("direction", 1);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);
				animator.SetInteger ("direction", 3);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector3.down * movementSpeed * Time.deltaTime);
				animator.SetInteger ("direction", 0);
			}
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector3.up * movementSpeed * Time.deltaTime);
				animator.SetInteger ("direction", 2);
			}
		} else {
			animator.SetInteger ("direction", 2);
		}
	}

	// TODO: complete implementation
	void DestroyAhead () {
		Vector3 positionAhead =  new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		float radius = 1;

		if (Physics.CheckSphere (positionAhead, radius)) {
			
		} else {
			// No object ahead
		}
	}

	void OnCollisionStay2D(Collision2D col) {

//		Debug.Log ("collision triggered");
//		Debug.Log("Destroy tag: " + col.gameObject.tag);

		if(col.gameObject.tag == "rock")
		{
//			Debug.Log ("is rock");
			if (Input.GetKey (KeyCode.LeftShift) && timer <= 0) {
				timer = cooldown;
//				Debug.Log("Destroy rock");
				PlayExplosion(col.gameObject);
				Destroy (col.gameObject);
				destroyerAudio.PlayOneShot(rockBreak, Volume);
			}
		}
		
	}


	void PlayExplosion(GameObject rock) {
		GameObject explosion = (GameObject)Instantiate (Explosion);
		explosion.transform.position = rock.transform.position;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "volcano") {
			BackgroundScript.Alive = false;
			PlayExplosion (Destroyer);
			Destroy (Destroyer);
		}
	}
}
