using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour {

	[SerializeField] AudioClip[] screamClips;
	public static List<Scream> screams = new List<Scream> ();
	AudioSource audio;
	[HideInInspector] public bool use;
	// Use this for initialization
	void Start () {
		screams.Add (this);
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (use) {
			if (Time.timeScale > 0.95f) {
				audio.pitch = 1.18f;
			} else {
				audio.pitch = 0.58f;
			}
		}
	}

	public void SetClip(int i, Transform parent)
	{
		transform.parent = parent;
		transform.position = Vector3.zero;
		audio.clip = screamClips[i];
		if (Menu.soundEnabled)
			audio.Play();
		use = true;
		StartCoroutine(SetUse());
	}

	IEnumerator SetUse(){
		yield return new WaitForSeconds (1.8f);
		use = false;
		transform.parent = null;
		transform.position = new Vector3 (0, 88, 0);
	}

	public virtual void OnLevelWasLoaded(int level){
		screams.Clear ();
	}
}
