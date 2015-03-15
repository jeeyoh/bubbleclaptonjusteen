using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;


public class FacebookController : MonoBehaviourGUI
{
	#if UNITY_IPHONE || UNITY_ANDROID	
	static FacebookController instance;
	private string _userId;

	public static FacebookController Instance	
	{
		get 
		{
			if (instance == null)
			{
				instance = (new GameObject("FacebookController")).AddComponent<FacebookController>();
				instance.Initialize();
				DontDestroyOnLoad( instance.gameObject );
			}
			return instance;
		}
	}
	
	// common event handler used for all graph requests that logs the data to the console
	void completionHandler( string error, object result )
	{
		if( error != null )
			Debug.LogError( error );
		else
			Prime31.Utils.logObject( result );
	}
	
	void Initialize()
	{
		// dump custom data to log after a request completes
		FacebookManager.graphRequestCompletedEvent += result =>
		{
			Prime31.Utils.logObject( result );
		};

		// optionally enable logging of all requests that go through the Facebook class
		//Facebook.instance.debugRequests = true;

		FacebookCombo.init();
	}

	public void Login ()
	{
		// Note: requesting publish permissions here will result in a crash. Only read permissions are permitted.
		var permissions = new string[] { "email" };
		FacebookCombo.loginWithReadPermissions( permissions );
	}

	public void ReauthPublishPermissions ()
	{
		var permissions = new string[] { "publish_actions", "publish_stream" };
		FacebookCombo.reauthorizeWithPublishPermissions( permissions, FacebookSessionDefaultAudience.OnlyMe );
	}

	public void Logout ()
	{
		FacebookCombo.logout();
	}

	public bool isSessionValid ()
	{
		// isSessionValid only checks the local session so if a user deauthorizies the application on Facebook's website it can be incorrect
		var isLoggedIn = FacebookCombo.isSessionValid();
		
		//// This way of checking a session hits Facebook's servers ensuring the session really is valid
		//Facebook.instance.checkSessionValidityOnServer( isValid =>
		//                                               {
		//	Debug.Log( "checked session validity on server. Is it valid? " + isValid );
		//});

		return isLoggedIn;
	}

	public string GetAccessToken ()
	{
		var token = FacebookCombo.getAccessToken();
		return token;
	}

	public void GetGrantedPermissions ()
	{			
		// This way of getting session permissions uses Facebook's SDK which is a local cache. It can be wrong for various reasons.
		var permissions = FacebookCombo.getSessionPermissions();
		foreach( var perm in permissions )
			Debug.Log( perm );
		
		// This way of getting the permissions hits Facebook's servers so it is certain to be valid.
		Facebook.instance.getSessionPermissionsOnServer( completionHandler );
	}

	public void ShowShareDialog ( string link, string name, string picture, string caption, string description )
	{
		var parameters = new Dictionary<string,object>
		{
			{ "link", link },
			{ "name", name },
			{ "picture", picture },
			{ "caption", caption },
			{ "description", description }
		};
		FacebookCombo.showFacebookShareDialog( parameters );
	}

	public void PostImage (string imagePath)
	{
		var pathToImage = imagePath;
		if( !System.IO.File.Exists( pathToImage ) )
		{
			Debug.LogError( "there is no screenshot avaialable at path: " + pathToImage );
			return;
		}
		
		var bytes = System.IO.File.ReadAllBytes( pathToImage );
		Facebook.instance.postImage( bytes, "im an image posted from iOS", completionHandler );
	}

	public void PostMessage ( string message )
	{
		Facebook.instance.postMessage( message, completionHandler );
	}

	public void PostMessageWithExtras ( string message, string link, string linkName, string image, string imageName)
	{
		Facebook.instance.postMessageWithLinkAndLinkToImage( message,
		                                                    link, 
		                                                    linkName, 
		                                                    image, 
		                                                    imageName, 
		                                                    completionHandler );
	}

	public void PostScore ( int score )
	{
		// note that posting a score requires publish_actions permissions!
		Facebook.instance.postScore( score, ( didPost ) => { Debug.Log( "score did post: " + didPost ); } );
	}

	public void ShowStreamPublishDialog ( string link, string name, string picture, string caption)
	{
		// parameters are optional. See Facebook's documentation for all the dialogs and paramters that they support
		var parameters = new Dictionary<string,string>
		{
			{ "link", link },
			{ "name", name },
			{ "picture", picture },
			{ "caption", caption }
		};
		FacebookCombo.showDialog( "stream.publish", parameters );
	}

	public void GetFriends ()
	{
		Facebook.instance.getFriends( ( error, friends ) =>
		                             {
			if( error != null )
			{
				Debug.LogError( "error fetching friends: " + error );
				return;
			}
			
			Debug.Log( friends );
		});
	}

	public void GraphRequest ()
	{
		Facebook.instance.getMe( ( error, result ) =>
		                        {
			// if we have an error we dont proceed any further
			if( error != null )
				return;
			
			if( result == null )
				return;
			
			// grab the userId and persist it for later use
			_userId = result.id;

			Debug.Log( result );
		});
	}

	public Texture2D GetProfileImageTexture ()
	{
		Texture2D profileImage = null;

		GraphRequest ();

		if( _userId != null )
		{
			if( GUILayout.Button( "Show Profile Image" ) )
			{
				Facebook.instance.fetchProfileImageForUserId( _userId, ( tex ) =>
				                                             {
					if( tex != null )
						profileImage = tex;
				});
			}
		}
		else
		{
			GUILayout.Label( "Call the me Graph request to show user specific buttons" );
		}

		return profileImage;
	}
	
	#endif
}

