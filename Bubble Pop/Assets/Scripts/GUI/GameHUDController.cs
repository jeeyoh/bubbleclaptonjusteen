using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUDController : MonoBehaviour {

	public static GameHUDController instance {get; private set;}

	private const string BUBBLES_TO_POP = "Bubbles to Pop : ";
	private const string BUBBLES_COUNT = "Bubbles Count : ";

	[SerializeField] private GameObject m_timeModeImg;
	[SerializeField] private GameObject m_endlessModeImg;
//	[SerializeField] private GameObject m_startBtn;
	[SerializeField] private Text m_stopWatchText;
	[SerializeField] private Text m_bubblesText;
	[SerializeField] private Text m_bubblesCountText;
	[SerializeField] private GameObject m_returnMenu;
	[SerializeField] private GameObject m_guide;
	[SerializeField] private Text m_timeModeGuideText;
	[SerializeField] private Text m_endlessModeGuideText;
	[SerializeField] private GameObject m_blocker;
	
	private float m_timeStopWatch;
	private bool m_isTimeRunning;
	private int m_bubblesCount;
	private int m_maxBubbles;

	public bool isGameOver = false;

	void Awake() {
		instance = this;

		if ( ThirdPartyController.Instance.CheckAdsCounter () )
			ThirdPartyController.Instance.ShowInterstitial(true);
	}

	void Start() {
		Init ();
		GameController.instance.OnGameOver += GameOver;
	}

	void OnDestroy (){
		GameObject.Destroy (instance);
	}
	
	void OnDisable() {
		GameController.instance.OnGameOver -= GameOver;
	}

	private void GameOver() {
		isGameOver = true;
	}

	private void Init() {
		m_returnMenu.SetActive(false);
		m_blocker.SetActive(true);
		ShowGuide();
		SoundController.instance.PlayMenuBGM(1f);
		if(GameController.instance.gameModeType == GameModeType.timeMode) {
			m_timeModeImg.SetActive(true);
			m_endlessModeImg.SetActive(false);
		} else if(GameController.instance.gameModeType == GameModeType.endlessMode) {
			m_timeModeImg.SetActive(false);
			m_endlessModeImg.SetActive(true);
		}

//		m_startBtn.SetActive(true);
		ResetTime();
		InitBubbleCounter();
//		Invoke("HideGuide", 2f);
	}

	private void InitBubbleCounter() {
		GameModeType _gameModeType = GameController.instance.gameModeType;
		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameModeType) {
		case GameModeType.timeMode:
			switch(_gameMode) {
			case GameMode.timeMode50:
				m_bubblesCount = 0;
				m_maxBubbles = 50;
				break;
			case GameMode.timeMode100:
				m_bubblesCount = 0;
				m_maxBubbles = 100;
				break;
			case GameMode.timeMode150:
				m_bubblesCount = 0;
				m_maxBubbles = 150;
				break;
			}
			m_bubblesText.text = BUBBLES_TO_POP;
			m_bubblesCountText.text = m_bubblesCount + "/" + m_maxBubbles;
			break;
		case GameModeType.endlessMode:
			switch(_gameMode) {
			case GameMode.endlessMode5:
				m_bubblesCount = 5;
				m_maxBubbles = 5;
				break;
			case GameMode.endlessMode25:
				m_bubblesCount = 25;
				m_maxBubbles = 25;
				break;
			case GameMode.endlessMode50:
				m_bubblesCount = 50;
				m_maxBubbles = 50;
				break;
			}
			m_bubblesText.text = BUBBLES_COUNT;
			m_bubblesCountText.text = m_bubblesCount + "/" + m_maxBubbles;
			break;
		}
	}

	private void ShowGuide() {
		m_guide.SetActive(true);
		if(GameController.instance.gameModeType == GameModeType.timeMode) {
			m_timeModeGuideText.gameObject.SetActive(true);
			m_endlessModeGuideText.gameObject.SetActive(false);
		} else if (GameController.instance.gameModeType == GameModeType.endlessMode) {
			m_endlessModeGuideText.gameObject.SetActive(true);
			m_timeModeGuideText.gameObject.SetActive(false);
			string _guideText = "Don't let those \nbubbles exceed "; // 5!";
			switch(GameController.instance.gameMode) {
			case GameMode.endlessMode5:
				_guideText += "5!";
				break;
			case GameMode.endlessMode25:
				_guideText += "25!";
				break;
			case GameMode.endlessMode50:
				_guideText += "50!";
				break;
			}
			m_endlessModeGuideText.text = _guideText;
		}
	}

	public void StartGame() {
		HideGuide();
		BubbleCreator.instance.StartGame();
		StartTime();
	}

	private void HideGuide() {
		m_guide.SetActive(false);
		m_blocker.SetActive(false);
	}

//	public void StartGame() {
//		m_startBtn.SetActive(false);
//		GameController.instance.ChangeState(GameState.playing);
//		LevelController.instance.StartGame();
//		StartTime();
//	}

	public void StartTime() {
		m_isTimeRunning = true;
	}

	public void StopTime() {
		m_isTimeRunning = false;
	}

	public float GetTime() {
		return m_timeStopWatch;
	}

	public void ResetTime() {
		m_timeStopWatch = 0;
		m_stopWatchText.text = "0s";
		m_isTimeRunning = false;
	}

	public void SetBubblesCount(int p_count) {
		m_bubblesCount = p_count;
	}

	public void OpenReturnMenu() {
		if(isGameOver) return;

		Time.timeScale = 0;
		SoundController.instance.SetSounds(false, true);
		m_returnMenu.SetActive(true);
		m_blocker.SetActive(true);
	}

	public void ReturnToMain() {
		Time.timeScale = 1;
		
		ThirdPartyController.Instance.IncreaseAdsCounter ();
		bool _sound = GameController.instance.AllowSound;
		SoundController.instance.SetSounds(_sound, _sound);
		GameController.instance.ChangeState(GameState.main);
	}

	public void CloseReturnMenu() {
		Time.timeScale = 1;
		bool _sound = GameController.instance.AllowSound;
		SoundController.instance.SetSounds(_sound, _sound);
		m_returnMenu.SetActive(false);
		m_blocker.SetActive(false);
	}
	
	void Update() {
		if(m_isTimeRunning) {
			m_timeStopWatch += Time.deltaTime;
			m_stopWatchText.text = m_timeStopWatch.ToString("F2") + "s";
		}

		m_bubblesCount = BubbleCreator.instance.goodBubblesCount;
		m_bubblesCountText.text = m_bubblesCount + "/" + m_maxBubbles;
	}
}
