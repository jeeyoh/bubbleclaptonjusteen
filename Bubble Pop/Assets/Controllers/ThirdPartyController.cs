using UnityEngine;
using System.Collections;
using Prime31;

public class ThirdPartyController : MonoBehaviour 
{
	public enum MODE
	{
		ENDLESS,
		TIME,
	};

	public static ThirdPartyController Instance;

	AnalyticsController analyticsHandler;
	TwitterController twitterHandler;
	FacebookController fbHandler;
	AdmobController admobHandler;

	const string ENDLESS_DESCRIPTION = "I did xxx seconds in Bubble Poppp Endless Mode!";
	const string TIME_DESCRIPTION = "I completed Bubble Poppp Time Mode in xxx seconds!";
	const string FAILED_DESCRIPTION = "I failed in Bubble Poppp.";

	string FB_LINK = "http://www.facebook.com/pages/Bubble-Pop/844026942305921";
	const string FB_LINK_NAME = "I did xxx seconds in Bubble Poppp Endless Mode!";
	const string FB_IMAGE_LINK = "https://pbs.twimg.com/profile_images/583975470179332097/rF3HcnZr.jpg";
	const string FB_CAPTION = "Play bubble poppp now!";
	const string FB_DESCRIPTION = "Fun and addicting bubble popping game!";
	
	const string TWITTER_DESCRIPTION = "My best time in Bubble Poppp is xxx seconds!";

	bool fbLoginToPost = false;
	bool twitterLoginToPost = false;
	float gameScore = 0;
	string gameShareText = "";

	public bool showInterstitial = false;

	public delegate void ThirdPartyEvents();
	public static event ThirdPartyEvents OnTwitterLogin;
	public static event ThirdPartyEvents OnTwitterLoginSuccess;
	public static event ThirdPartyEvents OnTwitterLoginFail;
	public static event ThirdPartyEvents OnFacebookLogin;
	public static event ThirdPartyEvents OnFacebookSuccess;
	public static event ThirdPartyEvents OnFacebookLoginFail;

	void Awake () 
	{
		Instance = this;
		DontDestroyOnLoad( this.gameObject );
		
//		FacebookManager.sessionOpenedEvent += fbsessionOpenedEvent;
//		FacebookManager.loginFailedEvent += fbloginFailedEvent;
		TwitterManager.loginSucceededEvent += twitterloginSucceeded;
		TwitterManager.loginFailedEvent += twitterloginFailed;

		analyticsHandler = AnalyticsController.Instance;
		fbHandler = FacebookController.Instance;
		twitterHandler = TwitterController.Instance;
		admobHandler = AdmobController.Instance;

		AdmobController.Instance.RequestBanner();
		AdmobController.Instance.RequestInterstitial();
	}

	void OnDisable ()
	{
//		FacebookManager.sessionOpenedEvent -= fbsessionOpenedEvent;
//		FacebookManager.loginFailedEvent -= fbloginFailedEvent;
		TwitterManager.loginSucceededEvent -= twitterloginSucceeded;
		TwitterManager.loginFailedEvent -= twitterloginFailed;
	}
		
	void OnDestroy ()
	{
		analyticsHandler.LogUserEvent("GameEnded");
	}

	void Start ()
	{
		FB_LINK = ConfigManager.FB_APP_STORE;

		analyticsHandler.LogUserEvent("GameStarted");

		if ( Application.loadedLevelName == "Initialization" )
			Invoke("LoadMainScene", 2f);
	}

	private void LoadMainScene() {
		Application.LoadLevel("MainScene");
	}

	public void ShowBanner ( bool willShow )
	{
		if ( willShow )
		{
			AdmobController.Instance.bannerView.Show();
		}
		else
		{
			AdmobController.Instance.bannerView.Hide();
		}
	}

	public void ShowInterstitial ( bool willShow )
	{
		showInterstitial = CheckAdsCounter ();

		if ( !showInterstitial )
			return;

		Debug.Log("show ad");

		if ( willShow )
		{
			AdmobController.Instance.ShowInterstitial();
		}
		else
		{
			AdmobController.Instance.interstitial.Destroy();
		}
	}

