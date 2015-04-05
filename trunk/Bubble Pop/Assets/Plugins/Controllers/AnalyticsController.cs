using System.Diagnostics;
using UnityEngine;
using Analytics;

public class AnalyticsController : MonoBehaviour 
{
	IAnalytics service;

	static AnalyticsController instance;
	public static AnalyticsController Instance	
	{
		get 
		{
			if (instance == null)
			{
				instance = (new GameObject("AnalyticsController")).AddComponent<AnalyticsController>();
				instance.Initialize();
				DontDestroyOnLoad( instance.gameObject );
			}
			return instance;
		}
	}

	private void Initialize()
	{
		service = Flurry.Instance;
		
		//AssertNotNull(service, "Unable to create Flurry instance!", this);
		//Assert(!string.IsNullOrEmpty(_iosApiKey), "_iosApiKey is empty!", this);
		//Assert(!string.IsNullOrEmpty(_androidApiKey), "_androidApiKey is empty!", this);
		
		FlurryAndroid.SetLogEnabled(true);
		service.StartSession(ConfigManager.FLURRY_IOS_KEY, ConfigManager.FLURRY_ANDROID_KEY);
	}

	public void LogUserName ( string userName )
	{
		service.LogUserID(userName);
	}

	public void LogUserAge ( int userAge )
	{
		service.LogUserAge(userAge);
	}

	public void LogUserGender ( UserGender user )
	{
		service.LogUserGender(user);
	}

	public void LogUserEvent ( string userEvent )
	{
		service.LogEvent(userEvent);
	}

	public void BeginUserEvent ( string userEvent )
	{
		service.BeginLogEvent(userEvent);
	}

	public void EndUserEvent ( string userEvent )
	{
		service.EndLogEvent(userEvent);
	}
	
	public void LogError ( string errorID, string errorMessage )
	{
		service.LogError(errorID, errorMessage, this);
	}

	#region [Assert Methods]
	[Conditional("UNITY_EDITOR")]
	private void Assert(bool condition, string message, Object context)
	{
		if (condition)
		{
			return;
		}
		
//		Debug.LogError(message, context);
	}
	
	[Conditional("UNITY_EDITOR")]
	private void AssertNotNull(object target, string message, Object context)
	{
		Assert(target != null, message, context);
	}
	#endregion
}

