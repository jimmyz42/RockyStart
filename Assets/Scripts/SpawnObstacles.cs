using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {

	public GameObject river;
	public GameObject rock;
	public GameObject goal;
	public GameObject volcano;
	public float speed;

	// Chance that the row contains rocks
	public float rockRowProbability;
	// Chance that the row contains rivers
	public float riverRowProbability;
	// Chance of rock in a given row
	public float rockProbability;

	public float finalProbability;

	private float size;
	private float width;
	private float height;

	private float increment;

	public float time;

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize * 2.0f;
		width = height * Camera.main.aspect;
		size = width / 10f;
		increment = (finalProbability - riverRowProbability) / time;
		InvokeRepeating ("spawn", 0, 1f * size / speed);
		Invoke ("spawnGoal", 300);
		InvokeRepeating ("spawnVolcano", 1, 30);
		InvokeRepeating ("increaseRowProb", 2, 5);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void increaseRowProb() {
		rockProbability += increment;
		riverRowProbability += increment;
		rockRowProbability += increment;
	}

	void spawnGoal() {
		var goalCastle = Instantiate (goal, new Vector3 (0, height / 2f, 0), Quaternion.identity);
		goalCastle.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed);
	}

	void spawnVolcano() {
		float[] locations = {-4.5f,-3.5f,-2.5f,-1.5f,-0.5f,0.5f,1.5f,2.5f,3.5f,4.5f}; 
		float location = locations[Random.Range (0,10)]*size;
		var Volcano = Instantiate (volcano, new Vector3 (location, height / 2f, 0), Quaternion.identity);
		Volcano.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed);
	}

	void spawn() {
		var rand = Random.Range (0.0f, 1.0f);
		if (rand < riverRowProbability) { //generate river 
			for (float i = -4.5f; i < 5; i++) {
				var riverClone = Instantiate (river, new Vector3 (i * size, height/2f + size, 0),
					Quaternion.identity);
				riverClone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed);
			}
		} else if (rand < riverRowProbability + rockRowProbability) { // generate rocks
			for (float i = -4.5f; i < 5; i++) {
				var rand2 = Random.Range (0.0f, 1.0f);
				if (rand2 > rockProbability)
					continue;
				var rockClone = Instantiate (rock, new Vector3 (i * size, height/2f + size, 0),
					Quaternion.identity);
				rockClone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed);
			}
		}
	}
}
