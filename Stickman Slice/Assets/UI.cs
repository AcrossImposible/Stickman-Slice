using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
using TMPro;

public class UI : MonoBehaviour//, INonSkippableVideoAdListener
{

	[SerializeField] TMP_Text txt_score;
	[SerializeField] TMP_Text txt_level;
	[SerializeField] TMP_Text txt_experience;
	[SerializeField] GameObject p_GameOver;
	[SerializeField] GameObject txtGameOver;
	[SerializeField] Image[] healths;
	[SerializeField] GameObject p_Pause;
	public static TxtWarningWave txtWarningWave;
	[SerializeField] public Text txt_CountSaw;
	[SerializeField] CircularSaw circularSaw;
	[SerializeField] public GameObject btn_Saw;
	[SerializeField] Image fillRect;
	[SerializeField] Image fillRect2;
	[SerializeField] GameObject panelColorBlood;
	[SerializeField] public PoolObjects scoresAdditive;

	
	public static UI inst;
	public static bool gameOver = false;
	public static bool pause = false;
	GameObject blade;

	[SerializeField] GameObject[] blades;
	[SerializeField] GameObject[] boards;

	void Awake()
	{
		inst = this;
	}

	void Start()
	{
		Saves.LoadCoins();
		Saves.LoadBlades();
		Saves.LoadBoards();
		Saves.LoadSaw();
		Saves.LoadUseSaw();
		Saves.LoadColorBlood();

		txtWarningWave = FindObjectOfType<TxtWarningWave>();
		txtWarningWave.gameObject.SetActive(false);
		p_GameOver.SetActive(false);
		txtGameOver.SetActive(false);
		p_Pause.SetActive(false);
		panelColorBlood.SetActive(false);
		if (FindObjectOfType<Blade>() == false)
		{
			Instantiate(blades[Game.usedBlade]);
		}
		Instantiate(boards[Game.usedBoard]);
		gameOver = false;

		// Мозгоебство -----------------------
		var fields = panelColorBlood.GetComponent<ColorBloodManager>().GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
		Image im = (Image)fields[0].GetValue(panelColorBlood.GetComponent<ColorBloodManager>());
		Color c;
		if (Game.colorBlood == 0)
		{
			c = (Color)fields[1].GetValue(panelColorBlood.GetComponent<ColorBloodManager>());
		}
		else c = (Color)fields[2].GetValue(panelColorBlood.GetComponent<ColorBloodManager>());
		im.color = c;
		fields[0].SetValue(panelColorBlood.GetComponent<ColorBloodManager>(), im);
		// Мозгоебство окончено --------------------

		if (Menu.hideSaw)
			btn_Saw.SetActive(false);
		//AddSaw ();

		ConvertCountSaw();
		//Appodeal.setRewardedVideoCallbacks(this);
		//Appodeal.setNonSkippableVideoCallbacks (this);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Application.systemLanguage == SystemLanguage.Russian)
		{
			txt_score.text = "Очки:" + Mode_1.score;
		}
		else
		{
			txt_score.text = "Score:" + Mode_1.score;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			AddSaw();
			AddSaw();
			AddSaw();
			AddSaw();
			AddSaw();
			Saves.SaveSaw();
			ConvertCountSaw();
		}
	}

	public IEnumerator GameOver(){
		gameOver = true;
		txtGameOver.SetActive (true);
		yield return new WaitForSeconds (1.3f);
		txtGameOver.SetActive (false);
		p_GameOver.SetActive (true);
		gameOver = true;
		Game.exp += Mode_1.score;
		Game game = (Game)FindObjectOfType (typeof(Game));
		game.CalculateLvl ();
		if (Game.newLevel) {
			if (Application.systemLanguage == SystemLanguage.Russian) {
				txt_level.text = "Новый Уровень!:" + Game.lvl.ToString ();
			} else {
				txt_level.text = "Level Up !:" + Game.lvl.ToString ();
			}
			txt_level.color = new Color (0, 1, 0);
			txt_level.transform.parent.GetChild (1).gameObject.SetActive (false);
			Game.newLevel = false;
		} else {
			if (Application.systemLanguage == SystemLanguage.Russian) {
				txt_level.text = "Уровень:" + Game.lvl.ToString ();
			} else {
				txt_level.text = "Level:" + Game.lvl.ToString ();
			}
			txt_level.color = new Color (1, 1, 1);
			txt_level.transform.parent.GetChild (1).gameObject.SetActive (true);
		}
		if (Application.systemLanguage == SystemLanguage.Russian) {
			txt_experience.text = "Опыт:" + Mode_1.score.ToString ();
		} else {
			txt_experience.text = "Experience:" + Mode_1.score.ToString ();
		}
		fillRect.fillAmount = (float)(Game.expLocal-Mode_1.score) / (float)Game.leftExp;
		fillRect2.fillAmount = (float)Game.expLocal / (float)Game.leftExp;
		if (SceneManager.GetActiveScene ().name == "Mode 1") {
			Missions.countMode_1++;
		}

		Saves.SaveCoins ();
		Saves.SaveExp ();
		Saves.SaveLvl ();
		SaveStats ();

	}

