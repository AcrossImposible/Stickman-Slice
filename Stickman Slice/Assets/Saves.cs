using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour {


	public static void SaveCoins(){
		PlayerPrefs.SetInt ("coins", Game.coins);
		PlayerPrefs.Save ();
	}

	public static void LoadCoins()
	{
		if (!PlayerPrefs.HasKey("coins"))
			return;
		Game.coins = PlayerPrefs.GetInt("coins");
	}

	public static void SaveBlades(){
		string strBlades = "";
		for (int i = 0; i < Game.blades.Length; i++) {
			strBlades += string.Format ("{0};", Game.blades [i]);
		}
		PlayerPrefs.SetString ("blades", strBlades);
		PlayerPrefs.SetInt ("usedBlade", Game.usedBlade);
		PlayerPrefs.Save ();
	}
		

	public static void LoadBlades(){
		if (!PlayerPrefs.HasKey ("blades"))
			return;
		string strBlades = PlayerPrefs.GetString ("blades");
		if (strBlades.Length == 0)
			return;
		string[] itemsData = strBlades.Split (new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < itemsData.Length; i++) {
			Game.blades [i] = bool.Parse (itemsData [i]);
		}
		if (!PlayerPrefs.HasKey ("usedBlade"))
			return;
		Game.usedBlade = PlayerPrefs.GetInt ("usedBlade");
	}

	public static void SaveBoards(){
		string strBoards = "";
		for (int i = 0; i < Game.boards.Length; i++) {
			strBoards += string.Format ("{0};", Game.boards [i]);
		}
		PlayerPrefs.SetString ("boards", strBoards);
		PlayerPrefs.SetInt ("usedBoard", Game.usedBoard);
		PlayerPrefs.Save ();
	}

	public static void LoadBoards(){
		if (!PlayerPrefs.HasKey ("boards"))
			return;
		string strBoards = PlayerPrefs.GetString ("boards");
		if (strBoards.Length == 0)
			return;
		string[] itemsData = strBoards.Split (new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < itemsData.Length; i++) {
			Game.boards [i] = bool.Parse (itemsData [i]);
		}
		if (!PlayerPrefs.HasKey ("usedBoard"))
			return;
		Game.usedBoard = PlayerPrefs.GetInt ("usedBoard");
	}

	public static void SaveExp(){
		PlayerPrefs.SetInt ("exp", Game.exp);
		PlayerPrefs.Save ();
	}

	public static void LoadExp(){
		if (!PlayerPrefs.HasKey ("exp"))
			return;
		Game.exp = PlayerPrefs.GetInt ("exp");
	}

	public static void SaveLvl(){
		PlayerPrefs.SetInt ("lvl", Game.lvl);
		PlayerPrefs.Save ();
	}

	public static void LoadLvl(){
		if (!PlayerPrefs.HasKey ("lvl"))
			return;
		Game.lvl = PlayerPrefs.GetInt ("lvl");
	}

	public static void SaveCompleteMissions(){
		PlayerPrefs.SetInt ("countMis", Game.countCompleteMissions);
		PlayerPrefs.Save ();
	}

	public static void LoadCompleteMissions(){
		if (!PlayerPrefs.HasKey ("countMis"))
			return;
		Game.countCompleteMissions = PlayerPrefs.GetInt ("countMis");
	}

	public static void SaveSaw(){
		PlayerPrefs.SetString ("firstSaw", Game.firstSaw);
		PlayerPrefs.SetString ("secondSaw", Game.secondSaw);
		PlayerPrefs.Save();
	}

	public static void LoadSaw(){
		if(!PlayerPrefs.HasKey("secondSaw"))
			return;
		Game.firstSaw = PlayerPrefs.GetString ("firstSaw");
		Game.secondSaw = PlayerPrefs.GetString ("secondSaw");
	}

	public static void SaveStat(string name, ref int value){
		PlayerPrefs.SetInt (name, value);
		PlayerPrefs.Save ();
	}

	public static void LoadStat(string name, ref int value){
		if (!PlayerPrefs.HasKey (name))
			return;
		value = PlayerPrefs.GetInt (name);
	}

	public static void SaveUseSaw(){
		PlayerPrefs.SetString ("hideSaw", Menu.hideSaw.ToString());
		PlayerPrefs.Save();
	}

	public static void LoadUseSaw(){
		if (!PlayerPrefs.HasKey ("hideSaw"))
			return;
		Menu.hideSaw = bool.Parse(PlayerPrefs.GetString ("hideSaw"));
	}

	public static void SaveColorBlood(){
		PlayerPrefs.SetInt ("colorBlood", Game.colorBlood);
		PlayerPrefs.Save ();
	}

	public static void LoadColorBlood(){
		if (!PlayerPrefs.HasKey ("colorBlood"))
			return;
		Game.colorBlood = PlayerPrefs.GetInt ("colorBlood");
	}
}
