using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static int exp = 0;
	public static int expLocal = 0;
	public static int lvl = 1;
	public static int coins = 0;
	public static int leftExp = 0;
	public static bool[] blades = {
		true,
		false,
		false, 
		false, };
	public static int[] priceBlades = {
		0,
		380,
		1888, 
		1999, };
	public static int[] levelBlades = {
		1,
		2,
		4, 
		4, };
	public static bool[] boards = {
		true,
		false,
		false, };
	public static int[] priceBoards = {
		0,
		500,
		1500, };
	public static int[] levelBoards = {
		1,
		2,
		3, };
	public static int usedBlade = 0;
	public static int usedBoard = 0;
	public static int countCompleteMissions = 0;
	public static bool newLevel = false;
	public static string firstSaw = "";
	public static string secondSaw = "5";
	public static int colorBlood = 0;

	public static Vector2 TargetPoint { get; private set; }

	public void Awake()
	{
		TargetPoint = CalculateTargetPoint();
	}

	// Use this for initialization
	void Start () 
	{
		Saves.LoadExp ();
		Saves.LoadLvl ();
		CalculateLvl ();
	}

	public void CalculateLvl(){
		int oldLvl = lvl;
		int experience = exp;
		int origin = 188;
		lvl = 1;
		while (experience >= origin) {
			experience -= origin;
			origin = (int)Mathf.Pow (origin, 1.198f);
			lvl++;
		}
		leftExp = origin;
		if (lvl > oldLvl)
			newLevel = true;
		expLocal = experience;
	}

	public static Vector2 CalculateTargetPoint()
	{
		//Vector2 screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.68f);
		Vector2 screenPoint = new Vector2(Screen.width * Random.Range(0.35f, 0.65f), Screen.height * 0.68f);
		Vector2 target = Camera.main.ScreenToWorldPoint(screenPoint);

		return target;
	}
}
