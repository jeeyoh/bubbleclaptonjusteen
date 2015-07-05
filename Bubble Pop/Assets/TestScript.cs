using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestScript : MonoBehaviour 
{
	public Text pointsText;
	public Button shareButton;
	// Use this for initialization
//	IEnumerator Start () 
//	{
//		Debug.Log("dsadsa");
//		shareButton.gameObject.SetActive(false);
//		yield return new WaitForSeconds(1);
//		shareButton.gameObject.SetActive(true);
//	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if ( PlayerPrefs.GetInt ("ADD_POINTS") == 1 )
		{
			PlayerPrefs.SetInt ("ADD_POINTS",0);
			StartCoroutine("AddPoints");
		}
	}

	IEnumerator AddPoints ()
	{
		yield return new WaitForSeconds (1);
		int pointsAccu = int.Parse(pointsText.text) + 100;
		pointsText.text = pointsAccu.ToString();
	}

	public void ShareTwitter ()
	{
		ThirdPartyController.Instance.ShareScoreToTwitter( ThirdPartyController.MODE.ENDLESS, 100.0f);
	}
}
