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
	public float bestTimeModeScore;
	public float bestEndlessModeScore;
	public bool isPaused = false;

	public delegate void GameOverEvent();
	public event GameOverEvent OnGameOver;

	public bool AllowAds {
		get { 
			if(PlayerPrefs.HasKey("AllowAds")) {
				allowAds = (PlayerPrefs.GetInt("AllowAds") == 1 ? true : false);
				return allowAds;
			} else {
				allowAds = true; // default value
				PlayerPrefs.SetInt("AllowAds", 1);
				PlayerPrefs.Save();
				return false;
			}
		}
		set {
			allowAds = value;
			PlayerPrefs.SetInt("AllowAds", value ? 1 : 0);
			PlayerPrefs.Save();
		}
	}

	public bool AllowSound {
		get { 
			if(PlayerPrefs.HasKey("AllowSound")) {
				allowSound = (PlayerPrefs.GetInt("AllowSound") == 1 ? true : false);
				return allowSound;
			} else {
				allowSound = true; // default value
				PlayerPrefs.SetInt("AllowSound", 1);
				PlayerPrefs.Save();
				return true;
			}
		}
		set {
			allowSound = value;
			PlayerPrefs.SetInt("AllowSound", value ? 1 : 0);
			PlayerPrefs.Save();
		}
	}

	public float TimeModeBestScore {
		get { 
			if(PlayerPrefs.HasKey("BestTimeModeScore")) {
				bestTimeModeScore = PlayerPrefs.GetFloat("BestTimeModeScore");
				return  bestTimeModeScore;
			} else {
				bestTimeModeScore = 0;
				PlayerPrefs.SetFloat("BestTimeModeScore", 0);
				PlayerPrefs.Save();
				return 0;
			}
		}
		set {
			bestTimeModeScore = value;
			PlayerPrefs.SetFloat("BestTimeModeScore", value);
			PlayerPrefs.Save();
		}
	}

	public float EndlessModeBestScore {
		get { 
			if(PlayerPrefs.HasKey("BestEndlessModeScore")) {
				bestEndlessModeScore = PlayerPrefs.GetFloat("BestEndlessModeScore");
				return  bestEndlessModeScore;
			} else {
				bestEndlessModeScore = 0;
				PlayerPrefs.SetFloat("BestEndlessModeScore", 0);
				PlayerPrefs.Save();
				return 0;
			}
		}
		set {
			bestEndlessModeScore = value;
			PlayerPrefs.SetFloat("BestEndlessModeScore", value);
			PlayerPrefs.Save();
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

	public bool deletePlayerPrefs;
	void Start() {
		Init ();
		if(deletePlayerPrefs) PlayerPrefs.DeleteAll();
	}

	private void Init() {
		allowAds = AllowAds;
		allowSound = AllowSound;
		bestTimeModeScore = TimeModeBestScore;
		bestEndlessModeScore = EndlessModeBestScore;
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
				if(TimeModeBestScore == 0) {
					TimeModeBestScore = playerTimeScore;
				} else {
					TimeModeBestScore = ((playerTimeScore < TimeModeBestScore) ? playerTimeScore : TimeModeBestScore);
				}
			} else {
				playerTimeScore = 0;
			}
			break;
		case GameModeType.endlessMode:
			playerTimeScore = GameHUDController.instance.GetTime();
			if(EndlessModeBestScore == 0) {
				EndlessModeBestScore = playerTimeScore;
			} else {
				EndlessModeBestScore = ((playerTimeScore > EndlessModeBestScore) ? playerTimeScore : EndlessModeBestScore);
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
