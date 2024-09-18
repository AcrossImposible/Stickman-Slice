using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionExpCount : Missions {

	[Space(10)]
	[SerializeField] int requiredExp;

	public override void Start ()
	{
		base.Start ();
	}

	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name == "Mode 1")
		{
			if ((Game.exp + Mode_1.score) >= requiredExp && !helper)
			{
				ShowInHud();
				
				helper = true;
				StartCoroutine(TakeReward());
			}
		}
	}

	
}
