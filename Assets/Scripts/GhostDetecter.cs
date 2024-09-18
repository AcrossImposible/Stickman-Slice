using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetecter : MonoBehaviour {

	MngrGhosts mg;
	// Use this for initialization
	void Start () {
		mg = FindObjectOfType<MngrGhosts> ();
	}
	
	void OnTriggerEnter2D (Collider2D col){
		if (!col.transform.parent)
			return;
		ScriptManager sm = col.transform.parent.GetComponent<ScriptManager> ();
		if (sm && sm.sliced && sm.isGhost && col.tag != "GHOST") {
			for (int i = 0; i < sm.transform.childCount; i++) {
				sm.transform.GetChild(i).tag = "GHOST";
			}/**/
			mg.AddGhost ();
		}
	}
}
