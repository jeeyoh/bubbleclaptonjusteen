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
	public GameMode gameMode;
	public bool allowAds;
	public bool timeModeSuccess;
	public float playerTimeScore;
	public float bestTimeModeScore;
	public float bestEndlessModeScore;
	public bool isPaused = false;

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
//		PlayerPrefs.DeleteAll();
	}

	private void Init() {
		bestTimeModeScore = TimeModeBestScore;
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
			PlayAgain();
			break;
		}
	}

	private void LoadMainScene() {
		Application.LoadLevel("MainScene");
	}

	private void LoadGameScene() {
		timeModeSuccess = false;
		Application.LoadLevel("GameScene");
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
		GameHUDController.instance.StopTime();
		if(timeModeSuccess) {
			playerTimeScore = GameHUDController.instance.GetTime();
			TimeModeBestScore = ((playerTimeScore < TimeModeBestScore) ? playerTimeScore : TimeModeBestScore);
		} else {
			playerTimeScore = 0.00f;
		}
		Application.LoadLevel("GameOverScene");
	}

	public IEnumerator LoadLevel() {
		Application.LoadLevel("GameScene");
		yield return null;
		PlayAgain();
	}

	private void PlayAgain() {
		Debug.Log("PLAY AGAIN");
		// TODO Clean all object instance including player instance
		// TODO Restart GameHud
		// TODO Restart Level
		// TODO Instantiate player instance
		// TODO ChangeState(GameState.playing)
	}
}
