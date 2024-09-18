using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MisCountLevel : Missions {

	[Space(10)]
	[SerializeField] int requiredLevel;

	public override void Start ()
	{

		base.Start ();
	}

	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if (Game.lvl >= requiredLevel && !helper)
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
		return base.TakeReward ();
	}
}
