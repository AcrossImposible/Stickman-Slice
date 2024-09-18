using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Advertising : MonoBehaviour {

	public static string appKey;

	public static int countPlay = 0;

	static bool appodealInit;

	void Awake()
	{
		Appodeal.setLogLevel (Appodeal.LogLevel.Verbose);

		if (!appodealInit)
		{
			appodealInit = true;

			appKey = "05a0275d3a1e6745640036bf9a623ed01d13f82c63e091a1";

			Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO);
		}
	}


	public static void ShowInterstitial()
	{
        countPlay++;

        if (countPlay > 1)
        {
            if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            {
                Appodeal.show(Appodeal.REWARDED_VIDEO);
            }
            else if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            {
                Appodeal.show(Appodeal.INTERSTITIAL);
            }

            countPlay = 0;
        }
    }

	public static void ShowBanner()
	{
        if (Appodeal.isLoaded(Appodeal.BANNER_TOP))
        {
            Appodeal.show(Appodeal.BANNER_TOP);
        }
    }

	public static void HideBanner()
	{
		Appodeal.hide(Appodeal.BANNER_TOP);
	}
}
