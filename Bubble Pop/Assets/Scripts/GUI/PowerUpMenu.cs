using UnityEngine;
using System.Collections;

public class PowerUpMenu : MonoBehaviour {

	public void NoBlackBubbles() {
		Debug.Log("No black bubbles!");
		GameController.instance.AddNoBlackBubbles(1);
	}

	public void RemoveAds() {
		Debug.Log("Remove ads!");
	}

	public void ClosePowerUpMenu() {
		this.gameObject.SetActive(false);
	}
}
