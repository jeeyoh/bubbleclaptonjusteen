using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance;

	[SerializeField] private GameObject m_splashScreen;
	[SerializeField] private GameObject m_mainMenu;

	void Awake () {
		instance = this;
	}

	void Start() {
		Init ();
	}

	void Init() {
		m_splashScreen.SetActive(true);
		m_mainMenu.SetActive(false);
	}

	public void CloseSplashScreen() {
		m_splashScreen.SetActive(false);
		m_mainMenu.SetActive(true);
	}

	public void LoadGameScene() {
		Debug.Log("Load Game Scene");
	}
	
	public void OpenTimeMode() {
		Debug.Log("OpenTimeMode");
	}

	public void StartTimeMode(int p_mode) {
		Debug.Log("StartTimeMode " + p_mode);
		switch(p_mode) {
		case Constants.TIME_MODE_50:
			break;
		case Constants.TIME_MODE_100:
			break;
		case Constants.TIME_MODE_150:
			break;
		}
	}

	public void OpenEndlessMode() {
		Debug.Log("OpenEndlessMode");
	}

	public void StartEndlessMode(int p_mode) {
		Debug.Log("StartEndlessMode " + p_mode);
		switch(p_mode) {
		case Constants.ENDLESS_MODE_5:
			break;
		case Constants.ENDLESS_MODE_25:
			break;
		case Constants.ENDLESS_MODE_50:
			break;
		}
	}

	public void OpenPowerUpMenu() {
		Debug.Log("OpenPowerUpMenu");
	}

	public void OpenMoreMenu() {
		Debug.Log("OpenMoreMenu");
	}
}