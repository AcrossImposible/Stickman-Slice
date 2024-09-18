using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LngManager : MonoBehaviour
{
	void Start()
	{
		Language.SetLanguage(GetComponent<TMPro.TMP_Text>());
		Destroy(this);
	}
}
