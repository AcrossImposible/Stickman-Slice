using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return null;
		yield return null;
		Destroy (GetComponent<PointEffector2D> ());
		yield return new WaitForSeconds (3);
		Destroy (gameObject);
	}
}
