using UnityEngine;
using System.Collections;

public class MoreMenu : MonoBehaviour {

	public void LikeUsOnFacebook() {
		Debug.Log("Like us on facebook!");
		ThirdPartyController.Instance.LikeUsOnFacebook();
	}

	public void FollowUsOnTwitter() {
		Debug.Log("Follow us on twitter!");
		ThirdPartyController.Instance.FollowUsOnTwitter();
	}

	public void RateOurGame() {
		Debug.Log("Rate our game!");
		ThirdPartyController.Instance.RateOurApp();
	}

	public void ShowRewards() {
		Debug.Log("Rewards");
		ThirdPartyController.Instance.ShowRewards();
	}

	public void CloseMoreMenu() {
		this.gameObject.SetActive(false);
	}
}
