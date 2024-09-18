using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Blade_1 : Blade {

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

    void Update()
	{
		var uiObject = EventSystem.current.currentSelectedGameObject;

		if (Input.GetMouseButtonDown(0))
		{
			if (uiObject && uiObject.layer == 5)
			{
				StartCoroutine(Delay());
				return;
			}
			//oldPoint = null;
		}

		if (Time.timeScale < 0.1 || UI.gameOver)
		{
			return;
		}

		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // переменной записываются координаты мыши по иксу и игрику
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваивается переменная с координатами мыши
		transform.position = objPosition;// и собственно объекту записываються координаты

		if (!Input.GetMouseButton(0))
		{
			trail.Clear();
		}

		if (Input.GetMouseButton(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
			GameObject circleClone = Instantiate(circle, pos, Quaternion.identity);
			if (oldPoint)
			{
				Vector3 dirRay;
				if (newPoint)
				{
					dirRay = newPoint.position - oldPoint.position;
					//Debug.DrawRay (oldPoint.position, dirRay, new Color (0, 1f, 0.5f), 15);
					oldPoint = newPoint;
					newPoint = circleClone.transform;
				}
				else
				{
					newPoint = circleClone.transform;
					dirRay = newPoint.position - oldPoint.position;
					//Debug.DrawRay (oldPoint.position, dirRay, new Color (0, 1f, 0.5f), 15);
				}
				float dist = Vector3.Distance(oldPoint.position, newPoint.position);
				RaycastHit2D[] hit = Physics2D.RaycastAll(oldPoint.position, dirRay, dist * 1.88f);
				if (hit.Length > 0)
				{

					if (hit[0].transform.CompareTag("MEAT"))
					{
						Vector3 targetDirection = oldPoint.position - newPoint.position;
						float rotZ = Vector3.Angle(Vector3.right, targetDirection);
						Instantiate(bloodSlice[Game.colorBlood], hit[0].point, Quaternion.Euler(0, 0, rotZ));
						hit[0].transform.GetComponent<StickPart>().Slice();
						hit[0].transform.parent.GetComponent<ScriptManager>().OnHit();
					}

					for (int i = 0; i < hit.Length; i++)
					{
						if (hit[i].transform.CompareTag("MEAT"))
						{
							Blade1(hit, i);
							hit[i].transform.GetComponent<TouchDetect>().Damage();
							hit[i].transform.parent.GetComponent<ScriptManager>().sliced = true;

						}
					}

				}
			}
			else
			{
				oldPoint = circleClone.transform;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			oldPoint = null;
		}
	}

	void Blade1(RaycastHit2D[] hit, int i) {
		RaycastHit2D hitLocal = hit[i];
		Transform parent = hit [i].transform.parent;
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}
		ScriptManager sm = parent.GetComponent<ScriptManager> ();
		sm.countSlice++;
		if (sm.countSlice >= 1) {
			for (int j = 0; j < parent.childCount; j++) {
				parent.GetChild (j).tag = "USED";
			}
		} else {
			hit [i].transform.tag = "USED";
		}

		//hit[i].transform.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 0, 0);
		Instantiate (pusher, hit[i].point, Quaternion.identity);

		Effects (hit[i].point);



		/*RaycastHit2D hitLocal = hit[i];
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}

		for (int j = 0; j < hit [i].transform.parent.childCount; j++) {
			hit [i].transform.parent.GetChild (j).tag = "USED";
		}
	
		Instantiate (pusher, hit[i].point, Quaternion.identity);

		if (hitLocal.transform.parent.GetComponent<ScriptManager> ().sliced == false) {
		 
			Vector3 targetDirection = oldPoint.position - newPoint.position;
			float rotZ = Vector3.Angle (Vector3.right, targetDirection);
			Instantiate (bloodSlice[Game.colorBlood], hitLocal.point, Quaternion.Euler (0, 0, rotZ));
			hitLocal.transform.GetComponent<StickPart> ().Slice ();

			Effects (hit[i].point);
		}/**/
	}
		

	private void LookAt(Vector3 t)
	{
		var dir = t - transform.position;
		var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.AngleAxis(angle, transform.up);
	}

	/*public void Effects(Vector3 posSpray) {
		foreach (SoundSlice ss in SoundSlice.slices) {
			if (ss.use)
				continue;
			else {
				StartCoroutine (ss.PlaySlice ());
				break;
			}
		}
		foreach (Spray s in Spray.sprays) {
			if (s.use)
				continue;
			else {
				s.PLay (posSpray);
				break;
			}
		}
	}/**/


}
