using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDetect : MonoBehaviour {

	Color color;
	public ScriptManager sm;

	void Start () {
		color = GetComponent<SpriteRenderer> ().color;
		sm = transform.parent.GetComponent<ScriptManager> ();
	}

	public void Damage(){
		Mode_1.score++;
		//Instantiate (sm.bloodHit, transform.position, Quaternion.identity);
		GetComponent<StickPart> ().DetectPartSlice ();
		StartCoroutine (ColorReturn ());
		if (sm.isGhost) {
			StartCoroutine (BehaiorGhost ());
		}
	}

	IEnumerator ColorReturn(){
		yield return new WaitForSeconds (1);
		//GetComponent<SpriteRenderer> ().color = color;
	}

	IEnumerator BehaiorGhost(){
		yield return new WaitForSeconds (0.5f);
		Transform par = transform.parent;
		for (int i = 0; i < par.childCount; i++) {
			par.GetChild (i).GetComponent<Rigidbody2D> ().gravityScale = 0f;
		}/**/
		for (int i = 0; i < 50; i++) {
			for (int j = 0; j < par.childCount; j++) {
				par.GetChild (j).GetComponent<Rigidbody2D> ().velocity /= 1.02f;
			}
			yield return null;
		}

		yield return new WaitForSeconds (1.5f);

		for (int i = 0; i < 70; i++) {
			for (int j = 0; j < par.childCount; j++) {
				par.GetChild (j).GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,18));
			}
			yield return null;
		}
	}
}
