using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBloodManager : MonoBehaviour {

	[SerializeField] Image iconDrop;
	[SerializeField] Color blue;
	[SerializeField] Color red;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 2; i++) {
			transform.GetChild (i+1).GetChild (1).gameObject.SetActive (false);
		}
		transform.GetChild (Game.colorBlood+1).GetChild (1).gameObject.SetActive (true);
	}
	
	public void SetColorBlood(int id){
		Game.colorBlood = id;
		Saves.SaveColorBlood ();
		StartCoroutine (Close ());
		for (int i = 0; i < 2; i++) {
			transform.GetChild (i+1).GetChild (1).gameObject.SetActive (false);
		}
		transform.GetChild (id+1).GetChild (1).gameObject.SetActive (true);
		iconDrop.color = id == 0 ? blue : red;
		Blood2.ClearBloods ();
		PoolBlood.FillPool ();
		foreach (Spray s in Spray.sprays) {
			s.SetColor ();
		}
	}

	IEnumerator Close(){
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime = 0.002f;
		yield return new WaitForSeconds (0.03f);
		gameObject.SetActive (false);
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
	}

	public void Cancel(){
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		gameObject.SetActive (false);
	}
}
