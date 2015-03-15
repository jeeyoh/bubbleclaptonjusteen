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
	public const string MAT_PACKAGE_NAME = "com.mavlabs.bubblepop";
	//public const string MAT_SITE_ID = "";
	#else
	public const string MAT_ADVERTISER_ID = "";
	public const string MAT_CONVERSION_KEY = "";
	public const string MAT_PACKAGE_NAME = "";
	//public const string MAT_SITE_ID = "";
	#endif
	
	#endregion

	#region Twitter

	public const string TWITTER_CONSUMER_KEY = "7u6kRvv6fonKkxmekajjiRgos";
	public const string TWITTER_CONSUMER_SECRET = "sEhKiXoZD8GDplBgI8q7n1KRQP3sbMwng4HBnmEnfvOP5RUCft";
	public const string TWITTER_OWNER_ID = "3021945414";
	
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
