using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCountOutlast : Missions {

	[SerializeField] int requiredOutlastWave;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name == "Mode 1") {
			if (Missions.countOutlastWave >= requiredOutlastWave && !helper) {
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
		Missions.countOutlastWave = 0;
		Saves.SaveStat ("countOutlastWave", ref Missions.countOutlastWave);
		return base.TakeReward ();

	}
}
