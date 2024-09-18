using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode_1 : MonoBehaviour {

	public static int score = 0;

	[SerializeField] Transform[] targetPoints;

	static Mode_1 Inst { get; set; }

	private void Awake()
	{
		Inst = this;
	}

	void Start () 
	{
		score = 0;
	}
	
	public static Vector2 GetRandomPoint()
	{
		Vector2 v;

		if (Inst)
			v = Inst.targetPoints[Random.Range(0, Inst.targetPoints.Length)].position;
		else v = Game.CalculateTargetPoint();

		return v;
	}
	
}
