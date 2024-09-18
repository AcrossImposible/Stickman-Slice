using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


	[SerializeField] GameObject stickman;
	[SerializeField] GameObject stickmanPresageWave;
	[SerializeField] GameObject stickmanGhost;
	[SerializeField] [Range(1,18)] int delay = 1;
	public static bool pause = false;
	public static bool notBlueStickman = true;
	int limitDelay = 15;
	int lowDelay = 1;
	int degress;

	IEnumerator Start () {
		notBlueStickman = true;
		pause = false;
		yield return new WaitForSeconds (delay);
		StartCoroutine (Spawn ());
		StartCoroutine (LimitManager ());
	}
	
	IEnumerator Spawn(){
		degress = Random.Range (0, 18);
		degress *= 20;
		if (!pause) {
			int r = Random.Range (0, 38);
			if (r > 7) {
				Instantiate (stickman, transform.position, Quaternion.Euler (0, 0, degress));
			} else {
				Instantiate (stickmanGhost, transform.position, Quaternion.Euler (0, 0, degress));
			}
		}
		int range = Random.Range (lowDelay, limitDelay);
		yield return new WaitForSeconds (range);
		StartCoroutine (Spawn ());
	}

	public void Disable(){
		gameObject.SetActive (false);
	}

	IEnumerator LimitManager(){
		yield return new WaitForSeconds (8.8f);
		if (notBlueStickman && Random.Range (0, 11) > 8/**/) {
			Instantiate(stickmanPresageWave, transform.position+new Vector3(0, 1.8f, 0), Quaternion.Euler(0,0,degress));
		}
		if (limitDelay > 2) {
			limitDelay--;
		}
		if (limitDelay < 8) {
			lowDelay = 1;
		}
		StartCoroutine (LimitManager());
	}
}
