 using UnityEngine;
using System.Collections;

public enum GameState {
	main = 0,
	startGame,
	playing,
	paused,
	gameOver,
	playAgain,
}

public enum GameModeType {
	timeMode,
	endlessMode,
}

public enum GameMode {
	timeMode50,
	timeMode100,
	timeMode150,
	endlessMode5,
	endlessMode25,
	endlessMode50,
}

public class GameController : MonoBehaviour {

	public static GameController instance { get; private set; }

	public GameState gameState = GameState.main;
	public GameModeType gameModeType;
	public GameMode gameMode;
	public bool allowAds;
	public bool allowSound;
	public bool timeModeSuccess;
	public bool isBlackBubbleClicked;
	public float playerTimeScore;
	public float timeMode50BestTime;
	public float timeMode100BestTime;
	public float timeMode150BestTime;
	public float endlessMode5BestTime;
	public float endlessMode25BestTime;
	public float endlessMode50BestTime;
	public bool isPaused = false;
	public bool deletePlayerPrefs;
	public int noBlackBubblesCountAllModes;
	public int noBlackBubblesCountTimeMode50;
	public int noBlackBubblesCountTimeMode100;
	public int noBlackBubblesCountTimeMode150;
	public int noBlackBubblesCountEndlessMode5;
	public int noBlackBubblesCountEndlessMode25;
	public int noBlackBubblesCountEndlessMode50;
	public int timeMode50SucceedingWins;
	public int timeMode100SucceedingWins;
	public int timeMode150SucceedingWins;
	public int endlessMode5SucceedingWins;
	public int endlessMode25SucceedingWins;
	public int endlessMode50SucceedingWins;

	public delegate void GameOverEvent();
	public event GameOverEvent OnGameOver;

	public bool AllowAds {
		get { 
			allowAds = PlayerPrefsManager.GetBool(PlayerPrefsManager.ALLOW_ADS, true);
			return allowAds;
		}
		set {
			allowAds = value;
			PlayerPrefsManager.SetBool(PlayerPrefsManager.ALLOW_ADS, value);
		}
	}

	public bool AllowSound {
		get { 
			allowSound = PlayerPrefsManager.GetBool(PlayerPrefsManager.ALLOW_SOUND, true);
			return allowSound;
		}
		set {
			allowSound = value;
			PlayerPrefsManager.SetBool(PlayerPrefsManager.ALLOW_SOUND, value);
		}
	}

