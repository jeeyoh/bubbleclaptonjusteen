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
	public float playerTimeScore;
	public float timeMode50BestTime;
	public float timeMode100BestTime;
	public float timeMode150BestTime;
	public float endlessMode5BestTime;
	public float endlessMode25BestTime;
	public float endlessMode50BestTime;
	public bool isPaused = false;
	public bool deletePlayerPrefs;

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

	void Awake() {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start() {
		Init ();
		if(deletePlayerPrefs) PlayerPrefs.DeleteAll();
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
	}

	public void ChangeState(GameState p_newState) {
		gameState = p_newState;
		ProcessGameState();
	}

	private void ProcessGameState() {
		switch(gameState) {
		case GameState.main:
			LoadMainScene();
			break;
		case GameState.startGame:
			LoadGameScene();
			break;
		case GameState.playing:
			Resume();
			break;
		case GameState.paused:
			Pause();
			break;
		case GameState.gameOver:
			GameOver();
			break;
		case GameState.playAgain:
//			PlayAgain();
			break;
		}
	}

	private void LoadMainScene() {
		StartCoroutine(LoadScene("MainScene", 0));
	}

	private void LoadGameScene() {
		timeModeSuccess = false;
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
		SoundController.instance.StopMusic();
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
					if(TimeMode50BestTime == -1) {
						TimeMode50BestTime = playerTimeScore;
					} else {
						TimeMode50BestTime = ((playerTimeScore < TimeMode50BestTime) ? playerTimeScore : TimeMode50BestTime);
					}
					break;
				case GameMode.timeMode100:
					if(TimeMode100BestTime == -1) {
						TimeMode100BestTime = playerTimeScore;
					} else {
						TimeMode100BestTime = ((playerTimeScore < TimeMode100BestTime) ? playerTimeScore : TimeMode100BestTime);
					}
					break;
				case GameMode.timeMode150:
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
				if(EndlessMode5BestTime == 0) {
					EndlessMode5BestTime = playerTimeScore;
				} else {
					EndlessMode5BestTime = ((playerTimeScore > EndlessMode5BestTime) ? playerTimeScore : EndlessMode5BestTime);
				}
				break;
			case GameMode.endlessMode25:
				if(EndlessMode25BestTime == 0) {
					EndlessMode25BestTime = playerTimeScore;
				} else {
					EndlessMode25BestTime = ((playerTimeScore > EndlessMode25BestTime) ? playerTimeScore : EndlessMode25BestTime);
				}
				break;
			case GameMode.endlessMode50:
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

//	public IEnumerator LoadLevel() {
//		Application.LoadLevel("GameScene");
//		yield return null;
//		PlayAgain();
//	}

	public void TimeModeSuccess() {
		timeModeSuccess = true;
		ChangeState(GameState.gameOver);
	}

//	public void EndlessModeDone() {
//		ChangeState(GameState.gameOver);
//	}

//	private void PlayAgain() {
//		Debug.Log("PLAY AGAIN");
//		 TODO Clean all object instance including player instance
//		 TODO Restart GameHud
//		 TODO Restart Level
//		 TODO Instantiate player instance
//		 TODO ChangeState(GameState.playing)
//	}
}
