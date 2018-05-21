using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObjectScript : MonoBehaviour {

	float width;
	float height;
	float cellSize;

	void Awake()
	{
		//Unity units
		height = Camera.main.orthographicSize * 2.0f;
		width = height * Camera.main.aspect;
		cellSize = width / 10f;
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

		transform.localScale = 
			Vector3.Scale(transform.localScale, new Vector3(cellSize/local_sprite_size.x, cellSize/local_sprite_size.y, 1.0f));
		Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D>().size = S;

		// Sprite sorting order
		/*
		SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();

		if (thisSprite) {
			thisSprite.sortingOrder = 2;
		}
		*/
	}

	// Use this for initialization
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
