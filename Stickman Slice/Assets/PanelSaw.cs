using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class PanelSaw : MonoBehaviour
{
    [SerializeField] TMP_Text labelValue;
    [SerializeField] Button btnAdSaw;
    [SerializeField] HorizontalLayoutGroup group;


    private void Start()
    {
        btnAdSaw.onClick.AddListener(AddSaw);
    }

    private void OnEnable()
    {
        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return null;
            
            group.spacing = 1;
        }
    }

    private void Video_Closed(int obj)
    {
        var menu = FindObjectOfType<Menu>();
        menu.ShowReward();
    }

    private void Reward_Received(string obj)
    {
        var menu = FindObjectOfType<Menu>();
        
        menu.AddSaw();
    }

    void AddSaw()
    {
        //YandexSDK.instance.ShowRewarded("chtoto");
    }

    private void Update()
    {
        string strSaw = Game.firstSaw + Game.secondSaw;
        int countSaw = int.Parse(strSaw);

        labelValue.text = $"{countSaw}";
    }
}
