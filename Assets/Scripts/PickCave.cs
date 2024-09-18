using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCave : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		float size = Random.Range (0.58f, 1.8f);
		transform.localScale = new Vector3 (size, size, size);
		yield return new WaitForSeconds (1.8f);
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col) {
		if (col.transform.GetComponent<TouchDetect> ()) {
			col.transform.GetComponent<TouchDetect> ().Damage ();
			col.transform.parent.GetComponent<ScriptManager> ().sliced = true;
			col.transform.GetComponent<StickPart> ().Slice ();
		}
	}
}
