using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TxtWarningWave : MonoBehaviour {

	
	void OnEnable()
	{
		GetComponent<TMP_Text>().color = new Color(0.148f, 0.875f, 0.898f, 1);
		StartCoroutine(Behavior());
	}

	IEnumerator Behavior() {
		transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
		for (int i = 0; i < 10; i++) {
			transform.localScale += new Vector3 (0.05f, 0.05f, 0.05f);
			yield return null;
		}
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 8; i++) {
			transform.localScale -= new Vector3 (0.05f, 0.05f, 0.05f);
			yield return null;
		}
		//yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 12; i++) {
			transform.localScale += new Vector3 (0.02f, 0.02f, 0.02f);
			yield return null;
		}
		//yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 15; i++) {
			transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);
			yield return null;
		}
		yield return new WaitForSeconds (0.5f);
		for (float f = 1; f > 0; f -= 0.03f) {
			yield return null;
			GetComponent<TMP_Text> ().color = new Color (0.148f, 0.875f, 0.898f, f);
		}
		GetComponent<TMP_Text> ().color = new Color (0.148f, 0.875f, 0.898f, 1);
		gameObject.SetActive (false);
	}
}