	public bool CheckAdsCounter ()
	{
		int adsCounter = PlayerPrefs.GetInt ( "ADS_COUNTER" );
		if ( adsCounter >= 3 )
		{
			return true;
		} 
		else
		{
			return false;
		}
	}

	public void IncreaseAdsCounter ()
	{
		int adsCounter = PlayerPrefs.GetInt ( "ADS_COUNTER" );
		adsCounter++;

		PlayerPrefs.SetInt ( "ADS_COUNTER", adsCounter );
		PlayerPrefs.Save();
	}

	public bool LikeUsOnFacebook ()
	{
		Like ( ConfigManager.FACEBOOK_APP_ID );
		Application.OpenURL("http://www.facebook.com/pages/Bubble-Pop/844026942305921" );

		if ( PlayerPrefs.GetInt ( "LIKE_FACEBOOK" ) == 0 )
		{
			PlayerPrefs.SetInt ( "LIKE_FACEBOOK", 1 );
			PlayerPrefs.Save();
			return true;
		} 

		return false;
	}

	public bool FollowUsOnTwitter ()
	{
		Application.OpenURL( "http://twitter.com/BubblePopppGame" );

		if ( PlayerPrefs.GetInt ( "FOLLOW_TWITTER" ) == 0 )
		{
			PlayerPrefs.SetInt ( "FOLLOW_TWITTER", 1 );
			PlayerPrefs.Save();
			return true;
		}

		return false;
	}

	public bool RateOurApp ()
	{
		Application.OpenURL( ConfigManager.APP_STORE );

		if ( PlayerPrefs.GetInt ( "RATE_APP" ) == 0 )
		{
			PlayerPrefs.SetInt ( "RATE_APP", 1 );
			PlayerPrefs.Save();
			return true;
		}

		return false;
	}	

	public void ShowRewards ()
	{
	}

	public void Like(string likeID)
	{
//		Facebook.instance.graphRequest(
//			likeID+"/likes",
//			HTTPVerb.POST,
//			( error, obj ) =>
//			{
//			if (obj != null)
//			{
//				Prime31.Utils.logObject( obj );
//			}
//			if( error != null )
//			{
//				Debug.Log("Error liking:"+error);
//				return;
//			}
//			Debug.Log( "like finished: " );
//		});
	}

	public void LoginToFacebook ()
	{
//		if ( !fbHandler.isSessionValid() )
//			fbHandler.Login();
	}

	string fbDescription = "";
	public void ShareScoreToFacebook ( MODE gameMode, float score ) 
	{
		if ( OnFacebookLogin != null )
			OnFacebookLogin ();

		gameScore = score;
		string newDesc =  FB_LINK_NAME;

		if ( gameMode == MODE.TIME )
			newDesc =  TIME_DESCRIPTION.Replace("xxx", gameScore.ToString());
		else if ( gameMode == MODE.ENDLESS )
			newDesc =  ENDLESS_DESCRIPTION.Replace("xxx", gameScore.ToString());

		if ( score < 0 )
			newDesc = FAILED_DESCRIPTION;

		gameShareText = newDesc;
		fbDescription = newDesc;
		
		#if UNITY_EDITOR
		Debug.Log(newDesc);
		#endif

//		Application.OpenURL("https://www.facebook.com/dialog/feed?"+
//		                    "app_id="+ConfigManager.FACEBOOK_APP_ID+
//		                    "&link="+FB_LINK+
//		                    "&picture="+FB_IMAGE_LINK+
//		                    "&name="+SpaceHere("Bubble Poppp")+
//		                    "&caption="+SpaceHere(newDesc)+
//		                    "&description="+SpaceHere(FB_DESCRIPTION)+
//		                    "&redirect_uri=https://facebook.com/");

		if ( !fbHandler.isSessionValid() )
		{
			fbLoginToPost = true;
			fbHandler.Login();
		}
		else
		{
			fbHandler.ShowShareDialog(FB_LINK,
			                          newDesc,
			                          FB_IMAGE_LINK,
			                          FB_CAPTION,
			                          FB_DESCRIPTION);
		}
	}

//	string SpaceHere (string val) {
//		return val.Replace(" ", "%20"); // %20 is only used for space
//	}

