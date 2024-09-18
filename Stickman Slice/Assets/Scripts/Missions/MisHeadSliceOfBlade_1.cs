using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MisHeadSliceOfBlade_1 : Missions {

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
			if (Game.usedBlade == 0)
			{
				if (Missions.countSliceHeadOfBlade_1 >= requiredHeadSlice && !helper)
				{
					ShowInHud();

					helper = true;
					StartCoroutine(this.TakeReward());
				}
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
		Missions.countSliceHeadOfBlade_1 = 0;
		Game.countCompleteMissions++;
		Saves.SaveCompleteMissions ();
		Saves.SaveStat ("countSliceHeadOfBlade_1", ref Missions.countSliceHeadOfBlade_1);
		Saves.SaveCoins ();
		yield return new WaitForSeconds (1.8f);

		Destroy (gameObject);

	}
}
