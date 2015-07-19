using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FacebookController : MonoBehaviour
{
	#if UNITY_IPHONE || UNITY_ANDROID	
	protected string lastResponse = "";

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

	void Initialize()
	{
		CallFBInit();
	}

	#region FB.Init()
	
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	#endregion


	#region FB.Login()
	public void Login ()
	{
		CallFBLogin();
	}
	
	private void CallFBLogin()
	{
		FB.Login("public_profile,email,user_friends", LoginCallback);
	}
	
	private void CallFBLoginForPublish()
	{
		// It is generally good behavior to split asking for read and publish
		// permissions rather than ask for them all at once.
		//
		// In your own game, consider postponing this call until the moment
		// you actually need it.
		FB.Login("publish_actions", LoginCallback);
	}
	
	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
			lastResponse = "Error Response:\n" + result.Error;
		else if (!FB.IsLoggedIn)
		{
			lastResponse = "Login cancelled by Player";
			
			ThirdPartyController.Instance.fbLoginFail ();
		}
		else
		{
			lastResponse = "Login was successful!";

			ThirdPartyController.Instance.ShareFB();
			ThirdPartyController.Instance.fbLoginInSuccess ();
		}
	}
	
	private void CallFBLogout()
	{
		FB.Logout();
	}

	public bool isSessionValid ()
	{
		return FB.IsLoggedIn;
	}
	#endregion


	#region FB.Feed() example
	
	public string FeedToId = "";
	public string FeedLink = "";
	public string FeedLinkName = "";
	public string FeedLinkCaption = "";
	public string FeedLinkDescription = "";
	public string FeedPicture = "";
	public string FeedMediaSource = "";
	public string FeedActionName = "";
	public string FeedActionLink = "";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	public void ShowShareDialog ( string link, string name, string picture, string caption, string description )
	{
		FeedLink = link;
		FeedLinkName = name;
		FeedLinkCaption = caption;
		FeedLinkDescription = description;
		FeedPicture = picture;
		FeedActionLink = ConfigManager.FB_APP_STORE;

		CallFBFeed ();
	}

	private void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (IncludeFeedProperties)
		{
			feedProperties = FeedProperties;
		}
		FB.Feed(
			toId: FeedToId,
			link: FeedLink,
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: FeedPicture,
			mediaSource: FeedMediaSource,
			actionName: FeedActionName,
			actionLink: FeedActionLink,
			reference: FeedReference,
			properties: feedProperties
			);
	}
	
	#endregion

	#endif
}
