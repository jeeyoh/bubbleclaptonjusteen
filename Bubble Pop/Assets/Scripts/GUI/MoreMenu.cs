using UnityEngine;
using System.Collections;

public class MoreMenu : MonoBehaviour {

	public void LikeUsOnFacebook() {
		bool _likedOnFB = ThirdPartyController.Instance.LikeUsOnFacebook();
		if(_likedOnFB) {
			GameController.instance.AddNoBlackBubbles(1);
			MainMenuController.instance.Invoke("OpenRewardPopup", 1f);
		}
	}

	public void FollowUsOnTwitter() {
		bool _followOnTwitter = ThirdPartyController.Instance.FollowUsOnTwitter();
		if(_followOnTwitter) {
			GameController.instance.AddNoBlackBubbles(1); 
			MainMenuController.instance.Invoke("OpenRewardPopup", 1f);
		}
	}

	public void RateOurGame() {
		bool _rateOurGame = ThirdPartyController.Instance.RateOurApp();
		if(_rateOurGame) {
			GameController.instance.AddNoBlackBubbles(1); 
			MainMenuController.instance.Invoke("OpenRewardPopup", 1f);
		}
	}

	public void ShowRewards() {
		Debug.Log("Rewards");
		ThirdPartyController.Instance.ShowRewards();
	}

	public void CloseMoreMenu() {
		this.gameObject.SetActive(false);
	}
}
