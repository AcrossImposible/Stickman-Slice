using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWave : MonoBehaviour {

	[SerializeField] GameObject stickman;
	public static List<GameObject> spawners = new List<GameObject>();
	bool firstRun = true;


	IEnumerator Start(){
		firstRun = false;
		if (spawners.Count >= 7) {
			for (int i = 0; i < 7; i++) {
				spawners.RemoveAt (0);
			}
		}
		spawners.Add (gameObject);
		yield return null;
		gameObject.SetActive (false);
	}

	void OnEnable () {
		if (!firstRun) {
			//StartCoroutine (Delay ());
			Spawn();
		}
	}


	IEnumerator Delay(){
		yield return new WaitForSeconds (2.7f);
		//yield return new WaitForSeconds (Random.Range(0.1f, 1));
		Spawn();
	}

	public void Spawn(){
		int degress = Random.Range (0, 18);
		degress *= 20;
		Instantiate (stickman, transform.position, Quaternion.Euler (0, 0, degress));
		//gameObject.SetActive (false);
	}

}
