using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood2 : MonoBehaviour {


	public static List<GameObject> bloods = new List<GameObject>();
	Transform parent;


	void Start () {
		bloods.Add (gameObject);
		parent = transform.parent;
		gameObject.SetActive (false);
	}

	void OnEnable(){
		StartCoroutine (Active ());
	}

	IEnumerator Active(){
		yield return new WaitForSeconds (5.3f);
		transform.parent = parent;
		gameObject.SetActive (false);
	}

	public static GameObject SpawnBlood(Transform parent, Vector3 posLocal){
		for (int i = 0; i < bloods.Count; i++) {
			if (bloods [i].activeInHierarchy == false) {
				bloods [i].SetActive (true);
				bloods [i].transform.parent = parent;
				bloods [i].transform.localPosition = posLocal;
				bloods [i].transform.localRotation = Quaternion.identity;
				return bloods [i];
			}
		}
		return null;
	}

	void OnLevelWasLoaded(int level){
		ClearBloods ();
	}

	public static void ClearBloods(){
		bloods.Clear ();
	}

}
