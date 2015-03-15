using UnityEngine;
using System.Collections;
using Prime31;

public class ThirdPartyController : MonoBehaviour 
{
	public static ThirdPartyController Instance;

	TwitterController twitterHandler;
	FacebookController fbHandler;

	const string FB_LINK = "http://prime31.com";
	const string FB_LINK_NAME = "My best time in Bubble Poppp is xxx seconds!";
	const string FB_IMAGE_LINK = "http://prime31.com/assets/images/prime31logo.png";
	const string FB_CAPTION = "BUBBLE POPPP";
	const string FB_DESCRIPTION = "Play bubble poppp now!";
	
	const string TWITTER_DESCRIPTION = "My best time in Bubble Poppp is xxx seconds!";

	bool fbLoginToPost = false;
	bool twitterLoginToPost = false;
	float gameScore = 0;

	void Awake () 
	{
		Instance = this;

		DontDestroyOnLoad( this.gameObject );
		
		FacebookManager.sessionOpenedEvent += fbsessionOpenedEvent;
		FacebookManager.loginFailedEvent += fbloginFailedEvent;
		TwitterManager.loginSucceededEvent += twitterloginSucceeded;
		TwitterManager.loginFailedEvent += twitterloginFailed;

		fbHandler = FacebookController.Instance;
		twitterHandler = TwitterController.Instance;
	}

	void OnDisable ()
	{
		FacebookManager.sessionOpenedEvent -= fbsessionOpenedEvent;
		FacebookManager.loginFailedEvent -= fbloginFailedEvent;
		TwitterManager.loginSucceededEvent -= twitterloginSucceeded;
		TwitterManager.loginFailedEvent -= twitterloginFailed;
	}

	public void LoginToFacebook ()
	{
		if ( !fbHandler.isSessionValid() )
			fbHandler.Login();
	}

	public void ShareScoreToFacebook ( float score ) 
	{
		gameScore = score;
		string newDesc =  FB_LINK_NAME.Replace("xxx", gameScore.ToString());

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

	public void ShareScoreToTwitter ( float score ) 
	{
		gameScore = score;
		string newDesc =  TWITTER_DESCRIPTION.Replace("xxx", gameScore.ToString());

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
			string newDesc =  FB_LINK_NAME.Replace("xxx", gameScore.ToString());
			fbHandler.ShowShareDialog(FB_LINK,
			                          newDesc,
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
			string newDesc =  TWITTER_DESCRIPTION.Replace("xxx", gameScore.ToString());
			twitterHandler.PostScore(newDesc);
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
