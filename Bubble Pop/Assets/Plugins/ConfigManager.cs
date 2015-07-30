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

	#region Tapjoy
	
	#if UNITY_IPHONE
	public const string TAPJOY_ID = "bfdba7cb-e52d-40d9-8006-002459d42230";
	public const string TAPJOY_KEY = "oUdX6LbfcMSyk35DPQ3Y";
	#elif UNITY_ANDROID
	public const string TAPJOY_ID = "2fa280f2-7595-4bc3-bdc0-a6611aa1e29e";
	public const string TAPJOY_KEY = "GTrAk9lZR6lNB02YCYoQ";
	#else
	public const string TAPJOY_ID = "2fa280f2-7595-4bc3-bdc0-a6611aa1e29e";
	public const string TAPJOY_KEY = "GTrAk9lZR6lNB02YCYoQ";
	#endif
	
	#endregion
	
	#region Facebook

	public const string FACEBOOK_APP_ID = "1626847630861757";
	
	#endregion

	#region Twitter

	public const string TWITTER_CONSUMER_KEY = "7u6kRvv6fonKkxmekajjiRgos";
	public const string TWITTER_CONSUMER_SECRET = "sEhKiXoZD8GDplBgI8q7n1KRQP3sbMwng4HBnmEnfvOP5RUCft";
	public const string TWITTER_OWNER_ID = "3021945414";
	
	#endregion

	#region AdMob
	
	#if UNITY_IPHONE
	public const string ADMOB_BANNER_ID = "ca-app-pub-1399003609876804/1975816575";
	public const string ADMOB_INTERSTITIAL_ID = "ca-app-pub-1399003609876804/3195197777";
	#elif UNITY_ANDROID
	public const string ADMOB_BANNER_ID = "ca-app-pub-1399003609876804/1755490575";
	public const string ADMOB_INTERSTITIAL_ID = "ca-app-pub-1399003609876804/1718464578";
	#else
	public const string ADMOB_BANNER_ID = "NOT iphone or android";
	public const string ADMOB_INTERSTITIAL_ID = "NOT iphone or android";
	#endif

	#endregion

	#region App Store

	#if UNITY_ANDROID
	public const string APP_STORE = "market://details?id=com.mavlabs.bubblepop";
	#else
	public const string APP_STORE = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=973525203";
	#endif

	#endregion

	#region App Store Share
	
	#if UNITY_ANDROID
	public const string FB_APP_STORE = "https://play.google.com/store/apps/details?id=com.mavlabs.bubblepop";
	#else
	public const string FB_APP_STORE = "https://itunes.apple.com/us/app/bubble-poppp/id973525203?mt=8";
	#endif
	
	#endregion
	
	#region InApp Purchases
	
	#if UNITY_IPHONE
	public const string INAPP_API_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgLy+sOBHu1WAUSMssllrcasc0+yvhqYlDBd5biMkUom8Er5ucfEYj1DNQ6VtwugQMj8Vy2JCwMvsL2TXcNUXxK/ymwreKAFVG997vA5di+y5O1GSaWV6nBYmXMFiqwJZCHvL2ctEj3ctCMlgLE//XJCuGB5tWYrchtIsHyx/Uhyx49UFVAacP5S5u/4SEoC/tHuJR18uhXLSGhlSWgIsToPKOEnOysie4165KhTMcpe87VxdHoXIUnS2h1FcLp8PYJf4EYWunXStuvcO+6AjLv1RlPkFCIHaDwU+TLxrm6gLtlSLz+k7VEHLZHGgTitO6apAOs8PWQA+04lH2ygq8QIDAQAB";
	#elif UNITY_ANDROID
	public const string INAPP_API_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgLy+sOBHu1WAUSMssllrcasc0+yvhqYlDBd5biMkUom8Er5ucfEYj1DNQ6VtwugQMj8Vy2JCwMvsL2TXcNUXxK/ymwreKAFVG997vA5di+y5O1GSaWV6nBYmXMFiqwJZCHvL2ctEj3ctCMlgLE//XJCuGB5tWYrchtIsHyx/Uhyx49UFVAacP5S5u/4SEoC/tHuJR18uhXLSGhlSWgIsToPKOEnOysie4165KhTMcpe87VxdHoXIUnS2h1FcLp8PYJf4EYWunXStuvcO+6AjLv1RlPkFCIHaDwU+TLxrm6gLtlSLz+k7VEHLZHGgTitO6apAOs8PWQA+04lH2ygq8QIDAQAB";
	#else
	public const string INAPP_API_KEY = "";
	#endif
	
	#endregion
}
