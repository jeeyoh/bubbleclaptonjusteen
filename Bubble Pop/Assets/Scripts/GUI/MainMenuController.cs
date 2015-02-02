using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance;

	[SerializeField] private GameObject m_splashScreen;
	[SerializeField] private GameObject m_mainMenu;	
	[SerializeField] private GameObject[] TimeModeButtons = default ( GameObject[] );
	[SerializeField] private GameObject[] EndlessModeButtons = default ( GameObject[] );
	[SerializeField] private Image m_soundToggle;
	[SerializeField] private Sprite m_soundOn;
	[SerializeField] private Sprite m_soundOff;
	[SerializeField] private GameObject m_blocker;	

	private bool isSoundOn;

	void Awake () {
		instance = this;
	}

	void Start() {
		Init ();
	}

	void Init() {
		isSoundOn = GameController.instance.AllowSound;
		if(isSoundOn) {
			m_soundToggle.sprite = m_soundOn;
		} else {
			m_soundToggle.sprite = m_soundOff;
		}
		m_soundToggle.SetNativeSize();
		m_splashScreen.SetActive(true);
		m_mainMenu.SetActive(false);
		m_blocker.SetActive(false);
		Invoke ("CloseSplashScreen", 1f);
	}

	public void CloseSplashScreen() {
		m_splashScreen.SetActive(false);
		m_mainMenu.SetActive(true);
	}
	
	public void OpenTimeMode() {
		PopEndlessModeBubbles();
		for (int i = 0; i < TimeModeButtons.Length; i++) {
			TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Show");
		}
	}

	public void StartTimeMode(int p_mode) {
		GameController.instance.gameModeType = GameModeType.timeMode;
		switch(p_mode) {
		case Constants.TIME_MODE_50:
			PopTimeModeBubbles(0);
			GameController.instance.gameMode = GameMode.timeMode50;
			break;
		case Constants.TIME_MODE_100:
			PopTimeModeBubbles(1);
			GameController.instance.gameMode = GameMode.timeMode100;
			break;
		case Constants.TIME_MODE_150:
			PopTimeModeBubbles(2);
			GameController.instance.gameMode = GameMode.timeMode150;
			break;
		}
		m_blocker.SetActive(true);
		Invoke ("StartGame", 1f);
	}

	public void OpenEndlessMode() {
		PopTimeModeBubbles();
		for (int i = 0; i < EndlessModeButtons.Length; i++) {
			EndlessModeButtons[i].GetComponent<Animator>().Play("Bubble_Show");
		}
	}

	public void StartEndlessMode(int p_mode) {
		GameController.instance.gameModeType = GameModeType.endlessMode;
		switch(p_mode) {
		case Constants.ENDLESS_MODE_5:
			PopEndlessModeBubbles(0);
			GameController.instance.gameMode = GameMode.endlessMode5;
			break;
		case Constants.ENDLESS_MODE_25:
			PopEndlessModeBubbles(1);
			GameController.instance.gameMode = GameMode.endlessMode25;
			break;
		case Constants.ENDLESS_MODE_50:
			PopEndlessModeBubbles(2);
			GameController.instance.gameMode = GameMode.endlessMode50;
			break;
		}
		m_blocker.SetActive(true);
		Invoke ("StartGame", 1f);
	}

	private void StartGame() {
		GameController.instance.ChangeState(GameState.startGame);
	}

	public void OpenPowerUpMenu() {
		Debug.Log("OpenPowerUpMenu");
		PopTimeModeBubbles();
		PopEndlessModeBubbles();
	}

	public void OpenMoreMenu() {
		Debug.Log("OpenMoreMenu");
		PopTimeModeBubbles();
		PopEndlessModeBubbles();
	}

	private void PopTimeModeBubbles(int p_exempt = -1) {
		for (int i = 0; i < TimeModeButtons.Length; i++) {
			if(i == p_exempt) continue;
			if (TimeModeButtons[i].GetComponent<Image>().enabled) 
				TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
		}
	}

	private void PopEndlessModeBubbles(int p_exempt = -1) {
		for (int i = 0; i < EndlessModeButtons.Length; i++)	{
			if(i == p_exempt) continue;
			if ( EndlessModeButtons[i].GetComponent<Image>().enabled )
				EndlessModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
		}
	}

	public void ToggleSound() {
		isSoundOn = !isSoundOn;
		GameController.instance.AllowSound = isSoundOn;
		if(isSoundOn) {
			m_soundToggle.sprite = m_soundOn;
		} else {
			m_soundToggle.sprite = m_soundOff;
		}
		m_soundToggle.SetNativeSize();
	}
}