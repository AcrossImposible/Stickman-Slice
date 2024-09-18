using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Blade_4 : Blade {

	[SerializeField] GameObject circle;
	[SerializeField] GameObject pusher;
	Transform oldPoint, newPoint;
	[Space(18)]
	[SerializeField] RuntimeAnimatorController[] defilements;
	[SerializeField] Color[] colorDefilement;
	[SerializeField] GameObject boomDefilement;
	bool playing = false;
	[SerializeField] AudioClip[] farts;
	TrailRenderer trail;

	private void Start()
	{
		FindObjectOfType<BladeSound>().swings = farts;
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

		if (Input.GetMouseButtonDown (0)) {
			transform.GetChild (0).GetComponent<ParticleSystem> ().Play();
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
						if(hit[i].transform.CompareTag("MEAT"))
						{
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
			transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
		}
	}


	void Blade1(RaycastHit2D[] hit, int i){
		RaycastHit2D hitLocal = hit[i];
		Transform parent = hit [i].transform.parent;
		ScriptManager sm = parent.GetComponent<ScriptManager> ();
		if (Random.Range (0, 11) >= 7) {
			Defilement (hit [i].transform);
			Effect (hit [i].transform.parent.GetChild(2).position);
		} else {
			if (hit [i].transform.GetComponent<HingeJoint2D> ()) {
				Destroy (hit [i].transform.GetComponent<HingeJoint2D> ());
			} else {
				Destroy (hit [i].transform.parent.GetChild (1).GetComponent<HingeJoint2D> ());
			}
		}
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
	}

	void Defilement(Transform stick){
		float camSize = Camera.main.orthographicSize;
		StartCoroutine (SlowMo (stick));
		for (int i = 0; i < stick.parent.childCount; i++) {
			StartCoroutine(stick.parent.GetChild(i).GetComponent<StickPart>().Defilement (defilements, colorDefilement[0] ));
			stick.parent.GetChild (i).GetComponent<Rigidbody2D> ().drag = 8;
		}
		Instantiate (boomDefilement, stick.parent.GetChild (2));//.position, Quaternion.identity);
		Vector3 pos = stick.parent.GetChild (2).position;
		pos.z = -10;
		StartCoroutine(EffectsCam.CameraMove(pos, 30));
		StartCoroutine (ShowScore (stick.position));
	}

	IEnumerator ShowScore(Vector3 pos){
		GameObject score = UI.inst.scoresAdditive.GetObject ();
		score.transform.parent = UI.inst.transform;
		score.transform.position = Camera.main.WorldToScreenPoint (pos+new Vector3(3.89f,0,0));
		if (Application.systemLanguage == SystemLanguage.Russian) {
			score.GetComponent<Text>().text = "+5\r\nочков";
		} else {
			score.GetComponent<Text>().text = "+5\r\nscore";
		}
		Mode_1.score += 5;
		score.transform.localScale = new Vector3 (0, 0, 0);
		float iter = 11f;
		for (int i = 0; i < iter; i++) {
			score.transform.localScale += new Vector3 (1f / iter, 1f / iter, 1f / iter);
			yield return null;
		}
		yield return new WaitForSeconds (0.7f);
		for (int i = 0; i < iter; i++) {
			score.transform.localScale -= new Vector3 (1f / iter, 1f / iter, 1f / iter);
			yield return null;
		}

	}

	public IEnumerator SlowMo( Transform stick ){
		playing = true;
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < stick.parent.childCount; i++) {
			stick.parent.GetChild (i).GetComponent<Rigidbody2D> ().drag = 0;
			if (stick.parent.GetChild (i).GetComponent<HingeJoint2D> ()) {
				Destroy (stick.parent.GetChild (i).GetComponent<HingeJoint2D> ());	
			}
		}
		GameObject push = (GameObject)Resources.Load ("Pusher");
		Instantiate (push, stick.position-new Vector3(0,1,0), Quaternion.identity);
		playing = false;
	}

	void Effect(Vector3 pos){
		PoolEffects.effectsList ["Defilement Boom"].Play (pos);
	}

}
