using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSaw : MonoBehaviour {

	List<GameObject> stickmans = new List<GameObject>();
	[SerializeField] GameObject[] walls;
	Vector3 originPosCamera = new Vector3(0,1.8f,-10);
	[SerializeField] AnimationCurve curve;
	[SerializeField] GameObject btn_CircularSaw;
	Vector2[,] forces;

	public void Run () {
		
		walls [0].SetActive (false);
		walls [1].SetActive (false);
		ScriptManager[] sm = FindObjectsOfType<ScriptManager> ();
		for (int i = 0; i < sm.Length; i++) {
			if (!sm [i].sliced
				&& sm [i].transform.GetChild (2).position.y < 9.8f
				&& sm [i].transform.GetChild (2).position.y > -7.1f) {
				stickmans.Add (sm [i].gameObject);
			}
		}
			


		if (stickmans.Count > 0) {
			Time.timeScale = 0.048f;
			Time.fixedDeltaTime = 0.0009f;
			btn_CircularSaw.SetActive (false);
			StartCoroutine (Attack (sm));
			forces = new Vector2[stickmans.Count,11];
			for (int i = 0; i < stickmans.Count; i++) {
				for (int j = 0; j < 11; j++) {
					forces [i, j] = stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().velocity;
					stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().drag = 18;
					stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().angularDrag = 18;

				}
			}/**/
			/*forces = new Vector2[sm.Length,11];
			for (int i = 0; i < sm.Length; i++) {
				for (int j = 0; j < 11; j++) {
					forces [i, j] = sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().velocity;
					sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().drag = 18;
					sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().angularDrag = 18;
				}
			}/**/
		} else {
			FindObjectOfType<UI> ().AddSaw ();
			StartCoroutine (SetText ());
		}
	}

	IEnumerator Attack(ScriptManager[] sm){
		GetComponent<AudioSource> ().Play ();
		for (int i = 0; i < stickmans.Count; i++) {
			if (stickmans [i].GetComponent<ScriptManager> ().sliced)
				continue;
			Vector3 posCamera = stickmans [i].transform.GetChild (2).position - new Vector3(-1.5f,0,10);
			StartCoroutine (MoveCamera (posCamera));
			Vector3 pos = stickmans[i].transform.GetChild (2).position + new Vector3 (5f, 0, 0);
			transform.position = pos;
			StartCoroutine (Move ());
			yield return new WaitForSeconds (0.039f);
			stickmans [i].GetComponent<ScriptManager> ().sliced = true;
		}
		for (int i = 0; i < stickmans.Count; i++) {
			for (int j = 0; j < 11; j++) {
				if (j != 0) {
					stickmans [i].transform.GetChild (j).GetComponent<BoxCollider2D> ().enabled = false;
				} else stickmans [i].transform.GetChild (j).GetComponent<CircleCollider2D> ().enabled = false;
			}
		}
		yield return null;

		for (int i = 0; i < stickmans.Count; i++) {
			for (int j = 0; j < 11; j++) {
				stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().drag = 0.3f;
				stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().angularDrag = 0.3f;
				stickmans [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().velocity = forces [i, j] * 1.5f;
			}
		}/**/
		/*for (int i = 0; i < sm.Length; i++) {
			for (int j = 0; j < 11; j++) {
				if (sm [i]) {
					sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().drag = 0.3f;
					sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().angularDrag = 0.3f;
					sm [i].transform.GetChild (j).GetComponent<Rigidbody2D> ().velocity = forces [i, j] * 1.5f;
				}
			}
		}/**/
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		stickmans.Clear ();
		walls [0].SetActive (true);
		walls [1].SetActive (true);
		transform.position = new Vector3 (88, 88, 0);
		StartCoroutine(MoveCamera(originPosCamera, 8));
		btn_CircularSaw.SetActive (true);
		GetComponent<AudioSource> ().Stop ();
	}

	IEnumerator Move(){
		for (int i = 0; i < 108; i++) {
			yield return new WaitForEndOfFrame ();
			transform.position += new Vector3 (-0.088f, 0, 0);
		}
	}

	IEnumerator MoveCamera(Vector3 pos, float size = 2.8f){
		int countFrame = 10;
		Vector3 distance = Camera.main.transform.position - pos;
		float stepX = distance.x / countFrame;
		float stepY = distance.y / countFrame;
		float stepSize = (Camera.main.orthographicSize - size) / countFrame;
		for (int i = 0; i < countFrame-1; i++) {
			
			Camera.main.transform.position -= new Vector3 (stepX*curve.Evaluate (i), stepY*curve.Evaluate (i), 0);
			Camera.main.orthographicSize -= stepSize*curve.Evaluate (i);
			yield return new WaitForEndOfFrame ();
		}
		Camera.main.transform.position = pos;
		Camera.main.orthographicSize = size;
	}

	IEnumerator SetText(){
		yield return null;
		if (Application.systemLanguage == SystemLanguage.Russian) {
			FindObjectOfType<UI> ().txt_CountSaw.text = "Нужны Стикмены!";
		} else {
			FindObjectOfType<UI>().txt_CountSaw.text = "Need Stickman!";
		}
		yield return new WaitForSeconds (1.8f);
		FindObjectOfType<UI> ().ConvertCountSaw ();
	}
}
