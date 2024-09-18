using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public abstract class Missions : MonoBehaviour {

	public static int countAnnihilation = 0;
	public static int countSaveLive = 0;
	public static int countSliceHead = 0;
	public static int countSliceHeadOfBlade_1 = 0;
	public static int countMode_1 = 0;
	public static int countAllAnnihilation = 0;
	public static int countSliceArm = 0;
	public static int countSawUse = 0;
	public static int countBlueSlice = 0;
	public static int countSaveLifeGhost = 0;
	public static int countOutlastWave = 0;
	public static int countSliceHeadOfBlade_3 = 0;

	[SerializeField] public int reward;
	[SerializeField] public string description = "";
	[HideInInspector] public bool helper;
	[HideInInspector] public int indx;
	[HideInInspector] public Vector2 sizeDelta;
	[HideInInspector] public Vector3 position;

	// Use this for initialization
	public virtual void Start()
	{
		transform.GetChild(0).GetChild(0).GetComponent<Text>().text = reward.ToString();
		transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = description;
		indx = transform.GetSiblingIndex();
	}

	public void DontDestr(){
		DontDestroyOnLoad (this);
		//print ("gdf - "+GetComponent<RectTransform>().sizeDelta.ToString());

	}

	public void SetDeltaSize()
	{
		//GetComponent<RectTransform> ().sizeDelta = sizeDelta;
		//GetComponent<RectTransform> ().position = position;
	}

	public virtual void OnLevelWasLoaded(int level){
		if (level == 0) {
			StartCoroutine (SetParent ());
		}
	}

	IEnumerator SetParent()
	{
		yield return null;

		transform.SetParent(MngrMissions.instance.transform);
		transform.localScale = Vector3.one;
	}


	public virtual void ShowInHud()
    {
		UI ui = FindObjectOfType<UI>();
		transform.SetParent(ui.transform);
		transform.localScale = Vector3.one;
		var rectT = transform.GetComponent<RectTransform>();
		
		rectT.anchoredPosition = new Vector2(960, -200);
	}

	public virtual IEnumerator TakeReward()
	{
		//SetDeltaSize ();
		Game.coins += reward;
		Game.countCompleteMissions++;
		Saves.SaveCompleteMissions();
		Saves.SaveCoins();

		yield return new WaitForSeconds(2.1f);

		Destroy(gameObject);
	}
}
