using UnityEngine;
using System.Collections;

public class MoreMenu : MonoBehaviour {

	public void LikeUsOnFacebook() {
		Debug.Log("Like us on facebook!");
	}

	public void FollowUsOnTwitter() {
		Debug.Log("Follow us on twitter!");
	}

	public void RateOurGame() {
		Debug.Log("Rate our game!");
	}

	public void CloseMoreMenu() {
		this.gameObject.SetActive(false);
	}
}
