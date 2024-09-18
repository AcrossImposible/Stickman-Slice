using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngrMissions : MonoBehaviour {

	//[SerializeField] Missions[] missions;
	[SerializeField] public List<Missions> missions = new List<Missions>();
	public static List<Missions> _missions = new List<Missions>();
	static bool oneStart = false;
	public static MngrMissions instance;
	//[SerializeField] 
	// Use this for initialization

	void Awake()
	{
		if (_missions.Count > 0)
		{
			missions = _missions;
		}
		else if (!oneStart)
		{
			_missions = missions;
			Saves.LoadCompleteMissions();
			//print (Game.countCompleteMissions);
			for (int i = 0; i < Game.countCompleteMissions; i++)
			{
				_missions.RemoveAt(0);
			}
			oneStart = true;
		}
		instance = this;
	}

	IEnumerator Start() {
		missions = _missions;
		yield return null;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.V)) {
			missions.GetEnumerator().MoveNext();
		}
	}

	void OnEnable() {
		StartCoroutine(FillGrid());
	}

	IEnumerator FillGrid()
	{
		yield return null;
		int countNewMis = 3 - transform.childCount;
		if (_missions.Count < countNewMis)
		{
			countNewMis = _missions.Count;
		}
		//print (countNewMis);
		for (int i = 0; i < countNewMis; i++)
		{
			Instantiate(_missions[0], transform);
			_missions.Remove(_missions[0]);
			yield return null;
		}
	}
}
