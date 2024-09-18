using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	public static int health;
	GameObject stck;
	[SerializeField] Spawner[] spawners;
	[SerializeField] Color blood1particle1;
	[SerializeField] Color blood2particle2;
	[Space(18)]
	[SerializeField] Color blood1particle2;
	[SerializeField] Color blood2particle22;
	[Space(18)]
	[SerializeField] Color blood1particle3;
	[SerializeField] Color blood2particle3;
	[Space(18)]
	[SerializeField] Color blood1Burst1;
	//[SerializeField] Color blood2particle3;
	UI ui;

	// Use this for initialization
	void Start () {
		health = 3;
		ui = (UI)FindObjectOfType (typeof(UI));
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (!col.transform.parent)
			return;
		if (stck == null || stck != col.transform.parent.gameObject) {
			stck = col.transform.parent.gameObject;
			if (col.CompareTag("MEAT")) 
			{
				for (int i = 0; i < col.transform.parent.childCount; i++) {
					if (col.transform.parent.GetChild(i).CompareTag("USED"))
					{
						col.GetComponent<Rigidbody2D>().simulated = false;
						return;
					}
					else if (col.transform.parent.GetComponent<ScriptManager>().sliced)
					{
						col.GetComponent<Rigidbody2D>().simulated = false;
						return;
					}
					if (col.transform.parent.GetChild (i).GetComponent<BoxCollider2D> ())
						col.transform.parent.GetChild (i).GetComponent<BoxCollider2D> ().enabled = false;
					else
						col.transform.parent.GetChild (i).GetComponent<CircleCollider2D> ().enabled = false;
				}
				health--;
				ui.HealtChange ();
				MissionsChange (col.GetComponentInParent<ScriptManager>());
			}
			if (health == 0) {
				GetComponent<BoxCollider2D> ().enabled = false;
				foreach (Spawner s in spawners) {
					s.Disable ();
				}
				StartCoroutine(ui.GameOver());
			} 
		}

	}

	void MissionsChange(ScriptManager stickman){
		MissionSaveLive msl = FindObjectOfType<MissionSaveLive> ();
		MissionSaveLifeGhost mslg = FindObjectOfType<MissionSaveLifeGhost> ();
		if (msl) {
			Missions.countSaveLive++;
		}
		if (mslg && stickman.isGhost) {
			Missions.countSaveLifeGhost++;
		}
	}
}
