using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
using UnityEngine.Networking;

public class Menu : MonoBehaviour//, INonSkippableVideoAdListener 
{

	[SerializeField] Button btnPlayNext;

	[SerializeField] GameObject p_Modes;
	[SerializeField] GameObject p_Mode_1;
	[SerializeField] GameObject p_Mode_2;

	[SerializeField] Text txt_Coins;
	[SerializeField] Text txt_Level;

	[SerializeField] GameObject p_Reward;

	[SerializeField] Button btnSound;
	[SerializeField] GameObject noSound;

	[SerializeField] Image imageAd;
	[SerializeField] GameObject loading;
	[SerializeField] Button btnOpenAd;

	[Header("Refs For WebGL Fitter")]
	[SerializeField] GameObject[] androidObjects;
	[SerializeField] GameObject[] webGL_Objects;

	public static bool showMode_1 = false;
	public static bool hideSaw = true;
	public static bool showReward = false;
	public static bool soundEnabled = true;
	public static bool loaded;

	static GameObject reward;
	
	static readonly List<Sprite> sprites = new List<Sprite>();

	void Start()
	{
		loading.SetActive(false);

		p_Modes.SetActive(true);
		p_Mode_1.SetActive(false);
		p_Mode_2.SetActive(false);

		if (showMode_1)
		{
			OpenMode_1();
			showMode_1 = false;
		}

		reward = p_Reward;

		//Appodeal.setNonSkippableVideoCallbacks(this);

		Saves.LoadCoins();
		Saves.LoadBlades();
		Saves.LoadBoards();
		Saves.LoadSaw();
		Saves.LoadExp();
		Saves.LoadLvl();
		Saves.LoadCompleteMissions();
		Saves.LoadStat("countAllAnnihilation", ref Missions.countAllAnnihilation);
		Saves.LoadStat("countSaveLive", ref Missions.countSaveLive);
		Saves.LoadStat("countSliceHead", ref Missions.countSliceHead);
		Saves.LoadStat("countSliceHeadOfBlade_1", ref Missions.countSliceHeadOfBlade_1);
		Saves.LoadStat("countMode_1", ref Missions.countMode_1);
		Saves.LoadStat("countSliceArm", ref Missions.countSliceArm);
		Saves.LoadStat("countSawUse", ref Missions.countSawUse);
		Saves.LoadStat("countBlueSlice", ref Missions.countBlueSlice);
		Saves.LoadStat("countSaveLifeGhost", ref Missions.countSaveLifeGhost);
		Saves.LoadStat("countOutlastWave", ref Missions.countOutlastWave);
		Saves.LoadStat("countSliceHeadOfBlade_3", ref Missions.countSliceHeadOfBlade_3);

		btnOpenAd.onClick.AddListener(OpenAd);
		btnPlayNext.onClick.AddListener(OpenMode_1);
		btnSound.onClick.AddListener(BtnSound_Clicked);

#if UNITY_WEBGL
		OpenMode_1();
		foreach (var obj in androidObjects)
        {
			obj.SetActive(false);
        }
		foreach (var obj in webGL_Objects)
		{
			obj.SetActive(true);
		}
#else
		foreach (var obj in androidObjects)
		{
			obj.SetActive(true);
		}
		foreach (var obj in webGL_Objects)
		{
			obj.SetActive(false);
		}

		if (!loaded)
			StartCoroutine(LoadBundle());
		else
			StartCoroutine(SpriteSwitch());
#endif

	}

	private void BtnSound_Clicked()
    {
		noSound.SetActive(soundEnabled);
		soundEnabled = !soundEnabled;
    }

    void FixedUpdate()
	{
		txt_Coins.text = Game.coins.ToString();
		txt_Level.text = Game.lvl.ToString();

		if (Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
		}
	}


