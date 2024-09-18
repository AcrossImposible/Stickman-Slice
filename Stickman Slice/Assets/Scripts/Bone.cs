using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (1.8f);
		for (int i = 0; i < 100; i++) {
			yield return null;
			GetComponent<SpriteRenderer> ().color -= new Color (0,0,0, 1f/150f);
			//print (GetComponent<SpriteRenderer> ().color);
		}
		
	}
}
