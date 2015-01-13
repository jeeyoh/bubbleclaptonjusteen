using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance;
	
	void Awake () {
		instance = this;
	}

	public void LoadGameScene() {
		Debug.Log("Load Game Scene");
	}
	
	public void OpenSettings() {
		Debug.Log("Settings");
	}
}
