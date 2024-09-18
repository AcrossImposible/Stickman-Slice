using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Blade_3 : Blade {

	[SerializeField] GameObject circle;
	[SerializeField] GameObject pusher;
	Transform oldPoint, newPoint;
	TrailRenderer trail;

	private void Start()
	{
		trail = GetComponent<TrailRenderer>();
		trail.Clear();
	}

	IEnumerator Delay()
	{
		for (int i = 0; i < 3; i++)
		{
			trail.Clear();
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		var uiObject = EventSystem.current.currentSelectedGameObject;

		if (Input.GetMouseButtonDown(0))
		{
			if (uiObject && uiObject.layer == 5)
			{
				StartCoroutine(Delay());
				return;
			}
		}

		if (Time.timeScale < 0.1 || UI.gameOver)
		{
			return;
		}

		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // переменной записываються координаты мыши по иксу и игрику
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
		transform.position = objPosition;// + new Vector3(0,0,10); // и собственно объекту записываються координаты

		if (!Input.GetMouseButton(0))
		{
			trail.Clear();
		}

		//if (Input.GetMouseButton (0)) {
		//	Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // переменной записываються координаты мыши по иксу и игрику
		//	Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
		//	transform.position = objPosition;// + new Vector3(0,0,10); // и собственно объекту записываються координаты
		//}

		if (Input.GetMouseButton (0)) {

			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition +new Vector3(0,0,10));
			GameObject circleClone = (GameObject) Instantiate(circle,pos, Quaternion.identity);
			if (oldPoint) {
				Vector3 dirRay;
				if (newPoint) {
					dirRay = newPoint.position - oldPoint.position;
					Debug.DrawRay (oldPoint.position, dirRay, new Color (0, 1f, 0.5f), 15);
					oldPoint = newPoint;
					newPoint = circleClone.transform;
				} else {
					newPoint = circleClone.transform;
					dirRay = newPoint.position - oldPoint.position;
					Debug.DrawRay (oldPoint.position, dirRay, new Color (0, 1f, 0.5f), 15);
				}
				float dist = Vector3.Distance (oldPoint.position, newPoint.position);
				RaycastHit2D[] hit = Physics2D.RaycastAll (oldPoint.position, dirRay, dist*1.8f);
				if (hit.Length > 0 ) {
					if (hit[0].transform.tag == "MEAT") {
						Vector3 targetDirection = oldPoint.position - newPoint.position;
						float rotZ = Vector3.Angle (Vector3.right, targetDirection);
						Instantiate (bloodSlice[Game.colorBlood], hit [0].point, Quaternion.Euler (0, 0, rotZ));
						hit [0].transform.GetComponent<StickPart> ().Slice ();
						hit[0].transform.parent.GetComponent<ScriptManager>().OnHit();
					}

					for (int i = 0; i < hit.Length; i++) {
						if(hit[i].transform.tag == "MEAT"){
							Blade1 (hit, i);
							hit [i].transform.GetComponent<TouchDetect> ().Damage ();
							hit [i].transform.parent.GetComponent<ScriptManager> ().sliced = true;



						}
					}

				}
			} else {
				oldPoint = circleClone.transform;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			oldPoint = null;
			newPoint = null;
		}
	}


	void Blade1(RaycastHit2D[] hit, int i){
		RaycastHit2D hitLocal = hit[i];
		Transform parent = hit [i].transform.parent;
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}
		ScriptManager sm = parent.GetComponent<ScriptManager> ();
		sm.countSlice++;
		if (sm.countSlice >= 3) {
			for (int j = 0; j < parent.childCount; j++) {
				parent.GetChild (j).tag = "USED";
			}
		} else {
			hit [i].transform.tag = "USED";
		}

		//hit[i].transform.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 0, 0);
		Instantiate (pusher, hit[i].point, Quaternion.identity);

		Effects (hit[i].point);

		/*Vector3 targetDirection = oldPoint.position - newPoint.position;
		float rotZ = Vector3.Angle (Vector3.right, targetDirection);
		Instantiate (bloodSlice, hit [i].point, Quaternion.Euler(0,0,rotZ));
		hitLocal.transform.GetComponent<StickPart> ().Slice ();/**/
	}


}
