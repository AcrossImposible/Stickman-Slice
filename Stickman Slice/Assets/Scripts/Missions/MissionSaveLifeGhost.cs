using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSaveLifeGhost : Missions {

	[Space(18)]
	[SerializeField] int requiredSaveLiveGhost;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name == "Mode 1") {
			if (Missions.countSaveLifeGhost >= requiredSaveLiveGhost && !helper) {
				UI ui = FindObjectOfType<UI> ();
				transform.SetParent (ui.transform, false);
				transform.localPosition -= new Vector3 (-59, 10, 0);
				helper = true;
				StartCoroutine (this.TakeReward ());
			}
		}
	}

	public override void OnLevelWasLoaded (int level)
	{
		base.OnLevelWasLoaded (level);
	}

	public override IEnumerator TakeReward ()
	{
		SetDeltaSize ();
		Game.coins += reward;
		Game.countCompleteMissions++;
		Saves.SaveCompleteMissions ();
		Saves.SaveCoins ();
		Saves.SaveStat ("countSaveLifeGhost", ref Missions.countSaveLifeGhost);
		Missions.countSaveLifeGhost = 0;
		yield return new WaitForSeconds (1.8f);

		Destroy (gameObject);

	}
}