	public float TimeMode50BestTime {
		get { 
			timeMode50BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.TIME_MODE_50_BEST_TIME, -1);
			return  timeMode50BestTime;
		}
		set {
			timeMode50BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.TIME_MODE_50_BEST_TIME, value);
		}
	}

	public float TimeMode100BestTime {
		get { 
			timeMode100BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.TIME_MODE_100_BEST_TIME, -1);
			return  timeMode100BestTime;
		}
		set {
			timeMode100BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.TIME_MODE_100_BEST_TIME, value);
		}
	}

	public float TimeMode150BestTime {
		get { 
			timeMode150BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.TIME_MODE_150_BEST_TIME, -1);
			return  timeMode150BestTime;
		}
		set {
			timeMode150BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.TIME_MODE_150_BEST_TIME, value);
		}
	}

	public float EndlessMode5BestTime {
		get { 
			endlessMode5BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.ENDLESS_MODE_5_BEST_TIME, 0);
			return  endlessMode5BestTime;
		}
		set {
			endlessMode5BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.ENDLESS_MODE_5_BEST_TIME, value);
		}
	}

	public float EndlessMode25BestTime {
		get { 
			endlessMode25BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.ENDLESS_MODE_25_BEST_TIME, 0);
			return  endlessMode25BestTime;
		}
		set {
			endlessMode25BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.ENDLESS_MODE_25_BEST_TIME, value);
		}
	}

	public float EndlessMode50BestTime {
		get { 
			endlessMode50BestTime = PlayerPrefsManager.GetFloat(PlayerPrefsManager.ENDLESS_MODE_50_BEST_TIME, 0);
			return  endlessMode50BestTime;
		}
		set {
			endlessMode50BestTime = value;
			PlayerPrefsManager.SetFloat(PlayerPrefsManager.ENDLESS_MODE_50_BEST_TIME, value);
		}
	}

	public int NoBlackBubblesCountAllModes {
		get {
			noBlackBubblesCountAllModes = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_All_MODES, 0);
			return noBlackBubblesCountAllModes;
		}
		set {
			noBlackBubblesCountAllModes = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_All_MODES, value);
		}
	}

	public int NoBlackBubblesCountTimeMode50 {
		get {
			noBlackBubblesCountTimeMode50 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_50, 0);
			return noBlackBubblesCountTimeMode50;
		}
		set {
			noBlackBubblesCountTimeMode50 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_50, value);
		}
	}

	public int NoBlackBubblesCountTimeMode100 {
		get {
			noBlackBubblesCountTimeMode100 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_100, 0);
			return noBlackBubblesCountTimeMode100;
		}
		set {
			noBlackBubblesCountTimeMode100 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_100, value);
		}
	}

	public int NoBlackBubblesCountTimeMode150 {
		get {
			noBlackBubblesCountTimeMode150 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_150, 0);
			return noBlackBubblesCountTimeMode150;
		}
		set {
			noBlackBubblesCountTimeMode150 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_TIME_MODE_150, value);
		}
	}

	public int NoBlackBubblesCountEndlessMode5 {
		get {
			noBlackBubblesCountEndlessMode5 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_5, 0);
			return noBlackBubblesCountEndlessMode5;
		}
		set {
			noBlackBubblesCountEndlessMode5 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_5, value);
		}
	}

	public int NoBlackBubblesCountEndlessMode25 {
		get {
			noBlackBubblesCountEndlessMode25 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_25, 0);
			return noBlackBubblesCountEndlessMode25;
		}
		set {
			noBlackBubblesCountEndlessMode25 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_25, value);
		}
	}

	public int NoBlackBubblesCountEndlessMode50 {
		get {
			noBlackBubblesCountEndlessMode50 = PlayerPrefsManager.GetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_50, 0);
			return noBlackBubblesCountEndlessMode50;
		}
		set {
			noBlackBubblesCountEndlessMode50 = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_50, value);
		}
	}

	public int TimeMode50SucceedingWins {
		get {
			timeMode50SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.TIME_MODE_50_SUCCEEDING_WINS, 0);
			return timeMode50SucceedingWins;
		}
		set {
			timeMode50SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.TIME_MODE_50_SUCCEEDING_WINS, value);
		}
	}

	public int TimeMode100SucceedingWins {
		get {
			timeMode100SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.TIME_MODE_100_SUCCEEDING_WINS, 0);
			return timeMode100SucceedingWins;
		}
		set {
			timeMode100SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.TIME_MODE_100_SUCCEEDING_WINS, value);
		}
	}

	public int TimeMode150SucceedingWins {
		get {
			timeMode150SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.TIME_MODE_150_SUCCEEDING_WINS, 0);
			return timeMode150SucceedingWins;
		}
		set {
			timeMode150SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.TIME_MODE_150_SUCCEEDING_WINS, value);
		}
	}

	public int EndlessMode5SucceedingWins {
		get {
			endlessMode5SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.ENDLESS_MODE_5_SUCCEEDING_WINS, 0);
			return endlessMode5SucceedingWins;
		}
		set {
			endlessMode5SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.ENDLESS_MODE_5_SUCCEEDING_WINS, value);
		}
	}

	public int EndlessMode25SucceedingWins {
		get {
			endlessMode25SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.ENDLESS_MODE_25_SUCCEEDING_WINS, 0);
			return endlessMode25SucceedingWins;
		}
		set {
			endlessMode25SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.ENDLESS_MODE_25_SUCCEEDING_WINS, value);
		}
	}

	public int EndlessMode50SucceedingWins {
		get {
			endlessMode50SucceedingWins = PlayerPrefsManager.GetInt(PlayerPrefsManager.ENDLESS_MODE_50_SUCCEEDING_WINS, 0);
			return endlessMode50SucceedingWins;
		}
		set {
			endlessMode50SucceedingWins = value;
			PlayerPrefsManager.SetInt(PlayerPrefsManager.ENDLESS_MODE_50_SUCCEEDING_WINS, value);
		}
	}

	void Awake() {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Start() {
		if(deletePlayerPrefs) PlayerPrefs.DeleteAll();
		Init ();
	}

	private void Init() {
		allowAds = AllowAds;
		allowSound = AllowSound;
		timeMode50BestTime = TimeMode50BestTime;
		timeMode100BestTime = TimeMode100BestTime;
		timeMode150BestTime = TimeMode150BestTime;
		endlessMode5BestTime = EndlessMode5BestTime;
		endlessMode25BestTime = EndlessMode25BestTime;
		endlessMode50BestTime = EndlessMode50BestTime;
		noBlackBubblesCountAllModes = NoBlackBubblesCountAllModes;
		noBlackBubblesCountTimeMode50 = NoBlackBubblesCountTimeMode50;
		noBlackBubblesCountTimeMode100 = NoBlackBubblesCountTimeMode100;
		noBlackBubblesCountTimeMode150 = NoBlackBubblesCountTimeMode150;
		noBlackBubblesCountEndlessMode5 = NoBlackBubblesCountEndlessMode5;
		noBlackBubblesCountEndlessMode25 = NoBlackBubblesCountEndlessMode25;
		noBlackBubblesCountEndlessMode50 = NoBlackBubblesCountEndlessMode50;
		timeMode50SucceedingWins = TimeMode50SucceedingWins;
		timeMode100SucceedingWins = TimeMode100SucceedingWins;
		timeMode150SucceedingWins = TimeMode150SucceedingWins;
		endlessMode5SucceedingWins = EndlessMode5SucceedingWins;
		endlessMode25SucceedingWins = EndlessMode25SucceedingWins;
		endlessMode50SucceedingWins = EndlessMode50SucceedingWins;
	}

	public void ChangeState(GameState p_newState) {
		gameState = p_newState;
		ProcessGameState();
	}

	private void ProcessGameState() {
		switch(gameState) {
		case GameState.main:
			LoadMainScene();
//			ThirdPartyController.Instance.ShowInterstitial(true);
//			ThirdPartyController.Instance.ShowBanner(false);
			break;
		case GameState.startGame:
			LoadGameScene();
			ThirdPartyController.Instance.ShowBanner(false);
			break;
		case GameState.playing:
			Resume();
			break;
		case GameState.paused:
			Pause();
			break;
		case GameState.gameOver:
			GameOver();
//			ThirdPartyController.Instance.ShowBanner(true);
//			ThirdPartyController.Instance.ShowInterstitial(true);
			break;
		case GameState.playAgain:
			//			PlayAgain();
			ThirdPartyController.Instance.ShowBanner(false);
			break;
		}
	}

	private void LoadMainScene() {
		StartCoroutine(LoadScene("MainScene", 0));
	}

	private void LoadGameScene() {
		timeModeSuccess = false;
		isBlackBubbleClicked = false;
		StartCoroutine(LoadScene("GameScene", 0));
	}

	private void Pause() {
		isPaused = true;
		Time.timeScale = 0;
	}

	private void Resume() {
		isPaused = false;
		Time.timeScale = 1;
	}

	private void GameOver() {
		if(OnGameOver != null) OnGameOver();
//		SoundController.instance.StopMusic();
		RecordScore();
		StartCoroutine(LoadScene("GameOverScene", 1.5f));
	}

	private IEnumerator LoadScene(string p_sceneName, float p_delay) {
		yield return new WaitForSeconds(p_delay);
		Application.LoadLevel(p_sceneName);
	}

	private void RecordScore() {
		GameHUDController.instance.StopTime();
		switch(gameModeType) {
		case GameModeType.timeMode:
			if(timeModeSuccess) {
				playerTimeScore = GameHUDController.instance.GetTime();
				switch(gameMode) {
				case GameMode.timeMode50:
					TimeMode50SucceedingWins++;
					if(TimeMode50BestTime == -1) {
						TimeMode50BestTime = playerTimeScore;
					} else {
						TimeMode50BestTime = ((playerTimeScore < TimeMode50BestTime) ? playerTimeScore : TimeMode50BestTime);
					}
					break;
				case GameMode.timeMode100:
					TimeMode100SucceedingWins++;
					if(TimeMode100BestTime == -1) {
						TimeMode100BestTime = playerTimeScore;
					} else {
						TimeMode100BestTime = ((playerTimeScore < TimeMode100BestTime) ? playerTimeScore : TimeMode100BestTime);
					}
					break;
				case GameMode.timeMode150:
					TimeMode150SucceedingWins++;
					if(TimeMode150BestTime == -1) {
						TimeMode150BestTime = playerTimeScore;
					} else {
						TimeMode150BestTime = ((playerTimeScore < TimeMode150BestTime) ? playerTimeScore : TimeMode150BestTime);
					}
					break;
				}
			} else {
				playerTimeScore = -1;
			}
			break;
		case GameModeType.endlessMode:
			playerTimeScore = GameHUDController.instance.GetTime();
			switch(gameMode) {
			case GameMode.endlessMode5:
				if(playerTimeScore >= 35f) {
					timeModeSuccess = true;
					EndlessMode5SucceedingWins++;
				}
				if(EndlessMode5BestTime == 0) {
					EndlessMode5BestTime = playerTimeScore;
				} else {
					EndlessMode5BestTime = ((playerTimeScore > EndlessMode5BestTime) ? playerTimeScore : EndlessMode5BestTime);
				}
				break;
			case GameMode.endlessMode25:
				if(playerTimeScore >= 40f) {
					timeModeSuccess = true;
					EndlessMode25SucceedingWins++;
				}
				if(EndlessMode25BestTime == 0) {
					EndlessMode25BestTime = playerTimeScore;
				} else {
					EndlessMode25BestTime = ((playerTimeScore > EndlessMode25BestTime) ? playerTimeScore : EndlessMode25BestTime);
				}
				break;
			case GameMode.endlessMode50:
				if(playerTimeScore >= 45f) {
					timeModeSuccess = true;
					EndlessMode50SucceedingWins++;
				}
				if(EndlessMode50BestTime == 0) {
					EndlessMode50BestTime = playerTimeScore;
				} else {
					EndlessMode50BestTime = ((playerTimeScore > EndlessMode50BestTime) ? playerTimeScore : EndlessMode50BestTime);
				}
				break;
			}
			break;
		}
	}

	public void AddNoBlackBubbles(int p_count, bool p_allModes = false) {
		if (p_allModes) {
			NoBlackBubblesCountAllModes += p_count;
		} else {
			switch (gameMode) {
			case GameMode.timeMode50:
				NoBlackBubblesCountTimeMode50 += p_count;
				break;
			case GameMode.timeMode100:
				NoBlackBubblesCountTimeMode100 += p_count;
				break;
			case GameMode.timeMode150:
				NoBlackBubblesCountTimeMode150 += p_count;
				break;
			case GameMode.endlessMode5:
				NoBlackBubblesCountEndlessMode5 += p_count;
				break;
			case GameMode.endlessMode25:
				NoBlackBubblesCountEndlessMode25 += p_count;
				break;
			case GameMode.endlessMode50:
				NoBlackBubblesCountEndlessMode50 += p_count;
				break;
			}
		}
	}

	public bool IsNoBlackBubbles() {
		if (NoBlackBubblesCountAllModes > 0) {
			NoBlackBubblesCountAllModes--;
			return true;
		} else {
			switch (gameMode) {
			case GameMode.timeMode50:
				if(NoBlackBubblesCountTimeMode50 > 0) {
					NoBlackBubblesCountTimeMode50--;
					return true;
				} 
				break;
			case GameMode.timeMode100:
				if(NoBlackBubblesCountTimeMode100 > 0) {
					NoBlackBubblesCountTimeMode100--;
					return true;
				} 
				break;
			case GameMode.timeMode150:
				if(NoBlackBubblesCountTimeMode150 > 0) {
					NoBlackBubblesCountTimeMode150--;
					return true;
				} 
				break;
			case GameMode.endlessMode5:
				if(NoBlackBubblesCountEndlessMode5 > 0) {
					NoBlackBubblesCountEndlessMode5--;
					return true;
				} 
				break;
			case GameMode.endlessMode25:
				if(NoBlackBubblesCountEndlessMode25 > 0) {
					NoBlackBubblesCountEndlessMode25--;
					return true;
				} 
				break;
			case GameMode.endlessMode50:
				if(NoBlackBubblesCountEndlessMode50 > 0) {
					NoBlackBubblesCountEndlessMode50--;
					return true;
				} 
				break;
			}
		}

		return false;
	}

	public void TimeModeSuccess() {
		timeModeSuccess = true;
		ChangeState(GameState.gameOver);
	}
}
