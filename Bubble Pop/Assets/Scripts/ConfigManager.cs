using System;
using UnityEngine;


// ConfigManager provided by Nickelodeon.
// Note: Android is usually written with only the A capitalized, iOS is all capitals unless at the very beginning of a variable.

// Use ConfigManager for any plugin values used at runtime.
public class ConfigManager
{
	
	#region ConfigManager
	
	#if UNITY_IPHONE
	public const string CONFIG_VALUE = "UNITY_IPHONE";
	#elif UNITY_ANDROID
	public const string CONFIG_VALUE = "UNITY_ANDROID";
	#else
	public const string CONFIG_VALUE = "NOT iphone or android";
	#endif
	
	public const string VERSION_VALUE = "1.0";
	
	#endregion
	
	#region Flurry

	public const string FLURRY_IOS_KEY = "VCRWC44DSRMNFJR5J83N";
	public const string FLURRY_ANDROID_KEY = "8MW3KX97RZFNZTFRS77H";

	#endregion
	
	#region Facebook
	
	#if UNITY_IPHONE
	public const string MAT_ADVERTISER_ID = "19512";
	public const string MAT_CONVERSION_KEY = "bf719e4c5efcbd96e08ef752e83ae230";
	public const string MAT_PACKAGE_NAME = "com.mtvn.Nickelodeon.UnityTemplateProject";
	//public const string MAT_SITE_ID = "";
	#elif UNITY_ANDROID
	public const string MAT_ADVERTISER_ID = "19512";
	public const string MAT_CONVERSION_KEY = "bf719e4c5efcbd96e08ef752e83ae230";
	public const string MAT_PACKAGE_NAME = "com.mtvn.Nickelodeon.UnityTemplateProject";
	//public const string MAT_SITE_ID = "";
	#else
	public const string MAT_ADVERTISER_ID = "";
	public const string MAT_CONVERSION_KEY = "";
	public const string MAT_PACKAGE_NAME = "";
	//public const string MAT_SITE_ID = "";
	#endif
	
	#endregion

	
	#region InApp Purchases
	
	#if UNITY_IPHONE
	public const string INAPP_API_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArk7/qgZ0F/Y5ZKBM85dalsH7bRF+ePe8CnGquy4SuNy3uFP8J3UKBXl1Rdszkes6G5IdnpP517SPsAn44yAFj2zm6TwE+qHcEiA+Ph4PM96vND8QtDNzTVQO4Hvlo5vehOUCvXrjPzmgzfT1+Uk9mEuX3j978KMU2xRnofwZhTquWSdyRSzbo5o5zy1FrLgWChdqOxsyXhCEn0l3F2Tr29Y5JL1xfunSCzBzUcCyy2FDbSL1ThkRQ3V7vGTx33kgadRMQ0kGKzbn+1rBIw52L4KHnuSF5aPs6Xz8AlDg8SwYJRU9U4vOV5Ui+0WUUXfjzvaV7I3ft6y7DG//OG9qZwIDAQAB";
	#elif UNITY_ANDROID
	public const string INAPP_API_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArk7/qgZ0F/Y5ZKBM85dalsH7bRF+ePe8CnGquy4SuNy3uFP8J3UKBXl1Rdszkes6G5IdnpP517SPsAn44yAFj2zm6TwE+qHcEiA+Ph4PM96vND8QtDNzTVQO4Hvlo5vehOUCvXrjPzmgzfT1+Uk9mEuX3j978KMU2xRnofwZhTquWSdyRSzbo5o5zy1FrLgWChdqOxsyXhCEn0l3F2Tr29Y5JL1xfunSCzBzUcCyy2FDbSL1ThkRQ3V7vGTx33kgadRMQ0kGKzbn+1rBIw52L4KHnuSF5aPs6Xz8AlDg8SwYJRU9U4vOV5Ui+0WUUXfjzvaV7I3ft6y7DG//OG9qZwIDAQAB";
	#else
	public const string INAPP_API_KEY = "";
	#endif
	
	#endregion
}
