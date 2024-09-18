using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSound : MonoBehaviour {

	[SerializeField] public AudioClip[] swings;
	AudioSource audio;
	bool playing;
	Blade blade;
	float needForceSlice = 0;
	// Use this for initialization
	IEnumerator Start () {
		audio = GetComponent<AudioSource> ();
		yield return null;
		blade = FindObjectOfType<Blade> ();
		needForceSlice = Screen.width / 438f;
	}
	
	void FixedUpdate () 
	{
		if (Input.GetMouseButton (0)) {
			if (Time.timeScale > 0.95f) {
				audio.pitch = 1;
			} else {
				audio.pitch = 0.38f;
			}

			Vector2 forceSlice = new Vector2 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
			if (forceSlice.magnitude > needForceSlice && !playing) {
				if (blade) {
					transform.position = blade.transform.position;
				}
				playing = true;
				audio.clip = swings [Random.Range (0, swings.Length)];
				audio.Play ();
				StartCoroutine (SetPlaying());
			}
		}
	}

	IEnumerator SetPlaying(){
		yield return new WaitForSeconds (0.188f);
		playing = false;
	}
}
