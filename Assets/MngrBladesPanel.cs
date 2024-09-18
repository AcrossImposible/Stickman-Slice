using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using AppodealAds.Unity.Api;

public class MngrBladesPanel : MonoBehaviour {

	[SerializeField] Text txt_Coins;
	[SerializeField] Text txt_Level;

	int paddingVertical;

	void Awake()
	{
		Saves.LoadBlades ();
		Saves.LoadExp ();
		Saves.LoadCoins ();
	}
	// Use this for initialization
	void Start () {

		paddingVertical = Screen.height / 25;
		//GetComponent<HorizontalLayoutGroup> ().padding.top = paddingVertical;
		//GetComponent<HorizontalLayoutGroup> ().padding.bottom = paddingVertical;
		HandleBlades ();
		transform.parent.GetChild (1).GetComponent<Scrollbar> ().value = 0;

		Advertising.ShowBanner();
	}
	
	// Update is called once per frame
	void Update () {
		txt_Coins.text = Game.coins.ToString ();
		txt_Level.text = Game.lvl.ToString ();
	}

	void HandleBlades(){
		int widthElem = (int)(Screen.width / 4.5f);
		int countBlades = transform.childCount-1;
		for (int i = 0; i < countBlades; i++) {
			//transform.GetChild (i).GetComponent<LayoutElement> ().preferredWidth = widthElem;
			//print (Game.blades[i]);
			if (Game.blades [i] == false) {
				if (Game.priceBlades [i] <= Game.coins) {
					transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.47f, 0.9f, 0.31f);
				} else {
					transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
					transform.GetChild (i).GetChild (4).GetComponent<Button> ().interactable = false;
					transform.GetChild (i).GetComponent<Image> ().color = new Color (0.28f, 0.28f, 0.28f, 0.7f);
				}
			} else if (Game.usedBlade == i) {
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.39f, 0.37f, 0.286f);
			} else {
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.286f, 0.286f, 0.286f);
			}
			if (Game.levelBlades [i] <= Game.lvl) {
				transform.GetChild (i).GetChild (5).gameObject.SetActive (false);
			} else {
				transform.GetChild (i).GetChild (4).GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
				transform.GetChild (i).GetChild (4).GetComponent<Button> ().interactable = false;
				transform.GetChild (i).GetComponent<Image> ().color = new Color (0.28f, 0.28f, 0.28f, 0.7f);
			}
			if (Game.blades [i] == true) {
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

	public void BuyBlade(int iBlade)
	{
		Game.blades [iBlade] = true;
		Game.coins -= Game.priceBlades [iBlade];
		Game.usedBlade = iBlade;
		Saves.SaveCoins ();
		Saves.SaveBlades ();
		HandleBlades ();
	}

	public void Select(int iBlade){
		if (Game.blades [iBlade]) {
			Game.usedBlade = iBlade;
			HandleBlades ();
		}
		Saves.SaveBlades ();
	}
}