    public void Mode_1()
	{
		Missions[] mis = (Missions[])FindObjectsOfType (typeof(Missions));
		foreach (Missions m in mis) {
			m.transform.SetParent (null, true);
			m.DontDestr ();
			m.sizeDelta = m.GetComponent<RectTransform>().sizeDelta;
			m.position = m.GetComponent<RectTransform>().position;
		}
		SceneManager.LoadScene (1);
	}

	public void OpenMode_1(){
		p_Mode_1.SetActive (true);
		p_Modes.SetActive (false);
	}

	public void OpenMode_2(){
		p_Mode_2.SetActive (true);
		p_Modes.SetActive (false);
	}

	public void OpenModes(){
		p_Mode_2.SetActive (false);
		p_Mode_1.SetActive (false);
		p_Modes.SetActive (true);
	}

	public void OpenBlades(){
		Missions[] mis = (Missions[])FindObjectsOfType (typeof(Missions));
		foreach (Missions m in mis) {
			m.transform.SetParent (null, false);
			m.DontDestr ();
		}
		SceneManager.LoadScene ("Blades");
	}

	public void OpenBoards(){
		Missions[] mis = (Missions[])FindObjectsOfType (typeof(Missions));
		foreach (Missions m in mis) {
			m.transform.SetParent (null, false);
			m.DontDestr ();
		}
		SceneManager.LoadScene ("Boards");
	}

	public void Exit(){
		Application.Quit ();
	}

	public void PrivacyPolicy(){
		Application.OpenURL ("https://wogergames.github.io/StickmanSlice/");
	}

	private void OpenAd()
    {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.OtherStickmanGames.StickmanPunch");
    }

	public void ShowReward()
	{
		reward.SetActive(true);
		StartCoroutine(HideReward());
	}

	IEnumerator HideReward(){
		reward.SetActive (true);
		yield return new WaitForSeconds (3.8f);
		reward.SetActive (false);
		showReward = false;
	}

	public void AddSaw()
	{
		string strSaw = Game.firstSaw + Game.secondSaw;
		int countSaw = int.Parse(strSaw);
		countSaw++;
		strSaw = countSaw.ToString();
		if (strSaw.Length > 1)
		{
			Game.firstSaw = strSaw.Substring(0, 1);
			Game.secondSaw = strSaw.Substring(1, strSaw.Length - 1);
		}
		else
		{
			Game.firstSaw = "";
			Game.secondSaw = strSaw.ToString();
		}
		Saves.SaveSaw();
	}

#region Non Skippable Video callback handlers
	public void onNonSkippableVideoFinished() { }
	public void onNonSkippableVideoFailedToLoad() { }
	public void onNonSkippableVideoShown() { }
	public void onNonSkippableVideoLoaded(bool isPrecache) { }
	public void onNonSkippableVideoExpired() { }
	public void onNonSkippableVideoClosed(bool finished)
	{
		showReward = true;
		AddSaw();
		ShowReward();
	}
#endregion

	public virtual void OnLevelWasLoaded(int level){
		reward = p_Reward;
	}

	public void onNonSkippableVideoShowFailed()
	{
		throw new System.NotImplementedException();
	}

	private IEnumerator SpriteSwitch()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
			imageAd.sprite = sprites[i];

			yield return new WaitForSeconds(0.03f);
        }

		StartCoroutine(SpriteSwitch());
	}

	private IEnumerator LoadBundle()
    {
		loading.SetActive(true);

		sprites.Clear();

		string path = "https://drive.google.com/uc?export=download&id=1v7NSc4ELcaI9qPla5XmnGIBSzFx2AcsM";

		var request = UnityWebRequestAssetBundle.GetAssetBundle(path);

		var async = request.SendWebRequest();

		yield return async;

		var assetBundle = ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;

		foreach (var name in assetBundle.GetAllAssetNames())
        {
			var spriteRequest = assetBundle.LoadAssetAsync(name, typeof(Sprite));

			yield return spriteRequest;

			sprites.Add((Sprite)spriteRequest.asset);
		}

		loading.SetActive(false);

		StartCoroutine(SpriteSwitch());

		loaded = true;
	}
}
