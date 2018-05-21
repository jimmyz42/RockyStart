using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameMusic : MonoBehaviour {

	public AudioMixerSnapshot splashScreen;
	public AudioMixerSnapshot loadingGame;
	public AudioMixerSnapshot playingGame;

	public AudioClip[] stings;
	public AudioSource stingSource;
	// Fading based on the tempo of the music.
	public float bpm = 128;

	// For transitioning in and out of music snapshots.
	private float m_TransitionIn;
	private float m_TransitionOut;
	private float m_QuarterNote;

	// Use this for initialization
	void Start () 
	{
		// length in milliseconds
		m_QuarterNote = 60 / bpm;
		m_TransitionIn = m_QuarterNote;
		m_TransitionOut = m_QuarterNote * 32;
	}

	void onLoadGameMusic(Input loadGame) {
		// Check if user has clicked to start loading game.
		if (loadGame.Equals("loadGame")) {
			loadingGame.TransitionTo(m_TransitionIn);
		}
	}

	void onStartMainGameMusic(Input startGame) {
		// Check if user has clicked "Start Game."
		if (startGame.Equals("startGame")) {
			playingGame.TransitionTo(m_TransitionIn);
		}
	}

	/*
	 * Used for playing sound effects 
	 * 
	*/
	void PlaySting() {

	}
}
