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
		Invoke ("CloseSplashScreen", 1f);
	}

	public void CloseSplashScreen() {
		m_splashScreen.SetActive(false);
		m_mainMenu.SetActive(true);
	}
	
	public void OpenTimeMode() {

		for (int i=0; i<EndlessModeButtons.Length; i++)
		{
			if ( EndlessModeButtons[i].GetComponent<Image>().enabled )
				EndlessModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
		}

		for (int i=0; i<TimeModeButtons.Length; i++)
		{
			TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Show");
		}
	}

	public void StartTimeMode(int p_mode) {
		GameController.instance.gameModeType = GameModeType.timeMode;
		switch(p_mode) {
		case Constants.TIME_MODE_50:
			GameController.instance.gameMode = GameMode.timeMode50;
			break;
		case Constants.TIME_MODE_100:
			GameController.instance.gameMode = GameMode.timeMode100;
			break;
		case Constants.TIME_MODE_150:
			GameController.instance.gameMode = GameMode.timeMode150;
			break;
		}

		GameController.instance.ChangeState(GameState.startGame);
	}

	public void OpenEndlessMode() {

		for (int i=0; i<TimeModeButtons.Length; i++)
		{
			if ( TimeModeButtons[i].GetComponent<Image>().enabled )
				TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
		}

		for (int i=0; i<EndlessModeButtons.Length; i++)
		{
			EndlessModeButtons[i].GetComponent<Animator>().Play("Bubble_Show");
		}
	}

	public void StartEndlessMode(int p_mode) {
		GameController.instance.gameModeType = GameModeType.endlessMode;
		switch(p_mode) {
		case Constants.ENDLESS_MODE_5:
			GameController.instance.gameMode = GameMode.endlessMode5;
			break;
		case Constants.ENDLESS_MODE_25:
			GameController.instance.gameMode = GameMode.endlessMode25;
			break;
		case Constants.ENDLESS_MODE_50:
			GameController.instance.gameMode = GameMode.endlessMode50;
			break;
		}
		GameController.instance.ChangeState(GameState.startGame);
	}

	public void OpenPowerUpMenu() {
		Debug.Log("OpenPowerUpMenu");
	}

	public void OpenMoreMenu() {
		Debug.Log("OpenMoreMenu");
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

	IEnumerator StartGame ()
	{
//		for (int i=0; i<TimeModeButtons.Length; i++)
//		{
//			if ( TimeModeButtons[i].GetComponent<Button>().enabled )
//				TimeModeButtons[i].GetComponent<Button>().onClick
//		}

		switch ( GameController.instance.gameMode )
		{
		case GameMode.timeMode50:
			break;
		case GameMode.timeMode100:
			break;
		case GameMode.timeMode150:
			break;
		case GameMode.endlessMode5:
			break;
		case GameMode.endlessMode25:
			break;
		case GameMode.endlessMode50:
			break;
		}

		yield return null;

		GameController.instance.ChangeState(GameState.startGame);
	}
}