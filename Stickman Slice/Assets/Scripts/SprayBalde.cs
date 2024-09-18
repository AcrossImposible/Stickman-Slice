using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBalde : MonoBehaviour {

	public static List<SprayBalde> spraysBlade = new List<SprayBalde> ();
	[HideInInspector] public bool use;
	Animation animation;

	void Start(){
		this.animation = GetComponent<Animation> ();
		spraysBlade.Add (this);
	}

	public void PLay(Vector3 posSpray){
		transform.position = posSpray - new Vector3 (0, 0, -3);
		this.animation.Play ();
		StartCoroutine (SetUse ());
	}

	IEnumerator SetUse(){
		use = true;
		yield return new WaitForSeconds (0.1f);
		transform.position = new Vector3 (88, 0, 0);
		use = false;
	}

	public virtual void OnLevelWasLoaded(int level){
		spraysBlade.Clear ();
	}
}
