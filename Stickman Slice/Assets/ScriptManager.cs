using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour {

	[SerializeField] private float drag = 0f;
	[SerializeField] public bool isBlue;
	[SerializeField] public bool isGhost;
	[SerializeField] Sprite[] ebals;
	[SerializeField] public GameObject[] bloodHit;
	public bool isRain;
	[HideInInspector] public int countSlice;
	[HideInInspector] public bool sliced;

	bool helper = false;

	IEnumerator Start () {

		foreach (var item in GetComponentsInChildren<Rigidbody2D>())
		{
			item.drag = drag;
		}

		if (isBlue) {
			Spawner.notBlueStickman = false;
		}
		int countComponents = transform.childCount;
		for (int i = 0; i < countComponents; i++) {
			transform.GetChild (i).gameObject.AddComponent<TouchDetect> ();
		}
		yield return new WaitForSeconds (8);
		if (isBlue || isRain) {// Если стикммен часть одной из волн, то через это время возобновить работу спаунеров
			Spawner.pause = false;
			StartCoroutine (CheckHealth());
		}
		if (isGhost) {
			yield return new WaitForSeconds (15);
		} else {
			yield return new WaitForSeconds (8.8f);
		}
		if (isBlue) {
			Spawner.notBlueStickman = true;
		}
		Destroy (gameObject);
	}

	void FixedUpdate () {
		if (!helper && sliced) {
			if (!isGhost && !isBlue && Random.Range (0, 15) > 8) {
				StartCoroutine (BigHead ());
			}
			Missions.countAnnihilation++;
			Missions.countAllAnnihilation++;
			helper = true;
			if (isBlue) {
				Spawner.pause = true;
				StartCoroutine (EnableSpawners ());
				UI.txtWarningWave.gameObject.SetActive (true);
				MissionBlueSlice mbs = FindObjectOfType<MissionBlueSlice> ();
				if (mbs) {
					Missions.countBlueSlice++;
				}
			}
			Vector2 forceSlice = new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
			if (forceSlice.magnitude > 5.3f && Random.Range(0,11) > 7/**/) {
				StartCoroutine (SlowMo());
			}
		}
	}
		
	public void OnHit()
	{
		if(!sliced)
			SwitchLayerMask();
	}

	private void SwitchLayerMask()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("SLICED");
		}
	}

	public IEnumerator SlowMo(){
		Time.timeScale = 0.3f;
		Time.fixedDeltaTime = 0.005f;
		yield return new WaitForSeconds (1);
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
	}

	IEnumerator BigHead(){
		yield return null;
		float mass = transform.GetChild (0).GetComponent<Rigidbody2D> ().mass;
		transform.GetChild (0).GetComponent<Rigidbody2D> ().useAutoMass = false;
		transform.GetChild (0).GetComponent<Rigidbody2D> ().mass = mass;
		transform.GetChild (0).localScale += new Vector3 (1.1f, 1.1f, 1.1f);
		transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
		int ebaloId = Random.Range (0, ebals.Length);
		transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = ebals[ebaloId];
		foreach (Scream s in Scream.screams) {
			if (s.use)
				continue;
			else {
				s.SetClip (ebaloId, transform.GetChild (0));
				break;
			}
		}
		yield return new WaitForSeconds (1.3f);
		transform.GetChild (0).localScale -= new Vector3 (1.1f, 1.1f, 1.1f);
		transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
	}

	IEnumerator EnableSpawners(){
		yield return new WaitForSeconds (1.7f);

		if (Menu.hideSaw) {
			UI ui = FindObjectOfType<UI> ();
			ui.btn_Saw.SetActive (true);
			ui.btn_Saw.transform.GetChild (1).gameObject.SetActive (true);
			Menu.hideSaw = false;
			Saves.SaveUseSaw ();
			StartCoroutine (GlowBtnSaw (ui.btn_Saw));
		}

		int[] indexes = new int[SpawnerWave.spawners.Count];
		for (int i = 0; i < SpawnerWave.spawners.Count; i++) {
			indexes [i] = i;
		}
		var rand = new System.Random ();
		for (int i = indexes.Length-1; i >= 1; i--)
		{
			int j = rand.Next(i + 1);
			// обменять значения data[j] и data[i]
			var temp = indexes[j];
			indexes[j] = indexes[i];
			indexes[i] = temp;
		}
		float delay = 0.2f;

		for (int i = 0; i < SpawnerWave.spawners.Count; i++) {
			yield return new WaitForSeconds (delay);
			SpawnerWave.spawners [indexes [i]].SetActive (true);
		}

		for (int i = indexes.Length-1; i >= 1; i--)
		{
			int j = rand.Next(i + 1);
			// обменять значения data[j] и data[i]
			var temp = indexes[j];
			indexes[j] = indexes[i];
			indexes[i] = temp;
		}

		yield return new WaitForSeconds (delay);
		for (int i = 0; i < SpawnerWave.spawners.Count; i++) {
			yield return new WaitForSeconds (delay);
			SpawnerWave.spawners [indexes [i]].GetComponent<SpawnerWave> ().Spawn ();
		}/**/
		foreach (GameObject go in SpawnerWave.spawners) {
			go.SetActive (false);
		}
	}

	public void EnableBlur()
	{
		if (!isBlue && !isGhost) 
		{
			for (int i = 0; i < transform.childCount; i++) 
			{
				transform.GetChild (i).GetComponent<SpriteRenderer> ().color = new Color (0.15f,0.15f,0.15f,1); //enabled = false;
				try
				{
					transform.GetChild (i).Find("Sliced").gameObject.SetActive(true);	
				} 
				catch (System.Exception)
				{
					
				}
			}
		}
	}

	IEnumerator GlowBtnSaw(GameObject btn){
		for (int i = 0; i < 11; i++) {
			for (float f = 0; f < 1.01f; f += 0.1f) {
				btn.transform.GetChild (1).GetComponent<Image> ().color = new Color (1, 1, 1, f);
				yield return null;
			}
			for (float f = 1; f > 0; f -= 0.1f) {
				btn.transform.GetChild (1).GetComponent<Image> ().color = new Color (1, 1, 1, f);
				yield return null;
			}
		}
		btn.transform.GetChild (1).gameObject.SetActive (false);
	}

	IEnumerator CheckHealth(){
		yield return new WaitForSeconds (1);
		if (HealthManager.health > 0) {
			MissionCountOutlast mco = FindObjectOfType<MissionCountOutlast> ();
			if (mco) {
				Missions.countOutlastWave++;
			}
		}
	}
		
	/*public static void Shuffle<T>(this System.Random rng, T[] array)
	{
		int n = array.Length;
		while (n > 1)
		{
			int k = rng.Next(n--);
			T temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}/**/

}
