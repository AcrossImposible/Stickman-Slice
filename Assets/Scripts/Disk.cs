using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter2D (Collision2D col){
		if (col.transform.GetComponent<StickPart> ()) {
			col.transform.GetComponent<StickPart> ().Slice ();
			col.transform.GetComponent<TouchDetect> ().Damage ();
			col.transform.parent.GetComponent<ScriptManager> ().sliced = true;
			col.transform.GetComponent<Collider2D> ().enabled = false;
		}
	}
}
