using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class TwitterController : MonoBehaviour
{
	#if UNITY_IPHONE || UNITY_ANDROID
	static TwitterController instance;
	public static TwitterController Instance	
	{
		get 
		{
			if (instance == null)
			{
				instance = (new GameObject("TwitterController")).AddComponent<TwitterController>();
				instance.Initialize();
				DontDestroyOnLoad( instance.gameObject );
			}
			return instance;
		}
	}

	// common event handler used for all Twitter API requests that logs the data to the console
	void completionHandler( string error, object result )
	{
		if( error != null )
			Debug.LogError( error );
		else
			Prime31.Utils.logObject( result );
	}

	public void Initialize ()
	{
		TwitterCombo.init( ConfigManager.TWITTER_CONSUMER_KEY , ConfigManager.TWITTER_CONSUMER_SECRET );
	}

	public void Login ()
	{
		TwitterCombo.showLoginDialog();
	}

	public void Logout ()
	{
		TwitterCombo.logout();
	}

	public bool isLoggedIn ()
	{
		bool isLoggedIn = TwitterCombo.isLoggedIn();
		return isLoggedIn;
	}

	public string LoggedInUsername ()
	{
		string username = TwitterCombo.loggedInUsername();
		return username;
	}

	public void PostScore ( string score )
	{
		TwitterCombo.postStatusUpdate( score );
	}

	public void PostScoreWithImage ( string score, string imagePath )
	{
		//var pathToImage = Application.persistentDataPath + "/" + FacebookComboUI.screenshotFilename;
		TwitterCombo.postStatusUpdate( score, imagePath);
	}

	public void CustomRequest ()
	{
		var dict = new Dictionary<string,string>();
		dict.Add( "count", "2" );
		TwitterCombo.performRequest( "GET", "1.1/statuses/home_timeline.json", dict );
	}
	#endif
}
