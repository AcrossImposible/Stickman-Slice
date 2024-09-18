using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_3 : MonoBehaviour {

	[SerializeField] Sprite board;
	[SerializeField] GameObject pickCave;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindWithTag ("BOARD");
		go.GetComponent<SpriteRenderer> ().sprite = board;
		go.GetComponent<SpriteRenderer> ().drawMode = SpriteDrawMode.Simple;
		StartCoroutine (SpawnPicks ());
	}

	IEnumerator SpawnPicks(){
		yield return new WaitForSeconds (0.5f);
		Vector3 pos = Vector3.zero;
		StickPart[] sticks = FindObjectsOfType<StickPart> ();
		if (sticks.Length >= 55) {
			for (int i = 0; i < sticks.Length; i++) {
				yield return null;
				if (!sticks[i] || sticks [i].transform.position.y > 8.8f || sticks [i].transform.position.y < -3.8f)
					continue;
				else if(!sticks[i].transform.parent.GetComponent<ScriptManager>().isGhost) {
					if (!sticks [i].transform.parent.GetComponent<ScriptManager> ().sliced) {
						pos = new Vector3 (sticks [i].transform.position.x, 11.8f, 1);
						Instantiate (pickCave, pos, Quaternion.identity);
						break;
					}

				}
			}
		}

		StartCoroutine (SpawnPicks ());
	}
}
