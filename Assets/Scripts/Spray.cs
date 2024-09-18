using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour {

	public static List<Spray> sprays = new List<Spray> ();
	[HideInInspector] public bool use;
	[SerializeField] Color[] color;
	Animation animation;

	void Start(){
		this.animation = GetComponent<Animation> ();
		sprays.Add (this);/**/
		GetComponent<SpriteRenderer>().color = color[Game.colorBlood];
	}


	public void SetColor(){
		GetComponent<SpriteRenderer>().color = color[Game.colorBlood];
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.H)) {
			this.animation.Play ();
		}
	}

	public void PLay(Vector3 posSpray){
		transform.position = posSpray;
		this.animation.Play ();
		StartCoroutine (SetUse ());
	}

	IEnumerator SetUse(){
		use = true;
		yield return new WaitForSeconds (0.17f);
		transform.position = new Vector3 (88, 0, 0);
		use = false;
	}

	public virtual void OnLevelWasLoaded(int level){
		sprays.Clear ();
	}
}
