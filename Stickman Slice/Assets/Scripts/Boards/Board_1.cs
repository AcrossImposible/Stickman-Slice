using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board_1 : MonoBehaviour {

	[SerializeField] Sprite board;
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindWithTag ("BOARD");
		go.GetComponent<SpriteRenderer> ().sprite = board;
		go.GetComponent<SpriteRenderer> ().drawMode = SpriteDrawMode.Simple;
	}

}
