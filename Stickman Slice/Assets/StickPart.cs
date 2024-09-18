using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPart : MonoBehaviour {
	
	[SerializeField] Stick stick;
	[HideInInspector] public bool isTouch;
	StickPart stickOther;
	bool adjacentBlood;
	ScriptManager sm;
	[SerializeField] GameObject bone;


	IEnumerator Start () {
		if (transform.Find ("Sliced")) {
			transform.Find ("Sliced").gameObject.SetActive (false);
		}
		if (GetComponent<HingeJoint2D> ()) {
			stickOther = GetComponent<HingeJoint2D> ().connectedBody.GetComponent<StickPart> ();
		}
	
		yield return new WaitForSeconds (0.1f);
		ImplementForce ();
		sm = transform.parent.GetComponent<ScriptManager> ();
		StartCoroutine (CheckPosition ());
	}

	public void Slice(){
		if (Random.Range (0, 11) > 7) {
			Vector3 pos = Vector3.zero;
			if (stick != Stick.head) {
				pos.y = GetComponent<BoxCollider2D> ().size.y / 2;
			} else {
				pos.y = GetComponent<CircleCollider2D> ().radius * (-1);
			}
			GameObject go = Blood2.SpawnBlood (transform, pos);
			if (stick == Stick.head && go) 
				go.transform.localRotation = Quaternion.Euler (0, 0, 180);
			if (stickOther) {
				if (stick != Stick.head) {
					stickOther.GetComponent<StickPart> ().BloodOnOtherStick ();
				} else stickOther.GetComponent<StickPart> ().BloodOnOtherStick (1);
			}
		} 
		else// if(Random.Range(0,18) > 8) 
		{ 
			Instantiate (GetComponent<TouchDetect> ().sm.bloodHit[Game.colorBlood], transform);
		}/**/
		transform.parent.GetComponent<ScriptManager> ().EnableBlur ();
	}

	public void BloodOnOtherStick(int sign = -1){
		Vector3 pos = Vector3.zero;
		pos.y = GetComponent<BoxCollider2D> ().size.y * (sign);
		GameObject b = Blood2.SpawnBlood (transform, pos);
		if (sign == -1 && b) {
			b.transform.localRotation = Quaternion.Euler (0, 0, 180);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			ImplementForce ();
		}


	}

	public void DetectPartSlice(){
		if (!isTouch) {
			switch (stick) {
			case Stick.head:
				{
					MissionHeadSlice mhs = FindObjectOfType<MissionHeadSlice> ();
					MisHeadSliceOfBlade_1 mhsob1 = FindObjectOfType<MisHeadSliceOfBlade_1> ();
					MisHeadSliceOfBlade_3 mhsob3 = FindObjectOfType<MisHeadSliceOfBlade_3> ();
					if (mhs || mhsob1 || mhsob3) {
						Missions.countSliceHead++;
						if (Game.usedBlade == 0) {
							Missions.countSliceHeadOfBlade_1++;
						} else if (Game.usedBlade == 2) {
							Missions.countSliceHeadOfBlade_3++;
						}
					}
					break;
				}
			case Stick.arm:
				{
					MissionArmSlice mas = FindObjectOfType<MissionArmSlice> ();
					if (mas) {
						Missions.countSliceArm++;
					}
					break;
				}
			}
			isTouch = true;

		}

	}

	void ImplementForce()
	{
		Vector2 forceDirection = Mode_1.GetRandomPoint() - (Vector2)transform.position;
		//Vector2 forceDirection = Game.CalculateTargetPoint() - (Vector2)transform.position;

		ScriptManager sm = transform.parent.GetComponent<ScriptManager> ();
		//int forceX = Random.Range (-5, 5) * 130;
		//int forceY = 1700;
		forceDirection *= Random.Range(11, 15) * 10;
		if (sm.isBlue) 
		{
			forceDirection *= 2.1f;
		} 
		else if (sm.isGhost) 
		{
			//forceY = 1800;
		}
		if (sm.isRain) {
			forceDirection *= 0.01f;
		}
		if (Time.timeScale < 1) {
			//forceX *= 4;
			//forceY *= 4;
			forceDirection /= Time.timeScale;
		}
		//GetComponent<Rigidbody2D> ().AddForce (new Vector2 (forceX, forceY));
		GetComponent<Rigidbody2D>().AddForce(forceDirection);
	}

	IEnumerator CheckPosition() {
		yield return new WaitForSeconds (0.18f);
		if (sm.sliced && transform.position.y < -3 && Stick.head != stick) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
		StartCoroutine (CheckPosition ());
	}

	public IEnumerator Defilement (RuntimeAnimatorController[] controllers, Color color) {
		
		if (bone) {
			GameObject go = Instantiate (bone, transform);
			go.transform.localPosition = Vector3.zero + new Vector3 (0, 0, 1);
		}

		Animator animator = gameObject.AddComponent<Animator> ();
		GetComponent<SpriteRenderer> ().color = color;
		yield return new WaitForSeconds (0.3f);
		switch (stick) {
		case Stick.body_1:
			{
				animator.runtimeAnimatorController = controllers[1];
				break;
			}
		case Stick.body_2:
			{
				animator.runtimeAnimatorController = controllers[1];
				transform.localScale = new Vector3 (1, -1, 1);
				break;
			}
		case Stick.arm:
			{
				animator.runtimeAnimatorController = controllers[0];
				break;
			}
		case Stick.leg:
			{
				animator.runtimeAnimatorController = controllers[2];
				break;
			}
		case Stick.head:
			{
				animator.runtimeAnimatorController = controllers[3];
				break;
			}
		}


	}
}

[SerializeField]
public enum Stick {
	head,
	body_1,
	body_2,
	arm,
	leg,
}
