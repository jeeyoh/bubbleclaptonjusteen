using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdmobController : MonoBehaviour 
{
	string statsbanner = "none";
	string statspopup = "none";

	public BannerView bannerView;
	public InterstitialAd interstitial;
	
	static AdmobController instance;
	public static AdmobController Instance	
	{
		get 
		{
			if (instance == null)
			{
				instance = (new GameObject("AdmobController")).AddComponent<AdmobController>();
				DontDestroyOnLoad( instance.gameObject );
			}
			return instance;
		}
	}

	void OnGUI ()
	{
		GUI.Label(new Rect(10,10,200,30), statsbanner);
		GUI.Label(new Rect(10,50,200,30), statspopup);
	}

	public void RequestBanner()
	{
		// Create a 320x30 banner at the top of the screen.
		bannerView = new BannerView(ConfigManager.ADMOB_BANNER_ID, AdSize.SmartBanner, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
	}
	
	public void RequestInterstitial()
	{
		// Create an interstitial.
		interstitial = new InterstitialAd(ConfigManager.ADMOB_INTERSTITIAL_ID);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	
	// Returns an ad request with custom ad targeting.
	public AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.Build();
//        return new AdRequest.Builder()
//                .AddTestDevice(AdRequest.TestDeviceSimulator)
//                .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
//                .AddKeyword("game")
//                .SetGender(Gender.Male)
//                .SetBirthday(new DateTime(1985, 1, 1))
//                .TagForChildDirectedTreatment(false)
//                .AddExtra("color_bg", "9B30FF")
//                .Build();
		
	}
	
	public void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}
	
	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, EventArgs args)
	{
		statsbanner = "HandleAdLoaded event received.";
//		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		statsbanner = "HandleFailedToReceiveAd event received with message: " + args.Message;
//		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, EventArgs args)
	{
		statsbanner = "HandleAdOpened event received";
//		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args)
	{
		statsbanner = "HandleAdClosing event received";
//		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args)
	{
		statsbanner = "HandleAdClosed event received";
//		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		statsbanner = "HandleAdLeftApplication event received";
//		print("HandleAdLeftApplication event received");
	}
	
	#endregion
	
	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		statspopup = "HandleInterstitialLoaded event received.";
//		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		statspopup = "HandleInterstitialFailedToLoad event received with message: " + args.Message;
//		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		statspopup = "HandleInterstitialOpened event received.";
//		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		statspopup = "HandleInterstitialClosing event received";
//		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		
		RequestInterstitial();
		statspopup = "HandleInterstitialClosed event received";
//		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		statspopup = "HandleInterstitialLeftApplication event received";
//		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
}
