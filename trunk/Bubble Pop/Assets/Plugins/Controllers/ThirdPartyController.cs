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

	const string FB_LINK = "http://www.facebook.com/pages/Bubble-Pop/844026942305921";
	const string FB_LINK_NAME = "I did xxx seconds in Bubble Poppp Endless Mode!";
	const string FB_IMAGE_LINK = "https://pbs.twimg.com/profile_images/583975470179332097/rF3HcnZr.jpg";
	const string FB_CAPTION = "Play bubble poppp now!";
	const string FB_DESCRIPTION = "Fun and addicting bubble popping game!";
	
	const string TWITTER_DESCRIPTION = "My best time in Bubble Poppp is xxx seconds!";

	bool fbLoginToPost = false;
	bool twitterLoginToPost = false;
	float gameScore = 0;
	string gameShareText = "";

	void Awake () 
	{
		Instance = this;

		DontDestroyOnLoad( this.gameObject );
		
		FacebookManager.sessionOpenedEvent += fbsessionOpenedEvent;
		FacebookManager.loginFailedEvent += fbloginFailedEvent;
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
		FacebookManager.sessionOpenedEvent -= fbsessionOpenedEvent;
		FacebookManager.loginFailedEvent -= fbloginFailedEvent;
		TwitterManager.loginSucceededEvent -= twitterloginSucceeded;
		TwitterManager.loginFailedEvent -= twitterloginFailed;
	}
		
	void OnDestroy ()
	{
		analyticsHandler.LogUserEvent("GameEnded");
	}

	void Start ()
	{
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
		
		if ( willShow )
		{
			AdmobController.Instance.ShowInterstitial();
		}
		else
		{
			AdmobController.Instance.interstitial.Destroy();
		}
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
		Facebook.instance.graphRequest(
			likeID+"/likes",
			HTTPVerb.POST,
			( error, obj ) =>
			{
			if (obj != null)
			{
				Prime31.Utils.logObject( obj );
			}
			if( error != null )
			{
				Debug.Log("Error liking:"+error);
				return;
			}
			Debug.Log( "like finished: " );
		});
	}

	public void LoginToFacebook ()
	{
		if ( !fbHandler.isSessionValid() )
			fbHandler.Login();
	}

	public void ShareScoreToFacebook ( MODE gameMode, float score ) 
	{
		gameScore = score;
		string newDesc =  FB_LINK_NAME;

		if ( gameMode == MODE.TIME )
			newDesc =  TIME_DESCRIPTION.Replace("xxx", gameScore.ToString());
		else if ( gameMode == MODE.ENDLESS )
			newDesc =  ENDLESS_DESCRIPTION.Replace("xxx", gameScore.ToString());

		if ( score < 0 )
			newDesc = FAILED_DESCRIPTION;

		gameShareText = newDesc;
		
		#if UNITY_EDITOR
		Debug.Log(newDesc);
		#endif

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

	public void LoginToTwitter ()
	{
		if ( !twitterHandler.isLoggedIn() )
			twitterHandler.Login();
	}

	public void ShareScoreToTwitter ( MODE gameMode, float score )  
	{
		gameScore = score;
		string newDesc =  TWITTER_DESCRIPTION;
		
		if ( gameMode == MODE.TIME )
			newDesc =  TIME_DESCRIPTION.Replace("xxx", gameScore.ToString());
		else if ( gameMode == MODE.ENDLESS )
			newDesc =  ENDLESS_DESCRIPTION.Replace("xxx", gameScore.ToString());
		
		if ( score < 0 )
			newDesc = FAILED_DESCRIPTION;

		gameShareText = newDesc;

		#if UNITY_EDITOR
		Debug.Log(newDesc);
		#endif

		if ( !twitterHandler.isLoggedIn() )
		{
			twitterLoginToPost = true;
			twitterHandler.Login();
		}
		else
		{
			twitterHandler.PostScore(newDesc);
		}
	}

	#region Listeners 
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
			twitterHandler.PostScore(gameShareText);
			twitterLoginToPost = false;
		}
		//		Debug.Log( "Successfully logged in to Twitter: " + username );
	}
	
	void twitterloginFailed( string error )
	{
		//		Debug.Log( "Twitter login failed: " + error );
	}
	#endregion
}
