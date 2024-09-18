using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsPosDetect : MonoBehaviour {

	[SerializeField] bool left;
	// Use this for initialization
	void Start () {
		float posX;
		if (left) {
			posX = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).x - 0.01f;
		} else {
			posX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x + 0.01f; 
		}
		transform.position = new Vector3 (posX, 3, 0);
		Destroy (this);
	}
}
