using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Blade : MonoBehaviour {

	GameObject circleCloneEb;
	GameObject pusherClone;
	Transform oldPointDubl, newPointDubl;
	[SerializeField] public GameObject[] bloodSlice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale < 0.1)
        {
			return;
        }

		if (Input.GetMouseButton (0)) {
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // переменной записываються координаты мыши по иксу и игрику
			Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
			transform.position = objPosition;// + new Vector3(0,0,10); // и собственно объекту записываються координаты
		}

		if (Input.GetMouseButton (0)) {

			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition +new Vector3(0,0,10));
			GameObject circleClone = Instantiate(circleCloneEb, pos, Quaternion.identity);
			if (oldPointDubl) {
				Vector3 dirRay;
				if (newPointDubl) {
					dirRay = newPointDubl.position - oldPointDubl.position;
					Debug.DrawRay (oldPointDubl.position, dirRay, new Color (0, 1f, 0.5f), 15);
					oldPointDubl = newPointDubl;
					newPointDubl = circleClone.transform;
				} else {
					newPointDubl = circleClone.transform;
					dirRay = newPointDubl.position - oldPointDubl.position;
					Debug.DrawRay (oldPointDubl.position, dirRay, new Color (0, 1f, 0.5f), 15);
				}
				float dist = Vector3.Distance (oldPointDubl.position, newPointDubl.position);
				RaycastHit2D[] hit = Physics2D.RaycastAll (oldPointDubl.position, dirRay, dist*1.8f);
				if (hit.Length > 0 ) {
					for (int i = 0; i < hit.Length; i++) {
						if(hit[i].transform.tag == "MEAT"){
							Blade1 (hit, i);
							hit [i].transform.GetComponent<TouchDetect> ().Damage ();
							hit [i].transform.parent.GetComponent<ScriptManager> ().sliced = true;
						}
					}
				}
			} else {
				oldPointDubl = circleClone.transform;
			}
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			// Поиск перпендикуляра
			Vector3 v1 = new Vector3 (2, 1, 0);
			Vector3 v2 = new Vector3 (0, 0, 1);
			print ( Vector3.Cross(v2,v1));
		}

		if (Input.GetMouseButtonUp (0)) {
			oldPointDubl = null;
			newPointDubl = null;
		}
	}



	void Blade1(RaycastHit2D[] hit, int i){
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}
		hit[i].transform.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 0, 0);
		Instantiate (pusherClone, hit[i].point, Quaternion.identity);
	}

	void Blade2(RaycastHit2D[] hit, int i){
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}
		hit [i].transform.tag = "USED";
		hit[i].transform.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 0, 0);
		Instantiate (pusherClone, hit[i].point, Quaternion.identity);
	}

	void Blade3(RaycastHit2D[] hit, int i){ //-----------TODO 
		if (hit[i].transform.GetComponent<HingeJoint2D> ()) {
			Destroy (hit[i].transform.GetComponent<HingeJoint2D> ());
		} else {
			Destroy(hit[i].transform.parent.GetChild(1).GetComponent<HingeJoint2D>());
		}
		for (int j = 0; j < hit [i].transform.parent.childCount; j++) {
			hit [i].transform.parent.GetChild (j).tag = "USED";
		}
		hit[i].transform.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 0, 0);
		Instantiate (pusherClone, hit[i].point, Quaternion.identity);
	}

	void Reset(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void Effects(Vector3 posSpray){
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
		foreach (SprayBalde s in SprayBalde.spraysBlade) {
			if (s.use)
				continue;
			else {
				s.PLay (posSpray);
				break;
			}
		}
	}

}
