using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MngrGhosts : MonoBehaviour
{

	//[SerializeField] public List<GameObject> ghosts = new List<GameObject>();
	[SerializeField] GameObject ghost;
	int countGhost = 7;
	int countSliceGhost;
	[SerializeField] Color colorGhost;
	[SerializeField] Color colorPanel;
	[SerializeField] GameObject stickman;
	[SerializeField] Image glow;
	[SerializeField] GameObject txt_Warning;
	Vector2 anchPos;
	float damp = 18;
	int timer = 3;
	Vector2 newPos;
	bool shake;
	// Use this for initialization
	void Start()
	{
		glow.gameObject.SetActive(false);
		txt_Warning.SetActive(false);

		ghost.GetComponent<Image>().color = new Color(0, 0, 0, 0.53f);
		for (int i = 0; i < countGhost; i++)
		{
			Instantiate(ghost, transform);
		}
		anchPos = GetComponent<RectTransform>().anchoredPosition;
		//GetComponent<RectTransform> ().anchoredPosition += new Vector2 (100, -200);
		//GetComponent<RectTransform> ().sizeDelta += new Vector2(18,18);
		//print (GetComponent<RectTransform> ().anchoredPosition);
	}

	void FixedUpdate()
	{
		if (shake)
		{
			if (timer >= 3)
			{
				newPos = anchPos + (Random.insideUnitCircle * 3);
				timer = 0;
			}
			Vector2 originPos = GetComponent<RectTransform>().anchoredPosition;
			GetComponent<RectTransform>().anchoredPosition
		= Vector2.Lerp(originPos, newPos, Time.deltaTime * damp);
			timer++;/**/
		}
	}

	public void AddGhost()
	{
		if (transform.childCount >= countSliceGhost + 1)
		{
			transform.GetChild(countSliceGhost).GetComponent<Image>().color = colorGhost;
			StartCoroutine(GlowIconGhost(transform.GetChild(countSliceGhost).GetChild(0).GetComponent<Image>()));
			countSliceGhost++;
		}

		if (countSliceGhost >= countGhost && shake == false)
		{

			Spawner.pause = true;

			StartCoroutine(GhostsRise());
		}
	}

	IEnumerator GhostsRise()
	{
		shake = true;
		yield return new WaitForSeconds(0.8f);
		glow.gameObject.SetActive(true);
		GetComponent<Image>().color = new Color(0.59f, 0.59f, 0.59f, 1);
		StartCoroutine(Warning());
		StartCoroutine(GlowBlink());
		yield return new WaitForSeconds(1.8f);

		float step = 26f / (countGhost - 1);
		for (int i = 0; i < countGhost; i++)
		{
			Vector3 pos = new Vector3(-13 + (step * i), 12, 0);
			int degress = Random.Range(0, 18);
			degress *= 20;
			GameObject stck = Instantiate(stickman, pos, Quaternion.Euler(0, 0, degress));
			stck.GetComponent<ScriptManager>().isRain = true;
		}

		yield return new WaitForSeconds(3.0f);
		shake = false;

		yield return new WaitForSeconds(3.9f);
		for (int i = 0; i < countGhost; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		countSliceGhost = 0;
		countGhost++;
		for (int i = 0; i < countGhost; i++)
		{
			Instantiate(ghost, transform);
		}/**/
		glow.gameObject.SetActive(false);
		GetComponent<Image>().color = colorPanel;
	}

	IEnumerator GlowBlink()
	{

		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < 12; i++)
		{
			for (float f = 0.1f; f < 1; f += 0.038f)
			{
				glow.color = new Color(1, 1, 1, f);

				for (int j = 0; j < countGhost; j++)
				{
					transform.GetChild(j).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, f);
				}

				yield return null;
			}

			yield return null;
		}
	}

	IEnumerator GlowIconGhost(Image img)
	{
		for (float f = 0; f < 1.1f; f += 0.05f)
		{
			img.color = new Color(1, 1, 1, f);
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		for (float f = 1; f > 0; f -= 0.02f)
		{
			if (img)
			{
				img.color = new Color(1, 1, 1, f);
			}
			yield return null;
		}
	}

	IEnumerator Warning()
	{
		txt_Warning.SetActive(true);
		txt_Warning.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
		//txt_Warning.GetComponent<RectTransform> ().sizeDelta = new Vector2(0,-90);

		for (int i = 0; i < 23; i++)
		{
			yield return null;
			txt_Warning.GetComponent<RectTransform>().localScale += new Vector3(0.05f, 0.05f, 0.05f);
		}
		yield return new WaitForSeconds(0.15f);
		for (int i = 0; i < 10; i++)
		{
			yield return null;
			txt_Warning.GetComponent<RectTransform>().localScale -= new Vector3(0.025f, 0.025f, 0.025f);
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 8; i++)
		{
			yield return null;
			txt_Warning.GetComponent<RectTransform>().localScale += new Vector3(0.025f, 0.025f, 0.025f);
		}
		yield return new WaitForSeconds(1.3f);
		for (float f = 1; f > 0.01f; f -= 0.01f)
		{
			yield return null;
			txt_Warning.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 3.5f);
			txt_Warning.GetComponent<TMP_Text>().color = new Color(0.82f, 0.929f, 0.921f, f);
		}

		yield return new WaitForSeconds(1);
		txt_Warning.SetActive(false);
		txt_Warning.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		txt_Warning.GetComponent<TMP_Text>().color = new Color(0.82f, 0.929f, 0.921f, 1);
	}
}
