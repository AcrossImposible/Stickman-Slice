using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCountAnnihilation : Missions {

	[Space]

	[SerializeField] int requiredAnnihilation;

	new int countAnnihilation = 0;


	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Missions.countAnnihilation >= countAnnihilation && !helper)
			{
				ShowInHud();

				helper = true;
				StartCoroutine(TakeReward());
			}
		}
	}

	public override void OnLevelWasLoaded(int level)
	{
		base.OnLevelWasLoaded(level);
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			countAnnihilation = requiredAnnihilation;
			countAnnihilation += Missions.countAnnihilation;
		}
	}

}
