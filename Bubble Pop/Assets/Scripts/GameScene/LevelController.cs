using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public static LevelController instance;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init ();
	}

	public void Init() {
		Invoke("StartGame", 1f);
	}

	public void StartGame() {
		GameController.instance.ChangeState(GameState.playing);

		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			BubbleCreator.instance.StartTimeMode(50, 5);
			break;
		case GameMode.timeMode100:
			BubbleCreator.instance.StartTimeMode(100, 10);
			break;
		case GameMode.timeMode150:
			BubbleCreator.instance.StartTimeMode(150, 15);
			break;
		case GameMode.endlessMode5:
			BubbleCreator.instance.StartEndlessMode(1, 0.1f, 5, 5);
			break;
		case GameMode.endlessMode25:
			BubbleCreator.instance.StartEndlessMode(1, 0.1f, 5, 25);
			break;
		case GameMode.endlessMode50:
			BubbleCreator.instance.StartEndlessMode(1, 0.1f, 5, 50);
			break;
		}

		GameHUDController.instance.StartTime();
	}
}
