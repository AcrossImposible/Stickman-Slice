using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCountMode_1 : Missions {

	[SerializeField] int requiredMode_1;

	public override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Missions.countMode_1 >= requiredMode_1 && !helper)
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
		Missions.countMode_1 = 0;
		Saves.SaveStat ("countMode_1", ref Missions.countMode_1);
		return base.TakeReward ();

	}
}