	public void ShareFB ()
	{
//		Application.OpenURL("https://www.facebook.com/dialog/feed?"+
//		                    "app_id="+ConfigManager.FACEBOOK_APP_ID+
//		                    "&link="+FB_LINK+
//		                    "&picture="+FB_IMAGE_LINK+
//		                    "&name="+SpaceHere("Bubble Poppp")+
//		                    "&caption="+SpaceHere(fbDescription)+
//		                    "&description="+SpaceHere(FB_DESCRIPTION)+
//		                    "&redirect_uri=https://facebook.com/");

		
//		fbHandler.ShowShareDialog(FB_LINK,
//		                          FB_CAPTION,
//		                          FB_IMAGE_LINK,
//		                          FB_DESCRIPTION,
//		                          fbDescription);

		fbHandler.ShowShareDialog(FB_LINK,
		                          fbDescription,
		                          FB_IMAGE_LINK,
		                          FB_CAPTION,
		                          FB_DESCRIPTION);
	}

	public void LoginToTwitter ()
	{
		if ( !twitterHandler.isLoggedIn() )
			twitterHandler.Login();
	}

	const string Address = "http://twitter.com/intent/tweet";
	public string twitterDescription = "";
	public void ShareScoreToTwitter ( MODE gameMode, float score )  
	{

		if ( OnTwitterLogin != null )
			OnTwitterLogin ();

		gameScore = score;
		string newDesc =  TWITTER_DESCRIPTION;
		
		if ( gameMode == MODE.TIME )
			newDesc =  TIME_DESCRIPTION.Replace("xxx", gameScore.ToString());
		else if ( gameMode == MODE.ENDLESS )
			newDesc =  ENDLESS_DESCRIPTION.Replace("xxx", gameScore.ToString());
		
		if ( score < 0 )
			newDesc = FAILED_DESCRIPTION;

		gameShareText = newDesc + " " + FB_LINK + " via @BubblePopppGame";
		twitterDescription = gameShareText;

		#if UNITY_EDITOR
		Debug.Log(newDesc);
		#endif

		Application.OpenURL(Address +
		                    "?text=" + WWW.EscapeURL(newDesc + " " + FB_LINK + " via @BubblePopppGame") +
		                    "&amp;url=" + WWW.EscapeURL("\t") +
		                    "&amp;related=" + WWW.EscapeURL("\t") +
		                    "&amp;lang=" + WWW.EscapeURL("en"));

//		if ( !twitterHandler.isLoggedIn() )
//		{
//			twitterLoginToPost = true;
//
//			twitterHandler.Login();
//		}
//		else
//		{
//			twitterHandler.PostScore(newDesc + " " + FB_LINK + " via @BubblePopppGame");
//			StartCoroutine("TwitterSharingMessage");
//		}
	}

	IEnumerator TwitterSharingMessage ()
	{
		yield return new WaitForSeconds(1.5f);

		if ( OnTwitterLoginSuccess != null )
			OnTwitterLoginSuccess ();
		
		yield return null;
	}

	#region Listeners 

	public void fbLoginInSuccess ()
	{
		if ( OnFacebookSuccess != null )
			OnFacebookSuccess ();
	}

	public void fbLoginFail ()
	{
		if ( OnFacebookLoginFail != null )
			OnFacebookLoginFail ();
	}

	void fbsessionOpenedEvent()
	{
		if ( fbLoginToPost )
		{
			fbHandler.ShowShareDialog(FB_LINK,
			                          gameShareText,
			                          FB_IMAGE_LINK,
			                          FB_CAPTION,
			                          FB_DESCRIPTION);
			fbLoginToPost = false;
		}
		//		Debug.Log( "Successfully logged in to Facebook" );
	}
	
	void fbloginFailedEvent( P31Error error )
	{
		//		Debug.Log( "Facebook login failed: " + error );
	}
	
	void twitterloginSucceeded( string username )
	{
		if ( twitterLoginToPost )
		{
			if ( OnTwitterLoginSuccess != null )
				OnTwitterLoginSuccess ();

			twitterHandler.PostScore(gameShareText);
			twitterLoginToPost = false;
		}

		//		Debug.Log( "Successfully logged in to Twitter: " + username );
	}
	
	void twitterloginFailed( string error )
	{
		if ( OnTwitterLoginFail != null )
			OnTwitterLoginFail ();
		//		Debug.Log( "Twitter login failed: " + error );
	}
	#endregion
}
