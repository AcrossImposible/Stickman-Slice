using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using AppodealAds.Unity.Api;

public class MngrCuttingBoards : MonoBehaviour {

	[SerializeField] Text txt_Coins;
	[SerializeField] Text txt_Level;

	void Awake(){
		Saves.LoadBoards ();
		Saves.LoadCoins ();
		Saves.LoadExp ();
	}

	// Use this for initialization
	void Start () 
	{
		HandleBoards ();
		Advertising.ShowBanner();
	}

	// Update is called once per frame
	void Update () {
		txt_Coins.text = Game.coins.ToString ();
		txt_Level.text = Game.lvl.ToString ();
	}

	void HandleBoards(){
		int countBoards = transform.childCount-1;
		for (int i = 0; i < countBoards; i++) {
			if (Game.boards [i] == false) {
				if (Game.priceBoards [i] <= Game.coins) {
					transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.47f, 0.9f, 0.31f);
				} else {
					transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
					transform.GetChild (i).GetChild (4).GetComponent<Button> ().interactable = false;
					transform.GetChild (i).GetComponent<Image> ().color = new Color (0.28f, 0.28f, 0.28f, 0.7f);
				}
			} else if (Game.usedBoard == i) {
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.39f, 0.37f, 0.286f);
			} else {
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.286f, 0.286f, 0.286f);
			}
			if (Game.levelBoards [i] <= Game.lvl) {
				transform.GetChild (i).GetChild (5).gameObject.SetActive (false);
			} else {
				transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
				transform.GetChild (i).GetChild (4).GetComponent<Button> ().interactable = false;
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.28f, 0.28f, 0.28f, 0.7f);
			}
			if (Game.boards [i] == true) {
				transform.GetChild (i).GetChild (4).gameObject.SetActive (false);
			}
		}
	}

	public void OpenMode_1()
	{
		Advertising.HideBanner();
		SceneManager.LoadScene (1);
	}

	public void OpenMenu()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		Menu.showMode_1 = true;
		Advertising.HideBanner();
		SceneManager.LoadScene (0);
	}

	public void BuyBoard(int iBoard){
		Game.boards [iBoard] = true;
		Game.coins -= Game.priceBoards [iBoard];
		Game.usedBoard = iBoard;
		Saves.SaveCoins ();
		Saves.SaveBoards ();
		HandleBoards ();
	}

	public void Select(int iBoard){
		if (Game.boards [iBoard]) {
			Game.usedBoard = iBoard;
			HandleBoards ();
		}
		Saves.SaveBoards ();
	}
}
