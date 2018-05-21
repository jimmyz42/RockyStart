using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour {

	public GameObject[] splash;
	public Vector2 speed;
	private Material material;
	private int splashCnt = 0;

	public static bool Alive = true;
	

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			if (splashCnt < splash.Length) {
				Destroy (splash [splashCnt]);
				splashCnt++;
			} else if(splashCnt == splash.Length) { 
				Time.timeScale = 1;
				splashCnt++; // so that it doesn't do any more splash screen stuff
			} else if (!Alive) {
				SceneManager.LoadScene("MainScene");
				Time.timeScale = 1;
				Alive = true;
			}
		}
		material.SetTextureOffset("_MainTex", Time.time * speed);
	}
}
