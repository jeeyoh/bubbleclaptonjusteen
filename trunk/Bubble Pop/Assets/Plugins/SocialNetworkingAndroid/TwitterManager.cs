using UnityEngine;
using System;


public partial class TwitterManager : MonoBehaviour
{
	// Android only. Fired after the Twitter service is initialized and ready for use.
	public static event Action twitterInitializedEvent;


#if UNITY_ANDROID
	public void twitterInitialized()
	{
		if( twitterInitializedEvent != null )
			twitterInitializedEvent();
	}
#endif
}
