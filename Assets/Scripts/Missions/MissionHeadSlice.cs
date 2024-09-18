using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionHeadSlice : Missions {

	[Space(18)]
	[SerializeField] int requiredHeadSlice;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Missions.countSliceHead >= requiredHeadSlice && !helper)
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
		Missions.countSliceHead = 0;
		Saves.SaveStat ("countSliceHead", ref Missions.countSliceHead);
		yield return new WaitForSeconds (1.8f);

		Destroy (gameObject);

	}
}
