using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBlood : MonoBehaviour {

	[SerializeField] GameObject[] blood;
	[SerializeField] int countBlood = 15;
	static PoolBlood instance;

	// Use this for initialization
	void Awake () {
		instance = this;
		FillPool ();

	}
	
	void OnLevelWasLoaded(int level){
		FillPool ();
	}

	public static void FillPool(){
		Saves.LoadColorBlood ();
		for (int i = 0; i < instance.countBlood; i++) {
			GameObject go = Instantiate (instance.blood [Game.colorBlood], instance.transform);
			go.transform.position = Vector3.zero;
		}	
	}
}