	public void HealtChange(){
		switch (HealthManager.health) {
		case 2: {
				healths [2].color = new Color (0.3f, 0.1f, 0.1f);
				break;
			}
		case 1: {
				healths [1].color = new Color (0.3f, 0.1f, 0.1f);
				break;
			}
		case 0: {
				healths [0].color = new Color (0.3f, 0.1f, 0.1f);
				break;
			}
		}
	}

	public void Reset()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;

		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

		Advertising.ShowInterstitial();
	}

	public void OpenMenu()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		Menu.showMode_1 = true;

		SceneManager.LoadScene (0);

		Advertising.ShowInterstitial();

		//YandexSDK.instance.ShowInterstitial();
	}

	public void Pause(bool pause)
	{
		if (p_GameOver.activeSelf || panelColorBlood.activeSelf)
			return;

		if (!blade)
		{
			blade = FindObjectOfType<Blade>().gameObject;
		}
		blade.SetActive(!pause);
		Time.timeScale = pause ? 0 : 1;
		Time.fixedDeltaTime = pause ? 0.05f : 0.02f;
		p_Pause.SetActive(pause);
	}

	public void ConvertCountSaw(){
		string strSaw = Game.firstSaw + Game.secondSaw;
		txt_CountSaw.text = strSaw;
	}

	public void CircularSaw(){
		string strSaw = Game.firstSaw + Game.secondSaw;
		int countSaw = int.Parse (strSaw);
		if (countSaw > 0) {
			circularSaw.Run ();
			SubSaw ();
			MissionsSawUse msu = FindObjectOfType<MissionsSawUse> ();
			if (msu) {
				Missions.countSawUse++;
				Saves.SaveStat ("countSawUse", ref Missions.countSawUse);
			}
		}
		ConvertCountSaw ();
	}

	

	#region Non Skippable Video callback handlers
	public void onNonSkippableVideoFinished() {}
	public void onNonSkippableVideoFailedToLoad() {}
	public void onNonSkippableVideoShown() {}
	public void onNonSkippableVideoLoaded(bool isPrecache) {}
	public void onNonSkippableVideoExpired() {}
	public void onNonSkippableVideoClosed(bool finished) 
	{

	}
	#endregion

	public void AddSaw(){
		string strSaw = Game.firstSaw + Game.secondSaw;
		int countSaw = int.Parse (strSaw);
		countSaw++;
		strSaw = countSaw.ToString ();
		if (strSaw.Length > 1) {
			Game.firstSaw = strSaw.Substring (0, 1);
			Game.secondSaw = strSaw.Substring (1, strSaw.Length - 1);
		} else {
			Game.firstSaw = "";
			Game.secondSaw = strSaw.ToString ();
		}
		Saves.SaveSaw ();
		ConvertCountSaw ();
	}

	public void SubSaw(){
		string strSaw = Game.firstSaw + Game.secondSaw;
		int countSaw = int.Parse (strSaw);
		countSaw--;
		strSaw = countSaw.ToString ();
		if (strSaw.Length > 1) {
			Game.firstSaw = strSaw.Substring (0, 1);
			Game.secondSaw = strSaw.Substring (1, strSaw.Length - 1);
		} else {
			Game.firstSaw = "";
			Game.secondSaw = strSaw.ToString ();
		}
		Saves.SaveSaw ();
		ConvertCountSaw ();
	}

	public void OpenPanelColorBlood()
	{
		if (p_GameOver.activeSelf || p_Pause.activeSelf)
			return;

		panelColorBlood.SetActive(true);

		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;
	}

	void SaveStats() {
		Saves.SaveStat("countAllAnnihilation", ref Missions.countAllAnnihilation);
		Saves.SaveStat("countSaveLive", ref Missions.countSaveLive);
		Saves.SaveStat("countSliceHead", ref Missions.countSliceHead);
		Saves.SaveStat("countSliceHeadOfBlade_1", ref Missions.countSliceHeadOfBlade_1);
		Saves.SaveStat("countMode_1", ref Missions.countMode_1);
		Saves.SaveStat("countSliceArm", ref Missions.countSliceArm);
		Saves.SaveStat("countBlueSlice", ref Missions.countBlueSlice);
		Saves.SaveStat("countSaveLifeGhost", ref Missions.countSaveLifeGhost);
		Saves.SaveStat("countOutlastWave", ref Missions.countOutlastWave);
		Saves.SaveStat("countSliceHeadOfBlade_3", ref Missions.countSliceHeadOfBlade_3);
	}

	public void onNonSkippableVideoShowFailed()
	{
		throw new System.NotImplementedException();
	}
}
