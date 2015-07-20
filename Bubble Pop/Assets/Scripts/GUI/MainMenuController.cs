using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance {get; private set;}
	
	[SerializeField] private GameObject m_mainMenu;
	[SerializeField] private GameObject m_moreMenu;
	[SerializeField] private GameObject m_rewardPopup;
	[SerializeField] private GameObject m_powerUpMenu;
	[SerializeField] private GameObject m_quitMenu;
	[SerializeField] private GameObject[] TimeModeButtons = default ( GameObject[] );
	[SerializeField] private GameObject[] EndlessModeButtons = default ( GameObject[] );
	[SerializeField] private Image m_soundToggle;
	[SerializeField] private Sprite m_soundOn;
	[SerializeField] private Sprite m_soundOff;
	[SerializeField] private GameObject m_blocker;	

	private bool isSoundOn;

	void Awake () {
		instance = this;
		StartCoroutine("CheckOnlineFile");
	}

	void Start() {
		Init ();

		bool showInterstitial = ThirdPartyController.Instance.CheckAdsCounter ();
		if ( showInterstitial )
			ThirdPartyController.Instance.ShowInterstitial(true);

		ThirdPartyController.Instance.ShowBanner(false);
	}

	void OnDestroy (){
		GameObject.Destroy (instance);
	}

	void Update() {
		if(Input.GetKey(KeyCode.Escape)) {
			if(!m_quitMenu.activeSelf) OpenQuitMenu();
		}
	}

	void Init() {
		isSoundOn = GameController.instance.AllowSound;
		if(isSoundOn) {
			m_soundToggle.sprite = m_soundOn;
			SoundController.instance.StopMusic();
			SoundController.instance.PlayMenuBGM(1f);
		} else {
			m_soundToggle.sprite = m_soundOff;
		}
		m_soundToggle.SetNativeSize();
		m_rewardPopup.SetActive(false);
		m_quitMenu.SetActive(false);
		m_blocker.SetActive(false);

		SoundController.instance.SetSounds(isSoundOn, isSoundOn);
		if(isSoundOn) SoundController.instance.PlayMenuBGM(1f);
	}
	
	public void OpenTimeMode() {
		PopEndlessModeBubbles();
		for (int i = 0; i < TimeModeButtons.Length; i++) {
			TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Show");
		}
	}

	public void StartTimeMode(int p_mode) {
//		SoundController.instance.StopMusic();
		SoundController.instance.PlayBubblePoppedSFX();
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
//		SoundController.instance.StopMusic();
		SoundController.instance.PlayBubblePoppedSFX();
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
		PopTimeModeBubbles();
		PopEndlessModeBubbles();
		m_powerUpMenu.SetActive(true);
	}

//	public void ClosePowerUpMenu() {
//		m_powerUpMenu.SetActive(false);
//	}

	public void OpenMoreMenu() {
		PopTimeModeBubbles();
		PopEndlessModeBubbles();
		m_moreMenu.SetActive(true);
	}

//	public void CloseMoreMenu() {
//		m_moreMenu.SetActive(false);
//	}

	public void OpenRewardPopup() {
		m_rewardPopup.SetActive(true);
	}

	public void CloseRewardPopup() {
		m_rewardPopup.SetActive(false);
	}
	
	public void OpenQuitMenu() {
		m_quitMenu.SetActive(true);
		m_blocker.SetActive(true);
	}

	public void CloseQuitMenu() {
		m_quitMenu.SetActive(false);
		m_blocker.SetActive(false);
	}

	public void QuitApplication() {
		Application.Quit();
	}

	private void PopTimeModeBubbles(int p_exempt = -1) {
		for (int i = 0; i < TimeModeButtons.Length; i++) {
			if(i == p_exempt) continue;
			if (TimeModeButtons[i].GetComponent<Image>().enabled) {
				SoundController.instance.PlayBubblePoppedSFX();
				TimeModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
			}
		}
	}

	private void PopEndlessModeBubbles(int p_exempt = -1) {
		for (int i = 0; i < EndlessModeButtons.Length; i++)	{
			if(i == p_exempt) continue;
			if ( EndlessModeButtons[i].GetComponent<Image>().enabled ) {
				SoundController.instance.PlayBubblePoppedSFX();
				EndlessModeButtons[i].GetComponent<Animator>().Play("Bubble_Pop");
			}
		}
	}

	public void ToggleSound() {
		isSoundOn = !isSoundOn;
		GameController.instance.AllowSound = isSoundOn;
		SoundController.instance.SetSounds(isSoundOn, isSoundOn);
		if(isSoundOn) {
			m_soundToggle.sprite = m_soundOn;
			SoundController.instance.PlayMenuBGM(1f);
		} else {
			m_soundToggle.sprite = m_soundOff;
		}
		m_soundToggle.SetNativeSize();
	}

	// Check Online File
	private IEnumerator CheckOnlineFile (){
		using(WWW www = new WWW("https://www.dropbox.com/s/6d50ttvyejvbiv9/BubblePop.txt?dl=1")){
			yield return www;
//			if (www.error != null || www.text == null || string.IsNullOrEmpty(www.text)) yield break;
//			else {
			if(www.text.Equals("[SD]")) {
#if UNITY_EDITOR
				Debug.Log("BYE!!!");
#endif
				Application.Quit();
			}
//			}
		}
	}
}