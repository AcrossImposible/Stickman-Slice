using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionBlueSlice : Missions {

	[Space(10)]
	[SerializeField] int requiredBlueSlice;

	public override void Start ()
	{
		base.Start ();
	}

	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name == "Mode 1") {
			if (Missions.countBlueSlice >= this.requiredBlueSlice && !helper) {
				UI ui = FindObjectOfType<UI> ();
				transform.SetParent (ui.transform, false);
				transform.localPosition -= new Vector3 (-59, 10, 0);
				helper = true;
				StartCoroutine (TakeReward ());
			}
		}
	}

	public override void OnLevelWasLoaded (int level)
	{
		base.OnLevelWasLoaded (level);
	}

	public override IEnumerator TakeReward ()
	{
		Saves.SaveStat ("countBlueSlice", ref Missions.countBlueSlice);
		return base.TakeReward ();
	}
}
