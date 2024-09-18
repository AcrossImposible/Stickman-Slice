using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour {

	[SerializeField] GameObject objectPool;
	[SerializeField] [Range(3,8)] int amount = 3;
	[SerializeField] Object bro;
	List<GameObject> objects = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for (int i = 0; i < amount; i++) {
			objects.Add (Instantiate (objectPool, transform));
		}
	}
	
	public GameObject GetObject(int lifetime = 3){
		foreach (GameObject go in objects) {
			if (go.activeSelf == false) {
				go.SetActive (true);
				StartCoroutine(Life(go,lifetime));
				return go;
			}
		}
		GameObject goc = Instantiate (objectPool, transform);
		objects.Add (goc);
		StartCoroutine(Life(goc,lifetime));
		return goc;
	}

	IEnumerator Life(GameObject go, float time){
		yield return new WaitForSeconds (time);
		go.transform.parent = transform;
		go.SetActive (false);
	}
		
}
