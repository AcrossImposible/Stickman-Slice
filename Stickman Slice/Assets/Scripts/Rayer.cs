using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
		hit = Physics2D.Raycast (transform.position, -Vector2.up);
		if (hit) {
			print (hit.transform.name);
			Debug.DrawRay (transform.position, -Vector2.up, new Color (1f, 1f, 1f), 15);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
