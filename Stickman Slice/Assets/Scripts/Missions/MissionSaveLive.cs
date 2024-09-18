using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSaveLive : Missions {

	[Space(18)]
	[SerializeField] int requiredSaveLive;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Missions.countSaveLive >= requiredSaveLive && !helper)
			{
				ShowInHud();

				helper = true;
				StartCoroutine(this.TakeReward());
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
		Saves.SaveStat ("countSaveLive", ref Missions.countSaveLive);
		Missions.countSaveLive = 0;
		yield return new WaitForSeconds (1.8f);

		Destroy (gameObject);

	}
}
