using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorController : MonoBehaviour {

	public GameObject Creator;
	public Rigidbody2D rb;
	public GameObject bridge;
	public GameObject Explosion;

	public float cooldown = 1;
	float timer;
	public float bridgeSpeed = 2;
	public float movementSpeed = 5;

	public AudioSource creatorAudio;
	public AudioClip iceBreak;
	public float Volume;

	private Animator animator;

	// Use this for initialization
	void Start () {
		timer = cooldown;
		Creator = GameObject.Find("Creator1");
		rb = GetComponent<Rigidbody2D>();

		creatorAudio = GetComponent<AudioSource> ();
		animator = this.GetComponent<Animator>();
		animator.SetInteger ("Direction", 2);
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (timer > 0) {
			timer -= Time.deltaTime;	
		}

		/*
		float moveLeft = 0;
		float moveRight = 0;
		float moveUp = 0;
		float moveDown = 0;
		*/

		// Move
		if (Input.anyKey) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				//moveLeft = 1;
				animator.SetInteger ("Direction", 1);
				transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
			} 
			if (Input.GetKey (KeyCode.RightArrow)) {
				//moveRight = 1;
				animator.SetInteger ("Direction", 3);
				transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				//moveDown = 1;
				animator.SetInteger ("Direction", 0);
				transform.Translate (Vector3.down * movementSpeed * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				//moveUp = 1;
				animator.SetInteger ("Direction", 2);
				transform.Translate (Vector3.up * movementSpeed * Time.deltaTime);
			}
		} else {
			animator.SetInteger ("Direction", 2);
		}

		/* 
		// Using RigidBody, do not delete, might be useful later
		float moveHorizontal = moveRight - moveLeft; // = Input.GetAxis ("Horizontal");
		float moveVertical = moveUp - moveDown; // = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		if (moveHorizontal != 0 || moveVertical != 0) {
			rb.AddForce (movement * speed);
		}
		*/
	}
		
	/*
	void Update () {
		// Create bridge ahead
		if (Input.GetKeyDown(KeyCode.RightShift)) {
			Debug.Log("Create bridge");
			CreateBridge ();
		}
	}
	*/

	void CreateBridge () {
		var bridgeClone = Instantiate (bridge, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
		bridgeClone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -bridgeSpeed);
	}

	void CreateBridge (Vector3 position) {
		var bridgeClone = Instantiate (bridge, position, Quaternion.identity);
		bridgeClone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -bridgeSpeed);
	}

	void OnCollisionStay2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "river") {
			if (Input.GetKeyDown (KeyCode.RightShift) && timer <= 0) {
				timer = cooldown;
				CreateBridge (coll.gameObject.transform.position);
				Destroy (coll.gameObject);
				creatorAudio.PlayOneShot (iceBreak, Volume);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "volcano") {
			BackgroundScript.Alive = false;
			PlayExplosion (Creator);
			Destroy (Creator);
		}
	}

	void PlayExplosion(GameObject rock) {
		GameObject explosion = (GameObject)Instantiate (Explosion);
		explosion.transform.position = rock.transform.position;
	}
}
	

