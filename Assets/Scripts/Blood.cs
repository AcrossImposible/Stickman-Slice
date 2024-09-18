using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
		//gameObject.SetActive (false);
	}
}
