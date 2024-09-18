using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board_2 : MonoBehaviour {

	[SerializeField] Sprite board;
	[SerializeField] GameObject boardBonus;
	UI ui;
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindWithTag ("BOARD");
		go.GetComponent<SpriteRenderer> ().sprite = board;
		go.GetComponent<SpriteRenderer> ().drawMode = SpriteDrawMode.Sliced;
		ui = FindObjectOfType<UI> ();
		StartCoroutine (CheckGame ());
	}

	IEnumerator CheckGame()
	{
		if (UI.gameOver)
		{
			Game.coins += 5;
			Instantiate(boardBonus, ui.transform);
			UI.gameOver = false;
		}
		else
		{
			yield return new WaitForSeconds(1);
			StartCoroutine(CheckGame());
		}
	}
}
