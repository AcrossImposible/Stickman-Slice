using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlice : MonoBehaviour {

	[SerializeField] AudioClip[] swings;
	AudioSource source;
	public static List<SoundSlice> slices = new List<SoundSlice> ();
	[HideInInspector] public bool use;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		slices.Add (this);
	}
	
	void FixedUpdate () {
		if (Input.GetMouseButton (0)) {
			if (Time.timeScale > 0.95f) {
				source.pitch = 1;
			} else {
				source.pitch = 0.38f;
			}
		}
	}

	public IEnumerator PlaySlice()
	{
		if (!source) source = GetComponent<AudioSource>();
		source.clip = swings[Random.Range(0, swings.Length)];
		if(Menu.soundEnabled)
			source.Play();
		use = true;
		yield return new WaitForSeconds(0.1f);
		use = false;
	}

	public virtual void OnLevelWasLoaded(int level){
		slices.Clear ();
	}
}
