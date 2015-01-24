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

public class GameController : MonoBehaviour {

	public static GameController instance { get; private set; }

	public GameState gameState = GameState.main;
	public int highScore;
	public int playerScore;
	public bool isPaused = false;

	public int HighScore {
		get { 
			if(PlayerPrefs.HasKey("HighScore")) {
				highScore = PlayerPrefs.GetInt("HighScore");
				return  highScore;
			} else {
				highScore = 0;
				PlayerPrefs.SetInt("HighScore", 0);
				PlayerPrefs.Save();
				return 0;
			}
		}
		set {
			highScore = value;
			PlayerPrefs.SetInt("HighScore", value);
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
	}

	private void Init() {
		highScore = HighScore;
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
		Debug.Log("GAME OVER!!!");
		HighScore = (playerScore > HighScore) ? playerScore : HighScore;
		// TODO GameHUD - show Score and HighScore
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
