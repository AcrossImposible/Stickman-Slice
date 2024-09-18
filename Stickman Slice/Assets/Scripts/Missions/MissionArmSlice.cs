using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionArmSlice : Missions {

	[Space(18)]
	[SerializeField] int requiredArmSlice;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Missions.countSliceArm >= requiredArmSlice && !helper)
			{
				ShowInHud();

				helper = true;
				StartCoroutine(TakeReward());
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
		Missions.countSliceArm = 0;
		Saves.SaveStat ("countSliceArm", ref Missions.countSliceArm);
		Saves.SaveCoins ();
		yield return new WaitForSeconds (1.8f);

		Destroy (gameObject);

	}
}
