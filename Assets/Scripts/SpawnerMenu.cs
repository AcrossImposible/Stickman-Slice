using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMenu : MonoBehaviour {

	[SerializeField] GameObject stickman;
	static int delay = -2;
	int degress;
	bool isFirst = false;
	// Use this for initialization
	IEnumerator Start () {
		delay += 2;
		yield return new WaitForSeconds (delay);
		StartCoroutine (Spawn ());
		isFirst = true;

	}

	public virtual void OnLevelWasLoaded(int level){
		delay = -2;
	}

	IEnumerator Spawn(){
		degress = Random.Range (0, 18);
		degress *= 20;
		int r = Random.Range (0, 38);
		Instantiate (stickman, transform.position, Quaternion.Euler (0, 0, degress));
		int range = Random.Range (5, 12);
		yield return new WaitForSeconds (range);
		StartCoroutine (Spawn ());
	}
}
