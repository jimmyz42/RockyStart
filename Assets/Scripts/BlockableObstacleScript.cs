using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockableObstacleScript : MonoBehaviour {

	float width;
	float height;
	public Sprite rock;
	public Sprite theRock;
	public Sprite volcano;
	bool destructable;
	public float theRockProbability;

	void Awake()
	{
		//size of a cell in unity units
		destructable = (Random.Range(0f, 1f) > theRockProbability);
		if (destructable) {
			GetComponent<SpriteRenderer> ().sprite = rock;
			gameObject.tag = "rock";
		} else {
			GetComponent<SpriteRenderer> ().sprite = theRock;
			gameObject.tag = "theRock";
		}
		height = Camera.main.orthographicSize * 2.0f;
//		Debug.Log ("height: " + height);
		width = height * Camera.main.aspect;
//		Debug.Log ("width: " + width);
		float cellwidth = width / 10f;
//		Debug.Log ("cellwidth: " + cellwidth);
//		float cellheight = height / 10f;
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
//		Debug.Log ("sprite_size: " + sprite_size);
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
//		Debug.Log ("local_sprite_size: " + local_sprite_size);
//		Debug.Log ("transform.localScale: " + transform.localScale);
		transform.localScale = 
			new Vector3(cellwidth/local_sprite_size.x, cellwidth/local_sprite_size.y, 1.0f);
//		Debug.Log ("transform.localScale: " + transform.localScale);

		Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D>().size = S;

//		Debug.Log ("new sprite size: " + GetComponent<SpriteRenderer>().sprite.rect.size/GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
//		Debug.Log ("GetComponent<BoxCollider2D>().size: " + GetComponent<BoxCollider2D>().size);
		SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();

		if (thisSprite) {
			thisSprite.sortingOrder = 2;
		}
		
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

		Vector2 pos = Camera.main.WorldToViewportPoint (transform.position);

		if (pos.y < (-local_sprite_size.y/2f) ) {
			Destroy (gameObject);
		}
	}

}
